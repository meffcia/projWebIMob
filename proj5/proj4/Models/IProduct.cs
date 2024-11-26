namespace proj4.Models
{
    public interface IProduct
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        decimal Price { get; set; }
    }
}