using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ProjectController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Tags)
                .ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ToDoProject project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ToDoProject updatedProject)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            project.Name = updatedProject.Name;
            project.IsPaused = updatedProject.IsPaused;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

