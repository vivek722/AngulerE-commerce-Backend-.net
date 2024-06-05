using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Admin
{
    public interface IAdminRepository
    {
        Task<AddProduct> AddProduct(AddProduct product);
        Task<List<AddProduct>> GetAllProducts();

        Task<AddProduct> addToCart(int id);
    }
}
