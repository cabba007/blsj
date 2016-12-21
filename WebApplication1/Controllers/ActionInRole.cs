using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ActionInRoleAttribute : ActionNameSelectorAttribute
    {
        public ActionInRoleAttribute(string role)
        {
            this.role = role;
        }
        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (controllerContext.HttpContext.User.IsInRole(role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string role;
    } 
}