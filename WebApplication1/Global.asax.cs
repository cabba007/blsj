using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
            {
                return;
            }
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }
            string[] roles = authTicket.UserData.Split(new char[] { ',' });
            if (Context.User != null)
            {
                if (roles.Contains("领导组"))
                    Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, new string[]{"领导组"});
                else if(roles.Contains("护士长组"))
                    Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, new string[] { "护士长组" });
                else if (roles.Contains("护士组"))
                    Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, new string[] { "护士组" });
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
