using Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public UserDto Owner { get; set; }
        public int? UserId { get; set; }
        public UserDto User { get; set; }
        [Required(ErrorMessage = "Please fill in product title")]
        public string Title { get; set; }
        [DisplayName("Short Description")]
        [Required(ErrorMessage = "Please fill this field")]
        public string ShortDescription { get; set; }
        [DisplayName("Long Description: ")]
        [Required(ErrorMessage = "Please fill this field")]
        public string LongDescription { get; set; }
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please fill this field")]
        [Range(typeof(Decimal), "1", "9999", ErrorMessage = "Price should be in the format of xx.xx")]
        public decimal Price { get; set; }
        [DisplayName("Image")]
        public byte[] Image1 { get; set; }
        [DisplayName("Image")]
        public byte[] Image2 { get; set; }
        [DisplayName("Image")]
        public byte[] Image3 { get; set; }
        public StateEnum State { get; set; }
        public DateTime AdddedToCart { get; set; }

        public ProductDto()
        {
            Date = DateTime.Now;
            AdddedToCart = new DateTime(1900, 1, 1);
            State = StateEnum.Available;
        }

    }
}