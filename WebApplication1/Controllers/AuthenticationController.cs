using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                string dept_name; string name; string password; string role;
                DataService.get_staff(Username,out dept_name,out name,out password,out role);
                if(Password == password)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                                                     Username + "," + name + "," + dept_name,
                                                                                     DateTime.Now,
                                                                                     DateTime.Now.AddMinutes(15),
                                                                                     false,
                                                                                     role);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    authCookie.HttpOnly = true;
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "NurseEvent");
                }
            }
            ModelState.AddModelError("", "无效的登录尝试。");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "NurseEvent");
        }

    }
}