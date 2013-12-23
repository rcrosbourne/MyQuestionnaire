using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyQuestionnaire.Web.Api.Models
{
    public class ApplicationRole : IdentityRole
    {
        private ICollection<ApplicationClaim> _applicationClaims = new Collection<ApplicationClaim>();

        public virtual ICollection<ApplicationClaim> ApplicationClaims
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