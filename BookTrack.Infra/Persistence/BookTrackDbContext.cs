using BookTrack.Core.Entitites;
using Microsoft.EntityFrameworkCore;

namespace BookTrack.Infra.Persistence;

public class BookTrackDbContext : DbContext
{
    public BookTrackDbContext(DbContextOptions<BookTrackDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }  
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);
        
        modelBuilder.Entity<User>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Review>(e =>
        {
            e.HasKey(r => r.Id);
            e.HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.IdBook)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });
  
        
        base.OnModelCreating(modelBuilder);
    }
}