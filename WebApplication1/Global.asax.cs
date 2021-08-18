using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Person.friends.Add(new Person { firstName = "Jimmy", lastName = "Jim", id = 1 });
            Person.friends.Add(new Person { firstName = "Annie", lastName = "Ann", id = 2 });
            Person.friends.Add(new Person { firstName = "Timmie", lastName = "Tim", id = 3 });
        }
    }
}
