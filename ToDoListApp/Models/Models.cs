using System.Collections.Generic;
using System.Reflection.Emit;

namespace ToDoListApp.Models
{
    //Project
    public class ToDoProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPaused { get; set; }
        public List<ToDoTask> Tasks { get; set; } = new();
        public List<ToDoTag> Tags { get; set; } = new();
        public int ProjectId { get; set; }
    }
    //Task
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public List<ToDoTag> Tags { get; set; } = new();
        public int ProjectId { get; set; }
    }
    //Tag
    public class ToDoTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ToDoTask> Tasks { get; set; } = new();
        public List<ToDoProject> Projects { get; set; } = new();
        public int ProjectId { get; set; }
        }
    }
