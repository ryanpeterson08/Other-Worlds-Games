using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OtherWorldsGames.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public byte[] Image { get; set; }
        public virtual Category Category { get; set; }

    }

}
