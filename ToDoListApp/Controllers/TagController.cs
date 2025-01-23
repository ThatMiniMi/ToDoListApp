using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ToDoContext _context;

        public TagController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _context.Tags.ToListAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] ToDoTag tag)
        {
            if (tag == null)
                return BadRequest();

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] ToDoTag updatedTag)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            tag.Name = updatedTag.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
