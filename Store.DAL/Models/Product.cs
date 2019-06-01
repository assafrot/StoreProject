using Common;
using Store.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models.DAL
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        [MaxLength(4000)]
        public string LongDescription { get; set; }

        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public StateEnum State { get; set; }
        public DateTime AdddedToCart { get; set; }


    }
}
