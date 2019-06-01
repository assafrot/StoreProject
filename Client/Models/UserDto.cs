using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Client.Extensions;

namespace Client.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please fill in First name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please fill in last name")]
        public string LastName { get; set; }
        [DisplayName("Birth Date")]
        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DateTimeRange]
        public DateTime BirthDate { get; set; }
        [DisplayName("E-mail address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address"),Required(ErrorMessage = "Please fill in your birth date")]
        public string Email { get; set; }
        [DisplayName("User Name"), Required(ErrorMessage = "Please fill in username")]
        [UniqueUser(ErrorMessage = "User Is Already taken")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Password"), Required(ErrorMessage = "Please fill in password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName(" Confirm Password"), Required(ErrorMessage = "Please fill in password")]
        [Compare("Password",ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}