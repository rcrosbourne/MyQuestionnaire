using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MyQuestionnaire.Web.Api.Models;
using Owin;
using MyQuestionnaire.Web.Api.Providers;

namespace MyQuestionnaire.Web.Api
{
    public partial class Startup
    {
       static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new SimpleAuthorizationServerProvider(UserManagerFactory),//new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1.0),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
           // app.UseCookieAuthentication(new CookieAuthenticationOptions());
           // app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
        }
    }
}
