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
using Microsoft.AspNetCore.Authorization;

namespace OtherWorldsGames.Controllers
{
    
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        ShoppingCart newShoppingCart = new ShoppingCart();
        
        public CartController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
       

        //public ISession SessionId
        //{
        //    get { return HttpContext.Session; }
        //}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int ProductId)
        {
            var currentUser = await _userManager.GetUserAsync(User);    
            string user = await _userManager.GetUserIdAsync(currentUser);//new

            ISession current = HttpContext.Session;
            newShoppingCart.ShoppingCartId = Context.GetCartId(current, user); 
            
            _db.ShoppingCarts.Add(newShoppingCart);
            _db.SaveChanges();

            var cartItem = _db.CartItems.SingleOrDefault(c => c.CartId == newShoppingCart.ShoppingCartId && c.Product.ProductId == ProductId);
                      
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    CartId = newShoppingCart.ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(p => p.ProductId == ProductId),
                    Quantity = 1,
                    DateCreated = DateTime.Now,
                    ShoppingCart = _db.ShoppingCarts.FirstOrDefault(s => s.ShoppingCartId == newShoppingCart.ShoppingCartId)
                    
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {            
                cartItem.Quantity++;
                _db.Entry(cartItem).State = EntityState.Modified;
            }
            _db.SaveChanges();
            return RedirectToAction("UserShoppingCart", "Cart");
             
        }

        //public string GetCartId()
        //{
            
        //   /*HttpContext.Session;*/
        //    if ( Context.GetSessionId() == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
        //        {
        //            SessionExtensions.SetString(Context.Current.Session, ShoppingCart.CartSessionKey, HttpContext.User.Identity.Name);
        //        }
        //        else
        //        {
        //            Guid tempCartId = Guid.NewGuid();
        //            SessionExtensions.SetString(Context.Current.Session, ShoppingCart.CartSessionKey, tempCartId.ToString());
        //        }
        //    }
        //    return Context.GetSessionId();
        //}

        public IActionResult Index()
        {
            return View();
        }

        public List<CartItem> GetCartItems(string userId)
        {
            var items = _db.CartItems.Include(c => c.Product).Where(c => c.CartId == userId).ToList();
            //var items = _db.CartItems.Include(c => c.ShoppingCart)
            //    .Include(c => c.Product)
            //    .Where(c => c.ShoppingCart.ShoppingCartId == userId).ToList();
            //var items = _db.CartItems.Include(c => c.Product).ToList();
            return items;
        }

        public async Task<IActionResult> UserShoppingCart()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            string user = await _userManager.GetUserIdAsync(currentUser);
            return View(GetCartItems(user));
        }
    }

}
