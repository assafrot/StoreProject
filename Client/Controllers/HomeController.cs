using Client.Extensions;
using Client.Models;
using Common;
using Store.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string order = "date")
        {
            List<ProductDto> orderProducts;
            using (var repo = new ProductRepository())
            {
                var products = repo.Read().Where(x => x.State == StateEnum.Available)
                    .Select(x => x.ToDto());
                if (order == "title")
                    orderProducts = products.OrderBy(x => x.Title).ToList();
                else
                    orderProducts = products.OrderByDescending(x => x.Date).ToList();
            }
            return View(orderProducts);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}