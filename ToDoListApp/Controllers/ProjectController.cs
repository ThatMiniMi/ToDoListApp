using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private static List<ToDoProject> Projects = new List<ToDoProject>();
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            return Ok(Projects);
        }
        [HttpGet("{id}")]
        public IActionResult GetProject(int id)
        {
            var project = Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }
        [HttpPost]
        public IActionResult CreateProject([FromBody] ToDoProject project)
        {
            project.Id = Projects.Count + 1;
            Projects.Add(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ToDoProject updatedProject)
        {
            var project = Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();
            project.Name = updatedProject.Name;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();
            Projects.Remove(project);
            return NoContent();
        }
    }
}
