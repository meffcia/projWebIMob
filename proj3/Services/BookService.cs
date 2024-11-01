using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using proj3.Models;

namespace proj3.Services
{
    public class BookService
    {
        private readonly string _filePath = "Data/books.json";

        public List<BookModel> GetBooks()
        {
            if (!File.Exists(_filePath)) return new List<BookModel>();
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<BookModel>>(json);
        }

        public void AddBook(BookModel book)
        {
            var books = GetBooks();
            books.Add(book);
            SaveBooks(books);
        }

        private void SaveBooks(IEnumerable<BookModel> books)
        {
            var json = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
