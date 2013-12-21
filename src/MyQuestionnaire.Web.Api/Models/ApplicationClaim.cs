using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public static ApplicationClaim CreateOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "Create",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim ReadAllOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "ReadAll",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim ReadByIdOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "ReadById",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim UpdateAllOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "UpdateAll",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim UpdateByIdOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "UpdateById",
                ClaimValue = "OpenEndedQuestion"
            };
        }
        public static ApplicationClaim DeleteAllOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "DeleteAll",
                ClaimValue = "OpenEndedQuestion"
            };
        }
        public static ApplicationClaim DeleteByIdOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "DeleteById",
                ClaimValue = "OpenEndedQuestion"
            };
        }
        public static ApplicationClaim AllOnOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "All",
                ClaimValue = "OpenEndedQuestion"
            };
        }
        /// <summary>
        /// This allows you to do all operations on all resources
        /// </summary>
        /// <returns></returns>
        public static ApplicationClaim AllOnAll()
        {
            return new ApplicationClaim()
            {
                ClaimType = "All",
                ClaimValue = "All"
            };
        }
    }
}