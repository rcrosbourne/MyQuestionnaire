using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization.WebApi;

namespace MyQuestionnaire.Web.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
