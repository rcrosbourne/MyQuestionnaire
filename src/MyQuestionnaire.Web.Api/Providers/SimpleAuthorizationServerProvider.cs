using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MyQuestionnaire.Web.Api.DBContext;
using MyQuestionnaire.Web.Api.Models;
using MyQuestionnaire.Web.Common;

namespace MyQuestionnaire.Web.Api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IDbContext _dbContext;
        private readonly Func<UserManager<ApplicationUser>> _userManagerFactory;
        public SimpleAuthorizationServerProvider(IDbContext dbContext, Func<UserManager<ApplicationUser>> userManagerFactory)
        {
            _dbContext = dbContext;
            _userManagerFactory = userManagerFactory;
        }

        public SimpleAuthorizationServerProvider(Func<UserManager<ApplicationUser>> userManagerFactory)
        {
            _userManagerFactory = userManagerFactory;
            _dbContext = WebContainerManager.Get<IDbContext>();
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // validate client credentials
            // should be stored securely (salted, hashed, iterated)
            
            string id, secret;
            if (context.TryGetBasicCredentials(out id, out secret))
            {
                var client = _dbContext
                    .ApiClients
                    .AsEnumerable()
                    .SingleOrDefault(c => c.Id.ToString() == id && c.IsBlacklisted == false);

                if (client != null)
                {
                    // need to make the client_id available for later security checks
                    context.OwinContext.Set("as:client_id", client.Id.ToString());
                    //context.OwinContext.Set("as:client_name", client.Name);
                    context.Validated();
                    return Task.FromResult<object>(null);
                }

            }
            context.Rejected();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // validate user credentials (demo mode)
            // should be stored securely (salted, hashed, iterated)      
            //UserManager needs to do authentication
            using (var userManager = _userManagerFactory())
            {
                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user,
                    context.Options.AuthenticationType);
                ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user,
                    CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = CreateProperties(user.UserName, context.ClientId);
                var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            
            }
        }

        public static AuthenticationProperties CreateProperties(string userName, string clientId)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "as:client_id", clientId }
            };
            return new AuthenticationProperties(data);
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.OwinContext.Get<string>("as:client_id");

            // enforce client binding of refresh token
            if (originalClient != currentClient)
            {
                context.Rejected();
                return;
            }
            var existingIdentity = new ClaimsIdentity(context.Ticket.Identity);
            //Ensure that the user is still a apart of the roles.
            using (var userManager = _userManagerFactory())
            {
                var user = await userManager.FindByNameAsync(existingIdentity.Name); //<--Fixed this. Get the current name property

                if (user == null)
                {

                    context.SetError("invalid_user",
                        string.Format("{0} not a valid user", existingIdentity.NameClaimType));
                    context.Rejected();
                    return;
                }

                var newId = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);
                var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
                context.Validated(newTicket);
            }
        }
    }
}