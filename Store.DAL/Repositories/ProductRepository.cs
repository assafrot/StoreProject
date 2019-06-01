using Common;
using Store.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Store.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private StoreContext _context;
        private bool _disposed = false;

        public ProductRepository()
        {
            _context = new StoreContext();
        }

        public void Create(Product obj)
        {
            _context.Products.Add(obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public Product Read(int id)
        {
            return _context.Products.Include("User").Include("Owner").FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> Read()
        {
            return _context.Products.Include("User").Include("Owner").ToList();
        }

        public IEnumerable<Product> Read(Expression<Func<Product, bool>> predicate)
        {
            return _context.Products.Include("User").Include("Owner").Where(predicate).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Product Update(int id, Product updatedObj)
        {
            var ProductToUpdate = _context.Products.Find(id);//Load old object
            var ProductEntry = _context.Entry(ProductToUpdate);
            ProductEntry.CurrentValues.SetValues(updatedObj);//update attached object
            ProductEntry.State = EntityState.Modified;
            return ProductToUpdate;
        }

        public IEnumerable<Product> GetCart(int userId)
        {
            var res = _context.Products.Where(x => x.UserId == userId
                                                   && x.State == StateEnum.InCart);
            return res;
        }

        public void PlaceOrder(IEnumerable<Product> cart)
        {
            foreach (var product in cart)
            {
                product.State = StateEnum.Sold;
                Update(product.Id, product);
            }
        }

        public void ReleaseProducts(DateTime currentTime)
        {
            var list = Read(x => x.State == StateEnum.InCart);
            foreach (var item in list)
            {
                if (item.AdddedToCart + TimeSpan.FromMinutes(1) < currentTime)
                {
                    item.State = StateEnum.Available;
                    item.AdddedToCart = new DateTime(1900, 1, 1);
                    item.UserId = null;
                    Update(item.Id, item);
                }
            }
        }
    }
}
