using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My_EShop_2.Repositories.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_EShop_2;
using My_EShop_2.Models.Context;
using My_EShop_2.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Components.Forms;

namespace My_EShop_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private ProductRepository _pr;


        private MyEShop_DB _context;

        public ProductsController(ProductRepository pr,MyEShop_DB context)
        {
            _pr = pr;
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _pr.GetAllProducts());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pr.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoriesList = new SelectList(_context.Categories, "CategoryId", "Title");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Title, int CategoryId, int Price, int QuantityInStock, IFormFile ProductImage)
        {
            if (ProductImage==null)
            {
                Product returnproduct = new Product()
                {
                    CategoryId = CategoryId,
                    Price = Price,
                    QuantityInStock = QuantityInStock,
                    Title = Title
                };
                ModelState.AddModelError("ImageExtension", "تصویر محصول اجباریست");
                ViewBag.CategoriesList = new SelectList(_context.Categories, "CategoryId", "Title");
                return View(returnproduct);
            }
            Product product = new Product()
            {
                CategoryId = CategoryId, 
                ImageExtension = Path.GetExtension(ProductImage.FileName),
                Price = Price,
                QuantityInStock = QuantityInStock,
                Title = Title
            };

            if (ModelState.IsValid)
            {
                product.ImageExtension = Path.GetExtension(ProductImage.FileName);
                await _pr.InsertProduct(product);
                using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory()+"/wwwroot/"+"/Images/"+"/ProductImages/",product.ProductId.ToString()+product.ImageExtension),FileMode.CreateNew))
                {
                    await ProductImage.CopyToAsync(stream);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
            //return View();
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.CategoriesList = new SelectList(_context.Categories, "CategoryId", "Title");
            var product = await _pr.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,string ImageExtension,string Title, int CategoryId, int Price, int QuantityInStock, IFormFile ProductImage)
        {
            var product = new Product()
            {
                CategoryId = CategoryId,
                Price = Price,
                ProductId = id,
                QuantityInStock = QuantityInStock,
                Title = Title
            };
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pr.EditProduct(product);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Images","ProductImages", product.ProductId.ToString() + product.ImageExtension);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    
                    try
                    {
                        string path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "ProductImages", product.ProductId + Path.GetExtension(ProductImage.FileName));
                        using (FileStream s = new FileStream(path2, FileMode.Create))
                        {
                            ProductImage.CopyTo(s);
                        }
                        product.ImageExtension = Path.GetExtension(ProductImage.FileName);
                    }
                    catch (NullReferenceException)
                    {
                        product.ImageExtension = ImageExtension;
                    }   
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pr.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _pr.GetProductById(id);
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images","ProductImages",product.ProductId.ToString() + product.ImageExtension);
                System.IO.File.Delete(path);
            }
            catch (NullReferenceException)
            {
                
            }
            await _pr.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            if (_pr.GetProductById(id)!=null)
            {
                return true;
            }
            return false;
        }
    }
}
