using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ToDoContext _context;

        public TaskController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Tags)
                .ToListAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ToDoTask task)
        {
            if (task == null)
                return BadRequest();

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ToDoTask updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Name = updatedTask.Name;
            task.Deadline = updatedTask.Deadline;

            if (updatedTask.Tags != null)
            {
                task.Tags = updatedTask.Tags;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

