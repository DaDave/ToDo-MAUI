using BACKEND.Models;
using Microsoft.EntityFrameworkCore;

namespace BACKEND;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
    }
    public DbSet<ToDo> ToDo { get; set; }
}