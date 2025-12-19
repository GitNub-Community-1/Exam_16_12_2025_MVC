using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<User, IdentityRole<long>, long>(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);
    }


}