using System.Collections.Generic;
using System.Web.Http;
using MyQuestionnaire.Web.Api.Models.StaticPages;
using MyQuestionnaire.Web.Api.ViewModels;
using Thinktecture.IdentityModel.Authorization;
using Thinktecture.IdentityModel.Authorization.WebApi;

namespace MyQuestionnaire.Web.Api.Controllers
{
    
   
    public class HomeApiStaticPageController : ApiController
    {
        readonly HomeApiStaticPage _homePage = new HomeApiStaticPage();
        // GET api/homeapistaticpage
        [ClaimsAuthorize("Get", "HomeApiStaticPage")]
        public IEnumerable<Link> Get()
        {
            var links = new List<Link>();
            if(ClaimsAuthorization.CheckAccess("Get", "ClientAdministraton"))
                links.AddRange(_homePage.ClientAdministraionLinks);
            if(ClaimsAuthorization.CheckAccess("Get", "UserAcoountAdministration"))
                links.AddRange(_homePage.UserAccountAdministrationLinks);
            if(ClaimsAuthorization.CheckAccess("Get", "QuestionnaireAdministration"))
                links.AddRange(_homePage.QuestionnaireAdministraionLinks);
            return links;

        }
    }
}
