using Store.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Store.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private StoreContext _context;
        private bool _disposed = false;

        public UserRepository()
        {
            _context = new StoreContext();
        }

        public void Create(User obj)
        {
            _context.Users.Add(obj);
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

        public User Read(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> Read()
        {
            return _context.Users.ToList();
        }

        public IEnumerable<User> Read(Expression<Func<User, bool>> predicate)
        {
            var res = _context.Users.Where(predicate);
            return res.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public User Update(int id, User updatedObj)
        {
            var UserToUpdate = _context.Users.Find(id);
            var UserEntry = _context.Entry(UserToUpdate);
            UserEntry.CurrentValues.SetValues(updatedObj);
            UserEntry.State = EntityState.Modified;
            return UserToUpdate;
        }

        public bool IsUserAvailible(string username)
        {
            var name = _context.Users.Where(n => n.UserName == username).FirstOrDefault();
            return (name == null);

        }
    }
}
