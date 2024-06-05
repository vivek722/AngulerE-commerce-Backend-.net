using AngulerE_commerce.Models;
using AutoMapper;
using E_commerce.Domain.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngulerE_commerce.Controllers
{
    public class AddProductController : Controller
    {
        private readonly AdminService adminService;
        private readonly  IMapper mapper ;
        private readonly IWebHostEnvironment env ;

        public AddProductController(AdminService admin,IMapper mapper,IWebHostEnvironment webHost)
        {
            adminService = admin;
            this.mapper = mapper;
            this.env = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> addproduct([FromBody]AddProductDto product)
            {
           var productmapper = mapper.Map<AddProduct>(product);
           mapper.Map<AddProductDto>(await adminService.AddProduct(productmapper));
           return Ok(product);
        }
    
        [HttpGet]
        public async Task<IActionResult> getAllProduct()
        {
            var totalproduct = await adminService.GetAllProducts();
            var totalproductmap = mapper.Map<List<AddProductDto>>(totalproduct);
            return Json(totalproductmap);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getTotalProduct()
        {
            var totalproduct = await adminService.GetAllProducts();
            var totalproducts = totalproduct.Count();
            return Json(totalproducts);
        }
  
        [HttpGet]
        public async Task<IActionResult> addtocart(int id)
                    {
            var totalproduct = await adminService.addToCart(id);
            var totalproductmap = mapper.Map<AddProductDto>(totalproduct);
            return Json(totalproductmap);
        }
    }
}
