using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.Ajax.Utilities;
using MyQuestionnaire.Web.Api.DBContext;
using MyQuestionnaire.Web.Api.Models;
using WebGrease.Css.Extensions;

namespace MyQuestionnaire.Web.Api.AuthorizationConfiguration
{
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            /*
             * Validationa Sequence check
             * 1) Check Principal Claims
             *      If principal has zero claims then return false (token expiration most likely)
             *      else get the role claims 
             *          if prinipal has admin role then return true
             *          else build a unique list of claims from the role claims
             *          then check to see if principal has a claim otherwise return false
             */

            var principalClaims = context.Principal.Claims;
            var principalClaimsArrary = principalClaims as Claim[] ?? principalClaims.ToArray();
            if (!principalClaimsArrary.Any())
            {
                return false;
            }
            //So we have lets get all the role claims
            var roleClaims = principalClaimsArrary.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value);

            var roleClaimsArray = roleClaims as string[] ?? roleClaims.ToArray();
            if (!roleClaimsArray.Any())
            {
                return false;
            }

            //get the db context
            var dbContext = new MyQuestionnaireDbContext();
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;
            var applicationClaims = new List<ApplicationClaim>();
            dbContext.ApplicationRoles.ForEach(role =>
            {
                
                if (roleClaimsArray.Contains(role.Name))
                {
                    applicationClaims.AddRange(role.ApplicationClaims.ToList());
                } 
            });
            applicationClaims = applicationClaims.Distinct().ToList();
            return applicationClaims.Any(c => c.ClaimType == action && c.ClaimValue == resource);
        }
    }
}