namespace ToDoListApp.Models
{
    //Project
    public class ToDoProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //Task
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //Tag
    public class ToDoTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
