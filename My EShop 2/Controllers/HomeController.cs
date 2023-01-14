using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My_EShop_2.Models;
using My_EShop_2.Repositories;

namespace My_EShop_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ProductRepository _pr;
        public HomeController(ILogger<HomeController> logger,ProductRepository pr)
        {
            _logger = logger;
            _pr = pr;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _pr.GetAllProducts();
            return View(products);

        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _pr.GetProductById(id);
            return View(product);
        }
    }
}
