namespace proj5API.Models
{
    public class AuthorWriter
    {
        public int AuthorId { get; set; }
        public int WriterId { get; set; }

        public Author Author { get; set; }
        public Writer Writer { get; set; }
    }
}
