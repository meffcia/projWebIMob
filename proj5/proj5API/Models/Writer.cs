namespace proj5API.Models
{
    public class Writer
    {
        public int WriterId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<AuthorWriter> AuthorWriters { get; set; }
    }
}
