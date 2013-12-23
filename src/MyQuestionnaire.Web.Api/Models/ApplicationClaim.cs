using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyQuestionnaire.Web.Api.Models
{
    public class ApplicationClaim 
    {
      
        public int Id { get; set; }
       
        [Required]
        public string ClaimType { get; set; }
       
        [Required]
        public string  ClaimValue { get; set; }

        private ICollection<ApplicationRole> _applicationRoles = new Collection<ApplicationRole>();

        public virtual ICollection<ApplicationRole> ApplicationRoles
        {
            get
            {
                return _applicationRoles;
            }
            set
            {
                _applicationRoles = value;
            }
        }
    }

    public static class SetupApplicationClaims
    {
        /// <summary>
        /// OpenEndedQuestionClaims
        /// </summary>
        /// <returns></returns>
        public static ApplicationClaim PostOpenEndedQuestion()
        {
            return new ApplicationClaim
            {
                ClaimType = "PostOpenEndedQuestion",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim GetAllOpenEndedQuestion()
        {
            return new ApplicationClaim
            {
                ClaimType = "GetAllOpenEndedQuestion",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim GetOneOpenEndedQuestion()
        {
            return new ApplicationClaim
            {
                ClaimType = "GetOneOpenEndedQuestion",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim PutOpenEndedQuestion()
        {
            return new ApplicationClaim
            {
                ClaimType = "PutOpenEndedQuestion",
                ClaimValue = "OpenEndedQuestion"
            };
        }
       
        public static ApplicationClaim DeleteOpenEndedQuestion()
        {
            return new ApplicationClaim
            {
                ClaimType = "DeleteOpenEndedQuestion",
                ClaimValue = "OpenEndedQuestion"
            };
        }
    }
}