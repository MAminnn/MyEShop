using Microsoft.AspNetCore.Mvc;
using My_EShop_2.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_EShop_2
{
    public class EditCategoryComponent:ViewComponent
    {
        private CategoryRepository _cr;
        public EditCategoryComponent(CategoryRepository cr)
        {
            _cr = cr;
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryid)
        {
            var category =_cr.GetCategoryById(categoryid);
            return View("_EditCategoryComponent.cshtml", category);
        }
    }
}
