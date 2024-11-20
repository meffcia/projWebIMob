using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IProductService
    {
        Task<List<IProduct>> GetAllProductAsync();
        Task<IProduct> AddProductAsync(IProduct product);
        Task<IProduct> UpdateProductAsync(IProduct product);
        Task<bool> DeleteProductAsync(int id);
    }
}
