using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoProject> Projects { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<ToDoTag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define many-to-many relationship between ToDoTask and ToDoTag
            modelBuilder.Entity<ToDoTask>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tasks)
                .UsingEntity(j => j.ToTable("TaskTags")); // optional, specify table name for many-to-many relationship

            // Define many-to-many relationship between ToDoProject and ToDoTag
            modelBuilder.Entity<ToDoProject>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Projects)
                .UsingEntity(j => j.ToTable("ProjectTags")); // optional, specify table name for many-to-many relationship

            // Define one-to-many relationship between ToDoProject and ToDoTask
            modelBuilder.Entity<ToDoProject>()
                .HasMany(p => p.Tasks)
                .WithOne()  // No navigation property in ToDoTask
                .HasForeignKey(t => t.ProjectId); // Add ProjectId to ToDoTask (not shown in your model but required)
        }
    }
}

