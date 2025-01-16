using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private static List<ToDoTag> Tags = new List<ToDoTag>();
        [HttpGet]
        public IActionResult GetAllTags()
        {
            return Ok(Tags);
        }
        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            var tag = Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return NotFound();
            return Ok(tag);
        }
        [HttpPost]
        public IActionResult CreateTask([FromBody] ToDoTag tag)
        {
            if (tag == null)
                return BadRequest();

            tag.Id = Tags.Count + 1;
            Tags.Add(tag);
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTag(int id, [FromBody] ToDoTag updatedTag)
        {
            var tag = Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return NotFound();

            tag.Name = updatedTag.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var tag = Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return NotFound();

            Tags.Remove(tag);
            return NoContent();
        }
    }
}