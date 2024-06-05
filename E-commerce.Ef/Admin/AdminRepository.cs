using E_commerce.Domain.Admin;
using E_commerce.Ef.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Ef.Adminrepository
{
    public class AdminRepository : IAdminRepository
    {
        private UserDbContext UserDbContext;

        public AdminRepository(UserDbContext userDb)
        {
            UserDbContext = userDb;
        }

        public async Task<AddProduct> AddProduct(AddProduct product)
        {
            await UserDbContext.AddAsync(product);
            await UserDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<AddProduct> addToCart(int id)
                {
           var addtocartdata =  await UserDbContext.AddProduct.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            return addtocartdata;
        }

        public async Task<List<AddProduct>> GetAllProducts()
            {
            var alllproduct = await UserDbContext.AddProduct.AsNoTracking().ToListAsync();
            return alllproduct;
        }
    }
}
