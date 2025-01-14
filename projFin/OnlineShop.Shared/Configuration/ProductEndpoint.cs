using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Shared.Configuration
{
    public class ProductEndpoint
    {
        public string BaseUrl { get; set; }
        public string GetProducts { get; set; }
        public string GetProduct { get; set; }
        public string CreateProduct { get; set; }
        public string UpdateProduct { get; set; }

        public string DeleteProduct { get; set; }
        public string SearchProduct { get; set; }
    }
}
