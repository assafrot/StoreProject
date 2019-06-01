using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Store.DAL.Repositories;

namespace Client.Extensions
{
    public class UniqueUserAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (var repo = new UserRepository())
                {
                    if (repo.IsUserAvailible(value.ToString()))
                        return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}