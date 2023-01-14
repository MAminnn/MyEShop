using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();

        Category GetCategoryById(int id);

        void InsertCategory(Category category);

        void EditCategory(Category category);

        void DeleteCategory(int categoryid);
    }
}
