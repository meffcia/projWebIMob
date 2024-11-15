namespace proj4.Models
{
    public class Book : IProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
    }

    // public class Movie : IProduct
    // {
    //     public int Id { get; set; }
    //     public string Title { get; set; }
    //     public string Genre { get; set; }
    //     public decimal Price { get; set; }
    //     public string Director { get; set; }
    //     public int DurationMinutes { get; set; }
    // }
}
