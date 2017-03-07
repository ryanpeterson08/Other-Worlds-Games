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



namespace OtherWorldsGames.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        ShoppingCart newShoppingCart = new ShoppingCart();
        
        [HttpPost]
        public IActionResult AddToCart(int ProductId)
        {

            newShoppingCart.ShoppingCartId = GetCartId();
            Debug.WriteLine("shopping id " + newShoppingCart.ShoppingCartId);
            //_db.ShoppingCarts.Add(newShoppingCart);
            //_db.SaveChanges();

            var cartItem = _db.CartItems.SingleOrDefault(c => c.ShoppingCart.ShoppingCartId == newShoppingCart.ShoppingCartId && c.Product.ProductId == ProductId);
                      
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    CartId = newShoppingCart.ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(p => p.ProductId == ProductId),
                    Quantity = 1,
                    DateCreated = DateTime.Now,
                    ShoppingCart = _db.ShoppingCarts.SingleOrDefault(s => s.ShoppingCartId == newShoppingCart.ShoppingCartId)
                    
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {               
                cartItem.Quantity++;
            }
            _db.SaveChanges();
            return RedirectToAction("UserShoppingCart", "Cart");
             
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

        public List<CartItem> GetCartItems()
        {
           
            var items = _db.CartItems.Include(c => c.ShoppingCart).Where(c => c.ShoppingCart.ShoppingCartId == newShoppingCart.ShoppingCartId).ToList();
            return items;
        }

        public IActionResult UserShoppingCart()
        {           
            return View(GetCartItems());
        }
    }

}
