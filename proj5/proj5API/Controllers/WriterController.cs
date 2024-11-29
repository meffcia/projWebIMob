using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proj5API.Data;
using proj5.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace proj5API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WritersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/writers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Writer>>> GetWriters()
        {
            return await _context.Writers.ToListAsync();
        }

        // GET: api/writers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Writer>> GetWriter(int id)
        {
            var writer = await _context.Writers.FindAsync(id);

            if (writer == null)
            {
                return NotFound();
            }

            return writer;
        }

        // PUT: api/writers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWriter(int id, Writer writer)
        {
            if (id != writer.Id)
            {
                return BadRequest();
            }

            _context.Entry(writer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WriterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/writers
        [HttpPost]
        public async Task<ActionResult<Writer>> PostWriter(Writer writer)
        {
            _context.Writers.Add(writer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWriter), new { id = writer.Id }, writer);
        }

        // DELETE: api/writers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWriter(int id)
        {
            var writer = await _context.Writers.FindAsync(id);
            if (writer == null)
            {
                return NotFound();
            }

            _context.Writers.Remove(writer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WriterExists(int id)
        {
            return _context.Writers.Any(e => e.Id == id);
        }
    }
}
