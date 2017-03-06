using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtherWorldsGames.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OtherWorldsGames.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _db.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
    }
}
