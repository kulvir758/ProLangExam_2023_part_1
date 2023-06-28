using Microsoft.EntityFrameworkCore;

namespace University.DataAccess;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>().HasKey(x => x.Id);
        modelBuilder.Entity<Skill>().HasKey(x => x.Id);
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Skill> Skills { get; set; }
}

