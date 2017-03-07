using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OtherWorldsGames.Models
{
    [Table("ShoppingCarts")]
    public class ShoppingCart
    {
        [Key]
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public ShoppingCart()
        {

        }

    }
}
