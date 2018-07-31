﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VsShop.Auth
{
    public class MinimumOrderAgeRequirement : AuthorizationHandler<MinimumOrderAgeRequirement>, IAuthorizationRequirement
    {
        private readonly int _minimumOrderAge;

        public MinimumOrderAgeRequirement(int minimumOrderAge)
        {
            _minimumOrderAge = minimumOrderAge;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumOrderAgeRequirement requirement)
        {
            if(!context.User.HasClaim(c=>c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }
            var birthday = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
            var ageInYears = DateTime.Today.Year - birthday.Year;
            if (ageInYears >= _minimumOrderAge)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
