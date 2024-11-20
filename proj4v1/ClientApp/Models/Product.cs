using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    public interface IProduct
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        decimal Price { get; set; }
    }
}
