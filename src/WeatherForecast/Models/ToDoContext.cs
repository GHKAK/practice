using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace TodoApi.Models;

public class TodoContext : DbContext {
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<User> Users { get; set; }

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options) {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TodoItem>()
           .HasOne(s => s.User)
           .WithMany(g => g.TodoItems)
           .HasForeignKey(s => s.UserId);
    }
}