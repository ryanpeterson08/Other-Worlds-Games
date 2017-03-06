using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtherWorldsGames.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OtherWorldsGames.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;

        public IActionResult Index()
        {
            return View(_db.Products.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile image, Product product)
        {
            var pictureArray = new byte[0];
            if (image.Length > 0)
            {
                using (var fileStream = image.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    pictureArray = ms.ToArray();
                }
                product.Image = pictureArray;
                _db.Add(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
