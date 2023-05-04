using System;
using System.Web;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;
using System.Net;

namespace FlightBookingSystem
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var userRole = GlobalState.UserRole;
            if (userRole == "Admin")
            {
                return;
            }
            //base.AuthenticationSchemes(filterContext);
        }
         
    }

}

