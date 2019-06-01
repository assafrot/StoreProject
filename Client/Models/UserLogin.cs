using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Client.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please fill in user name")]
        [DisplayName("User Name")]
        public string LoginUserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please fill in user password")]
        [DisplayName("Password")]
        public string LoginPassword { get; set; }
    }
}