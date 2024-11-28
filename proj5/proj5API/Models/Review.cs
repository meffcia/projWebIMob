namespace proj5API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
