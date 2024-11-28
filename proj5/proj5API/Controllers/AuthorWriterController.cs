using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proj5API.Data;
using proj5API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj5API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorWriterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorWriterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/authorwriter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorWriter>>> GetAuthorWriters()
        {
            return await _context.AuthorWriters.ToListAsync();
        }

        // GET: api/authorwriter/{authorId}/{writerId}
        [HttpGet("{authorId}/{writerId}")]
        public async Task<ActionResult<AuthorWriter>> GetAuthorWriter(int authorId, int writerId)
        {
            var authorWriter = await _context.AuthorWriters
                .FirstOrDefaultAsync(aw => aw.AuthorId == authorId && aw.WriterId == writerId);

            if (authorWriter == null)
            {
                return NotFound();
            }

            return authorWriter;
        }

        // POST: api/authorwriter
        [HttpPost]
        public async Task<ActionResult<AuthorWriter>> PostAuthorWriter(AuthorWriter authorWriter)
        {
            // Sprawdzenie, czy autor istnieje
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == authorWriter.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Autor o podanym ID nie istnieje.");
            }

            // Sprawdzenie, czy pisarz istnieje
            var writerExists = await _context.Writers.AnyAsync(w => w.Id == authorWriter.WriterId);
            if (!writerExists)
            {
                return BadRequest("Pisarz o podanym ID nie istnieje.");
            }

            // Sprawdzenie, czy relacja już istnieje
            var existingAuthorWriter = await _context.AuthorWriters
                .FirstOrDefaultAsync(aw => aw.AuthorId == authorWriter.AuthorId && aw.WriterId == authorWriter.WriterId);

            if (existingAuthorWriter != null)
            {
                return BadRequest("Relacja już istnieje.");
            }

            _context.AuthorWriters.Add(authorWriter);
            await _context.SaveChangesAsync();

            // Zwracamy status 201 i lokalizację nowego zasobu
            return CreatedAtAction(nameof(GetAuthorWriter), new { authorId = authorWriter.AuthorId, writerId = authorWriter.WriterId }, authorWriter);
        }


        // [HttpPost]
        // public async Task<ActionResult<IEnumerable<AuthorWriter>>> PostAuthorWriter(int authorId, [FromBody] List<int> writerIds)
        // {
        //     // Sprawdzenie, czy lista pisarzy nie jest pusta
        //     if (writerIds == null || !writerIds.Any())
        //     {
        //         return BadRequest("Lista pisarzy nie może być pusta.");
        //     }

        //     // Lista do przechowywania nowych relacji
        //     var authorWriters = new List<AuthorWriter>();

        //     foreach (var writerId in writerIds)
        //     {
        //         // Sprawdzenie, czy relacja już istnieje
        //         var existingAuthorWriter = await _context.AuthorWriters
        //             .FirstOrDefaultAsync(aw => aw.AuthorId == authorId && aw.WriterId == writerId);

        //         if (existingAuthorWriter == null)
        //         {
        //             // Dodanie nowej relacji
        //             authorWriters.Add(new AuthorWriter { AuthorId = authorId, WriterId = writerId });
        //         }
        //     }

        //     // Dodanie nowych relacji do bazy danych
        //     if (authorWriters.Any())
        //     {
        //         _context.AuthorWriters.AddRange(authorWriters);
        //         await _context.SaveChangesAsync();
        //     }

        //     return CreatedAtAction(nameof(GetAuthorWriter), new { authorId = authorId, writerIds = writerIds }, authorWriters);
        // }

        // DELETE: api/authorwriter/{authorId}/{writerId}
        [HttpDelete("{authorId}/{writerId}")]
        public async Task<IActionResult> DeleteAuthorWriter(int authorId, int writerId)
        {
            var authorWriter = await _context.AuthorWriters
                .FirstOrDefaultAsync(aw => aw.AuthorId == authorId && aw.WriterId == writerId);

            if (authorWriter == null)
            {
                return NotFound();
            }

            _context.AuthorWriters.Remove(authorWriter);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
