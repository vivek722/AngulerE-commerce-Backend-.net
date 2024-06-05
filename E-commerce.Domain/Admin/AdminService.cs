using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository repository)
        {
            _adminRepository = repository;
        }
        public Task<AddProduct> AddProduct(AddProduct product)
        {
            return _adminRepository.AddProduct(product);    
        }

        public Task<AddProduct> addToCart(int id)
        {
            return _adminRepository.addToCart(id);
        }

        public async Task<List<AddProduct>> GetAllProducts()
        {
            return await _adminRepository.GetAllProducts();
        }
    }
}
