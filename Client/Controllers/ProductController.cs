using Client.Extensions;
using Client.Models;
using Common;
using Store.DAL.Repositories;
using Store.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult GetProduct(int? id)
        {
            Uri prev = Request.UrlReferrer;
            ProductDto product = null;
            using (var repo = new ProductRepository())
            {
                if (id != null)
                    product = repo.Read((int)id).ToDto();
            }

            if (product == null)
            {
                if (prev == null)
                    return Redirect("/");
                return Redirect(prev.ToString());
            }

            ViewBag.refferer = prev.ToString();
            return View(product);
        }

        [HttpGet]
        public ActionResult PublishProduct()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/");
            return View();
        }

        [HttpPost]
        public ActionResult PostProduct(ProductDto productDto, List<HttpPostedFileBase> upload)
        {
            if (!ModelState.IsValid)
            {
                return View("PublishProduct", productDto);
            }

            else
            {
                int propIndex = 1;
                for (int i = 0; i < upload.Count; i++)
                {
                    if (upload[i] != null && upload[i].ContentLength > 0)
                    {
                        string imagePath = Path.GetFileName(upload[i].FileName);
                        using (var reader = new BinaryReader(upload[i].InputStream))
                        {
                            PropertyInfo propinfo = productDto.GetType().GetProperty($"Image{propIndex++}");
                            var value = reader.ReadBytes(upload[i].ContentLength);
                            propinfo.SetValue(productDto, value);
                        }
                    }
                }

                productDto.OwnerId = GetUserId();
                using (var repo = new ProductRepository())
                {
                    repo.Create(productDto.ToDbObject());
                    repo.Save();
                }
                return View("success");
            }
        }

        public ActionResult AddToCart(int productId)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = GetUserId();
            }

            using (var repo = new ProductRepository())
            {
                var productToUpdate = repo.Read(productId);
                if (productToUpdate != null)
                {
                    productToUpdate.State = StateEnum.InCart;
                    productToUpdate.AdddedToCart = DateTime.Now;
                    if (userId != null)
                        productToUpdate.UserId = userId;
                    else
                        AddToSession(repo.Read(productId).ToDto());
                    repo.Update(productId, productToUpdate);
                    repo.Save();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddToSession(ProductDto product)
        {
            List<ProductDto> cart;
            if (Session["cart"] == null)
                cart = new List<ProductDto>();
            else
                cart = (List<ProductDto>)Session["cart"];

            cart.Add(product);
            Session["cart"] = cart;
        }

        public ActionResult RemoveFromCart(int productId)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = GetUserId();
            }
            using (var repo = new ProductRepository())
            {
                var productToUpdate = repo.Read(productId);
                if (productToUpdate != null)
                {
                    productToUpdate.State = StateEnum.Available;
                    productToUpdate.AdddedToCart = new DateTime(1900, 1, 1);
                    if (userId != null)
                        productToUpdate.UserId = null;
                    else
                        RemoveFromSession(productId);
                    repo.Update(productId, productToUpdate);
                    repo.Save();
                }
            }
            return RedirectToAction("ViewCart", "Product");
        }

        private void RemoveFromSession(int productId)
        {
            if (Session["cart"] != null)
            {
                List<ProductDto> cart = (List<ProductDto>)Session["cart"];
                int prodIndex = cart.FindIndex(x => x.Id == productId);
                cart.RemoveAt(prodIndex);
            }
        }

        public ActionResult PlaceOrder()
        {
            List<Product> prodList = new List<Product>();
            if (User.Identity.IsAuthenticated)
            {
                int userId = GetUserId();
                using (var repo = new ProductRepository())
                {
                    prodList = repo.GetCart(userId)
                        .ToList();
                }
            }
            else
            {
                List<ProductDto> dtoList = (List<ProductDto>)Session["cart"];
                foreach (var product in dtoList)
                    prodList.Add(product.ToDbObject());
                Session["cart"] = null;
            }

            using (var repo = new ProductRepository())
            {
                repo.PlaceOrder(prodList);
                repo.Save();
            }
            return View("OrderSuccess");
        }

        public ActionResult ViewCart()
        {
            List<ProductDto> cartList = new List<ProductDto>();
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = GetUserId();
            }

            if (userId != null)
            {
                using (var repo = new ProductRepository())
                {
                    cartList = repo.GetCart((int)userId).Select(x => x.ToDto())
                        .ToList();
                }
            }
            else
            {
                if (Session["cart"] == null)
                    cartList = new List<ProductDto>();
                else
                {
                    ReleaseFromSession();
                    cartList = (List<ProductDto>)Session["cart"];
                }
            }

            ViewBag.sum = cartList.Sum(x => x.Price);
            if (User.Identity.IsAuthenticated)
                ViewBag.sumformember = ViewBag.sum * (decimal)0.9;
            return View(cartList);
        }

        private void ReleaseFromSession()
        {
            if (Session["cart"] != null)
            {
                List<ProductDto> list = (List<ProductDto>)Session["cart"];
                using (var repo = new ProductRepository())
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (repo.Read(list[i].Id).State == StateEnum.Available)
                            list.RemoveAt(i);
                    }
                }
                Session["cart"] = list;
            }
        }

        private int GetUserId()
        {
            int res;
            using (var repo = new UserRepository())
            {
                res = repo.Read(x => x.UserName == User.Identity.Name).Select(x => x.Id).FirstOrDefault();
            }
            return res;
        }
    }
}