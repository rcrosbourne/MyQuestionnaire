using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace MyQuestionnaire.Web.Api.AuthorizationConfiguration
{
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            Trace.WriteLine("\n\nClaimsAuthorizationManager\n_______________________\n");

            Trace.WriteLine("\nAction:"); //Name of the API Method
            var action = context.Action.First().Value; //API MethodName
            var res = context.Resource.First().Value;
            Trace.WriteLine("  " + action) ;

            Trace.WriteLine("\nResources:");
            foreach (var resource in context.Resource)
            {
                Trace.WriteLine("  " + resource.Value);
            }

            Trace.WriteLine("\nClaims:"); // If Claims is zero it could mean token expiration.
            foreach (var claim in context.Principal.Claims)
            {
                Trace.WriteLine(claim.Type + "  " + claim.Value);
            }
            return true;

            //return context.Principal.HasClaim(action, res);
        }
    }
}