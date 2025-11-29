using BookTrack.Core.Entitites;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Infra.Persistence;

public class BookTrackDbContext : DbContext
{
    public BookTrackDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);
    }
}