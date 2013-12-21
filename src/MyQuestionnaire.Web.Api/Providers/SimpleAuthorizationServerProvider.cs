using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace MyQuestionnaire.Web.Api.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // validate client credentials
            // should be stored securely (salted, hashed, iterated)
            string id, secret;
            if (context.TryGetBasicCredentials(out id, out secret))
            {
                if (secret == "secret")
                {
                    // need to make the client_id available for later security checks
                    context.OwinContext.Set<string>("as:client_id", id);
                    context.Validated();
                }
            }
            // Resource owner password credentials does not provide a client ID.
            //if (context.ClientId == null)
            //{
            //    context.Validated();
            //}

            return Task.FromResult<object>(null);
            
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // validate user credentials (demo mode)
            // should be stored securely (salted, hashed, iterated)        
            if (context.UserName != context.Password)
            {
                context.Rejected();
                return;
            }

            // create identity
            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("sub", context.UserName));
            id.AddClaim(new Claim("role", "user"));

            // create metadata to pass on to refresh token provider
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:client_id", context.ClientId }
                });

            var ticket = new AuthenticationTicket(id, props);
            context.Validated(ticket);
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

            // chance to change authentication ticket for refresh token requests
            var newId = new ClaimsIdentity(context.Ticket.Identity);
            newId.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);
        }
    }
}