using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace CowSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(object sender, System.EventArgs e)
        {
            var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
            if (formsIdentity != null && formsIdentity.IsAuthenticated && formsIdentity.AuthenticationType == "Forms")
            {
                HttpContext.Current.User = EMuTongFormsAuthentication<EMuTongUserDataPrincipal>.TryParsePrincipal(HttpContext.Current.Request);
            }
        }
    }
}
