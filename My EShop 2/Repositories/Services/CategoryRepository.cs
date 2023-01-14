using My_EShop_2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2.Repositories.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private MyEShop_DB _db;

        public CategoryRepository(MyEShop_DB db)
        {
            _db = db;
        }
        public void DeleteCategory(int categoryid)
        {
            var category = _db.Find<Category>(categoryid);
            _db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            _db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Categories;
        }

        public Category GetCategoryById(int id)
        {
           return _db.Categories.Find(id);
        }

        public void InsertCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }
    }
}
