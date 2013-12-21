using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyQuestionnaire.Web.Api.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
            
        }
        public virtual ICollection<ApplicationClaim> ApplicationClaims { get; set; }
    }
}