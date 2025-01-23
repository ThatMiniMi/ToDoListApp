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
            modelBuilder.Entity<ToDoTask>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tasks)
                .UsingEntity(j => j.ToTable("TaskTags"));
            modelBuilder.Entity<ToDoProject>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Projects)
                .UsingEntity(j => j.ToTable("ProjectTags"));
            modelBuilder.Entity<ToDoProject>()
                .HasMany(p => p.Tasks)
                .WithOne() 
                .HasForeignKey(t => t.ProjectId);
        }
    }
}

