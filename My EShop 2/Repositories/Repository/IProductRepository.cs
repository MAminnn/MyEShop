using My_EShop_2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace My_EShop_2.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();

        Task<List<Product>> GetProductsByGroup(int groupid);

        Task<Product> GetProductById(int id);

        Task InsertProduct(Product product);

        Task EditProduct(Product product);

        Task DeleteProduct(int productid);

    }
}
