using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My_EShop_2.Models.Context;

namespace My_EShop_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private MyEShop_DB _context;
        public HomeController(MyEShop_DB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Categories = _context.Categories.ToList();
            return View(Categories);
        }
    }
}