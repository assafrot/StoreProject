using Common;
using Store.DAL.Models;
using Store.Models.DAL;
using System;
using System.Data.Entity;

namespace Store.DAL
{
    class StoreInitializer : DropCreateDatabaseAlways<StoreContext>
    {
        public object HttpContext { get; private set; }

        protected override void Seed(StoreContext context)
        {
            var user = new User
            {
                FirstName = "Assaf",
                LastName = "Rot",
                BirthDate = new DateTime(1986, 11, 19),
                Email = "assafrot@gmail.com",
                UserName = "assaf",
                Password = "123"
            };

            var user2 = new User
            {
                FirstName = "Daniel",
                LastName = "Riz",
                BirthDate = new DateTime(1996, 12, 7),
                Email = "danielriz@gmail.com",
                UserName = "daniel",
                Password = "123"
            };

            context.Users.Add(user);
            context.Users.Add(user2);
            var product = new Product
            {
                OwnerId = 1,
                Owner = user,
                Title = "Lenovo Yoga",
                ShortDescription = "laptop",
                LongDescription = "broken",
                Date = DateTime.Now,
                Price = 63,
                State = StateEnum.Available,
                AdddedToCart = DateTime.Now
            };

            var product2 = new Product
            {
                OwnerId = 1,
                Owner = user,
                Title = "Xbox",
                ShortDescription = " video gaming ",
                LongDescription = "good condition.",
                Date = DateTime.Now,
                Price = (decimal)299.99,
                State = StateEnum.Available,
                AdddedToCart = DateTime.Now
            };
            context.Products.Add(product);
            context.Products.Add(product2);

            context.SaveChanges();
        }
    }
}
