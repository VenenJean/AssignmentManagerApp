using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext {
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Assignment> Assignments { get; set; } = null!;
  
  public AppDbContext() { }
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  
  private const string ConnectionString = 
    $"server=localhost;" +
    $"port=3306;" +
    $"user=root;" +
    $"password=Antidote2580134679#;" +
    $"database=TaskManagerApp;";

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    optionsBuilder.UseMySql(
      ConnectionString,
      new MariaDbServerVersion(new Version(11, 2, 2))
    );
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}