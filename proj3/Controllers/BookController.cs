// Controllers/BookController.cs
using Microsoft.AspNetCore.Mvc;
using proj3.Models;
using Newtonsoft.Json;

namespace proj3.Controllers
{
    public class BookController : Controller
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "books.json");

        public IActionResult Index()
        {
            var books = GetBooks();
            return View(books);
        }

        [HttpGet("Book/sort/{sortOrder?}")]
        public IActionResult Index(string sortOrder, string? searchString = null, string? authorFilter = null, int? price = null)
        {
            var allBooks = GetBooks();
            var books = allBooks.AsQueryable();

            if (!string.IsNullOrEmpty(authorFilter))
            {
                books = books.Where(b => b.Author == authorFilter);
            }

            if (price.HasValue)
            {
                books = books.Where(b => b.Price == price.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Id.ToString().Contains(searchString));
            }

            books = sortOrder switch
            {
                "title_asc" => books.OrderBy(b => b.Title),
                "title_desc" => books.OrderByDescending(b => b.Title),
                "price_asc" => books.OrderBy(b => b.Price),
                "price_desc" => books.OrderByDescending(b => b.Price),
                _ => books.OrderBy(b => b.Id)
            };

            return View(books.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                var books = GetBooks();
                book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
                books.Add(book);
                Console.WriteLine("Book saved and redirecting to Index");
                SaveBooks(books);
                return RedirectToAction(nameof(Index));
            } else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var books = GetBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BookModel book)
        {
            if (ModelState.IsValid)
            {
                var books = GetBooks();
                var existingBook = books.FirstOrDefault(b => b.Id == id);
                if (existingBook == null) return NotFound();

                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;

                SaveBooks(books);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var books = GetBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            books.Remove(book);
            SaveBooks(books);
            return RedirectToAction(nameof(Index));
        }

        private List<BookModel> GetBooks()
        {
            if (!System.IO.File.Exists(_filePath))
                return new List<BookModel>();
            var jsonData = System.IO.File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<BookModel>>(jsonData) ?? new List<BookModel>();
        }

        private void SaveBooks(List<BookModel> books)
        {
            var jsonData = JsonConvert.SerializeObject(books, Formatting.Indented);
            System.IO.File.WriteAllText(_filePath, jsonData);
        }
    }
}
