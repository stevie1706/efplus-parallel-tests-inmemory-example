using Microsoft.EntityFrameworkCore;

namespace MergeInMemory.Tests.Setup;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
    {
        
    }
    public string? DatabaseNameForTestingOnly { get; set; }
    
    public DbSet<MyEntity> MyEntities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyEntity>().HasKey(i => i.Id);
        base.OnModelCreating(modelBuilder);
    }

}