using System.Web.SessionState;
using Client.Models;
using Store.DAL.Models;
using Store.Models.DAL;

namespace Client.Extensions
{
    internal static class ModelConvertExtension
    {
        internal static UserDto ToDto(this User user)
        {
            if (user == null)
                return null;
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password
            };
        }

        internal static User ToDbObject(this UserDto userDto)
        {
            if (userDto == null)
                return null;
            return new User
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email,
                UserName = userDto.UserName,
                Password = userDto.Password
            };
        }

        internal static ProductDto ToDto(this Product product)
        {
            if (product == null)
                return null;
            return new ProductDto
            {
                Id = product.Id,
                OwnerId = product.OwnerId,
                Owner = product.Owner.ToDto(),
                UserId = product.UserId,
                User = product.User.ToDto(),
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                Date = product.Date,
                Price = product.Price,
                Image1 = product.Image1,
                Image2 = product.Image2,
                Image3 = product.Image3,
                State = product.State,
                AdddedToCart = product.AdddedToCart
            };
        }

        internal static Product ToDbObject(this ProductDto productDto)
        {
            if (productDto == null)
                return null;
            return new Product
            {
                Id = productDto.Id,
                OwnerId = productDto.OwnerId,
                Owner = productDto.Owner.ToDbObject(),
                UserId = productDto.UserId,
                User = productDto.User.ToDbObject(),
                Title = productDto.Title,
                ShortDescription = productDto.ShortDescription,
                LongDescription = productDto.LongDescription,
                Date = productDto.Date,
                Price = productDto.Price,
                Image1 = productDto.Image1,
                Image2 = productDto.Image2,
                Image3 = productDto.Image3,
                State = productDto.State,
                AdddedToCart = productDto.AdddedToCart
            };
        }
    }
}