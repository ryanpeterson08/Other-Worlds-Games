using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OtherWorldsGames.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set;}
        public virtual ICollection<Product> Products { get; set; }
    }
}
