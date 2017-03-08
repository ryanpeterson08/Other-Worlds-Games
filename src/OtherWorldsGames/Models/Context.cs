using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace OtherWorldsGames.Models
{
    public class Context
    {
        
        public static HttpContext current;

        public static string GetCartId(ISession sesh, string userId)
        {
            Debug.WriteLine("your id " + userId);
            //ISession current = Current.Session;
            if (sesh.Id != userId)
            {
                if (!string.IsNullOrWhiteSpace(userId /*current.User.Identity.Name*/))
                {
                    SessionExtensions.SetString(sesh, ShoppingCart.CartSessionKey, userId /*current.User.Identity.Name*/);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    SessionExtensions.SetString(sesh, ShoppingCart.CartSessionKey, tempCartId.ToString());
                }
            }
            Debug.WriteLine("sesh id " + sesh.Id);
            return sesh.Id;
        }
    }
}
