using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
                    .SingleOrDefault(c => c.Name == id && c.Id.ToString() == secret && c.IsBlacklisted == false);
                
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

                var id = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType); ;
                //var id = new ClaimsIdentity(context.Options.AuthenticationType); 
                //id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:client_id", context.ClientId }
                });
                var ticket = new AuthenticationTicket(id, props);
                context.Validated(ticket);
                
                //ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user,
                //    context.Options.AuthenticationType);
                //ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user,
                //    CookieAuthenticationDefaults.AuthenticationType);
                //AuthenticationProperties properties = CreateProperties(user.UserName);
                //ticket = new AuthenticationTicket(oAuthIdentity, properties);
                //context.Validated(ticket);
                //context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }

            //if (context.UserName != context.Password)
            //{
            //    context.Rejected();
            //    return;
            //}

            //// create identity
            //id = new ClaimsIdentity(context.Options.AuthenticationType);
            //id.AddClaim(new Claim("sub", context.UserName));
            //id.AddClaim(new Claim("role", "user"));

            //// create metadata to pass on to refresh token provider
            //props = new AuthenticationProperties(new Dictionary<string, string>
            //{
            //    { "as:client_id", context.ClientId }
            //});

            //ticket = new AuthenticationTicket(id, props);
            //context.Validated(ticket);
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

            //Ensure that the user is still a apart of the roles.
            
            // chance to change authentication ticket for refresh token requests
            var newId = new ClaimsIdentity(context.Ticket.Identity);
            newId.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);
        }
    }
}