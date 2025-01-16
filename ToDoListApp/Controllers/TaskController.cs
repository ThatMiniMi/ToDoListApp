using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<ToDoTask> Tasks = new List<ToDoTask>();
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(Tasks);
        }
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }
        [HttpPost]
        public IActionResult CreateTask([FromBody] ToDoTask task)
        {
            if (task == null)
                return BadRequest();

            task.Id = Tasks.Count + 1;
            Tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] ToDoTask updatedTask)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            task.Name = updatedTask.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            Tasks.Remove(task);
            return NoContent();
        }
    }
}
