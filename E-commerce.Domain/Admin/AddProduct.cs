using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Admin
{
    public class AddProduct
    {
        public int Id { get; set; }
        public string?  ProductName { get; set; }
        public string? ProductDesc { get; set; }
        public int? ProductActualPrice { get; set; }
        public int? ProductOfferPrice { get; set; }
        public string? ProductSize { get; set; }
        public string? ProductColour { get; set; }
        public string? ProductImage { get; set; }
        public int? productqty { get; set; } = 1;
        public int? totalprice { get; set; } = 0;
    }
 }
