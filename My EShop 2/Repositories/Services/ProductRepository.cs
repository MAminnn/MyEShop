using Microsoft.EntityFrameworkCore;
using My_EShop_2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private MyEShop_DB _db;

        public ProductRepository(MyEShop_DB db)
        {
            _db = db;
        }
        public async Task DeleteProduct(int productid)
        {
            var product = await _db.Products.FindAsync(productid);
            _db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task EditProduct(Product product)
        {
            _db.Update<Product>(product);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _db.Products.Include(p=>p.Category).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Products.Include(p=>p.Category).SingleOrDefaultAsync(p=>p.ProductId==id);
        }

        public async Task<List<Product>> GetProductsByGroup(int groupid)
        {
            return await _db.Products.Include(p=>p.Category).Where(p=>p.Category.CategoryId==groupid).ToListAsync();
        }

        public async Task InsertProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            _db.SaveChanges();
        }
    }
}
