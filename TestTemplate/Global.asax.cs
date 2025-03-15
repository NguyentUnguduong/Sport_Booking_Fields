using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using System.Web.Security;
using System.Security.Principal;

namespace TestTemplate
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Application["LuotTruyCap"] = 0;
        }

        protected void Session_Start()
        {
            Application.Lock(); // Đồng bộ hóa
            if (Application["LuotTruyCap"] == null)
            {
                Application["LuotTruyCap"] = 1;
            }
            else
            {
                Application["LuotTruyCap"] = (int)Application["LuotTruyCap"] + 1;
            }
            Application.UnLock();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var UserCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if(UserCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(UserCookie.Value);
                var roles = authTicket.UserData.Split(new Char[] { ',' });
                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
                Context.User = userPrincipal;
            }
        }

        

        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    Application["LuotTruyCap"] = 0;
        //}
    }
}
