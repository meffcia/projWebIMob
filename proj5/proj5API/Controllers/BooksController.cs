using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proj5API.Data;
using proj5.Domain.Models;

namespace proj5API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _context.Books
            .Include(b => b.Review) // Wczytanie powiązanej recenzji
            .ToListAsync();
        return books;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        // Sprawdzanie czy autor istnieje
        if (!_context.Authors.Any(a => a.Id == book.AuthorId))
        {
            return BadRequest("The specified author does not exist.");
        }

        // Tworzymy książkę
        _context.Books.Add(book);
        await _context.SaveChangesAsync(); // Zapisujemy książkę, aby mieć przypisany jej Id

        // Tworzymy recenzję i przypisujemy BookId
        if (book.Review != null)
        {
            book.Review.BookId = book.Id; // Teraz BookId jest ustawione po zapisaniu książki
            _context.Reviews.Add(book.Review); // Dodajemy recenzję do kontekstu
            await _context.SaveChangesAsync(); // Zapisujemy recenzję
        }

        // Zwracamy utworzoną książkę z przypisaną recenzją
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id) return BadRequest();

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Books.Any(e => e.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
