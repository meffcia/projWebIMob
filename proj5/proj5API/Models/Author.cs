namespace proj5API.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AuthorWriter> AuthorWriters { get; set; } = new List<AuthorWriter>();
    }
}
