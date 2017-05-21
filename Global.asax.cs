using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VK_Analyze.Controllers;
using VK_Analyze.Controllers.functions;
using VkNet;

namespace VK_Analyze
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        protected void _Application_Error(object sender, EventArgs e)
        {

            Exception exception = Server.GetLastError();
            Response.Clear();



            Session["error"] = exception.Message;
            // clear error on server
            Server.ClearError();

            Response.Redirect("~/Error/HttpError/", false);
        }

    }
}
