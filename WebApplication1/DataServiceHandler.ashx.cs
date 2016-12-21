using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// DataServiceHandler 的摘要说明
    /// </summary>
    public class DataServiceHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.QueryString["name"];
            if (name == "getpatname")
            {
                if (context.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName] != null)
                {
                    string dept_name = System.Web.Security.FormsAuthentication.Decrypt(context.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName].Value).Name.Split(new char[] { ',' })[2];
                    string names;
                    DataService.get_patients_name(dept_name, out names);
                    context.Response.ClearContent();
                    context.Response.ContentType = "application/json";
                    //context.Response.Write((new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(names));
                    context.Response.Write(names);
                }
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}