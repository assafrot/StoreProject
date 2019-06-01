using System;
using System.Collections.Generic;
using Store.DAL.Repositories;
using System.Timers;
using System.Web.Mvc;
using System.Web.Routing;
using Client.Controllers;
using Client.Models;
using Common;
using System.Web;

namespace Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Timer timer = new Timer(30000);
            timer.Elapsed += ReleaseProduct;
            timer.Start();

        }

         void ReleaseProduct(object sender,EventArgs args)
        {
            using (var repo = new ProductRepository())
            {
                repo.ReleaseProducts(DateTime.Now);
                repo.Save();
            }

        }

         
        
    }
}
