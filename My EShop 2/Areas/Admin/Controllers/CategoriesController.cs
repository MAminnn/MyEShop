using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_EShop_2.Models.Context;
using My_EShop_2.Repositories;
using My_EShop_2.Repositories.Services;

namespace My_EShop_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes =CookieAuthenticationDefaults.AuthenticationScheme,Roles ="Admin")]
    public class CategoriesController : Controller
    {
        private MyEShop_DB _db;
        private CategoryRepository _cr;
        public CategoriesController(MyEShop_DB db, CategoryRepository cr)
        {
            _db = db;
            _cr = cr;
        }
        public IActionResult Index()
        {
            var categories = _cr.GetAllCategories();
            return View(categories);
        }
        [HttpPost]
        public JsonResult Create(string title)
        {
            Category category = new Category()
            {
                Title = title
            };
            _cr.InsertCategory(category);
            var res = new
            {
                title = title,
                id = category.CategoryId
            };
            return Json(res);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            string categorytitle = _cr.GetCategoryById(Id).Title;
            object res =new {Id=Id,title=categorytitle};
            _cr.DeleteCategory(Id);
            return Json(res);
        }
        [HttpPost]
        public JsonResult Edit(int id,string title)
        {
            var category = _cr.GetCategoryById(id);
            category.Title = title;
            _cr.EditCategory(category);
            return Json(title);
        }
    }
}