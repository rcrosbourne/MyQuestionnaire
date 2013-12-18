using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyQuestionnaire.Web.Api.Models
{
    //public class RoleClaim
    //{

    //    public IdentityRole Role { get; set; }
    //    public List<ApplicationClaim> Claims { get; set; } 

    //}
    public class ApplicationRole : IdentityRole
    {
        private List<ApplicationClaim> _applicationClaims = new List<ApplicationClaim>();

        public List<ApplicationClaim> ApplicationClaims
        {
            get
            {
                return _applicationClaims;
            }
            set
            {
                _applicationClaims = value;
            }
        }
    }
}