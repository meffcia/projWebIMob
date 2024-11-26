namespace proj4.Models
{
    public class Book : IProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
    }

  
}
