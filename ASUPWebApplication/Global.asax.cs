using ASUPWebApplication.Initializers;
using ASUPWebApplication.Models;
using SeeAllClassLibrary.Entities;
using SeeAllClassLibrary.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ASUPWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<ApplicationDbContext>(new ASUPWebApplicationDbInitializer());
            var dbASUPWebApplication = new ApplicationDbContext();
            dbASUPWebApplication.Database.Initialize(true);

            Database.SetInitializer<EFDirectoriesContext>(new SeeAllClassLibraryDbInitializer());
            var dbSeeAllClassLibrary = new EFDirectoriesContext();
            dbSeeAllClassLibrary.Database.Initialize(true);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
