using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Shared.Configuration
{
    public class AppSettings
    {
        public AppSettings()
        {
            ProductEndpoint = new ProductEndpoint();

        }
        public string BaseApiUrl { get; set; }

        public string ApiUrl { get; set; }

        public ProductEndpoint ProductEndpoint { get; set; }
    }
}
