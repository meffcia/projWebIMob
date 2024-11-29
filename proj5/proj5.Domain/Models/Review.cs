namespace proj5.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
    }
}
