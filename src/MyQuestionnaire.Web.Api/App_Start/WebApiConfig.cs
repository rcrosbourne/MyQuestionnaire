using System.IdentityModel.Services;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using MyQuestionnaire.Web.Api.Controllers;
using Thinktecture.IdentityModel.Authorization.WebApi;
using Thinktecture.IdentityModel.Tokens.Http;

namespace MyQuestionnaire.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //ClaimsFilter
            config.Filters.Add(new ClaimsAuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "HomeApiStaticPage" }
            );
            //var authConfig = new AuthenticationConfiguration
            //{
            //    InheritHostClientIdentity = true,
            //    RequireSsl = false,
            //    ClaimsAuthenticationManager = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager
            //};

            // setup authentication against membership
            //authConfig.AddBasicAuthentication();

            //config.MessageHandlers.Add(new AuthenticationHandler(authConfig));
            
        }
    }
}
