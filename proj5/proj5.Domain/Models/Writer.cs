namespace proj5.Domain.Models
{
    public class Writer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<AuthorWriter> AuthorWriters { get; set; } = new List<AuthorWriter>();
    }
}
