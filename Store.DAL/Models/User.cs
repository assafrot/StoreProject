using Store.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Range(typeof(DateTime),"1/1/1900","6/6/2079")]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
