using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtherWorldsGames.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OtherWorldsGames.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;


        [HttpPost]
        public void AddToCart(int ProductId)
        {
            ShoppingCart newShoppingCart = new ShoppingCart();
            newShoppingCart.ShoppingCartId = GetCartId();

            Debug.WriteLine(newShoppingCart.ShoppingCartId);
            Debug.WriteLine(ProductId);
            
            var cartItem = _db.CartItems.SingleOrDefault(c => c.CartId == newShoppingCart.ShoppingCartId && c.Product.ProductId == ProductId);
            
            
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    CartId = newShoppingCart.ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(p => p.ProductId == ProductId),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _db.SaveChanges();
             
        }



        //Gets cart id for user
        public string GetCartId()
        {
            ISession current = HttpContext.Session;
            if(current.Id == null)
            {
                if(!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                    SessionExtensions.SetString(current, ShoppingCart.CartSessionKey, HttpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    SessionExtensions.SetString(current, ShoppingCart.CartSessionKey, tempCartId.ToString());
                }
            }
            return current.Id;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
