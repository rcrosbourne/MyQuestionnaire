using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        private List<ApplicationRole> _applicationRoles = new List<ApplicationRole>();

        public List<ApplicationRole> ApplicationRoles
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
                ClaimType = "CAN_CREATE",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim ReadAllOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "CAN_READ_ALL",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim ReadByIdOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "CAN_READ_BY_ID",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim UpdateAllOpenEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "CAN_UPDATE_ALL",
                ClaimValue = "OpenEndedQuestion"
            };
        }

        public static ApplicationClaim UpdateOneEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "CAN_UPDATE_ONE",
                ClaimValue = "OpenEndedQuestion"
            };
        }
        public static ApplicationClaim UpdateAllEndedQuestion()
        {
            return new ApplicationClaim()
            {
                ClaimType = "CAN_UPDATE_ALL",
                ClaimValue = "OpenEndedQuestion"
            };
        }
    }
}