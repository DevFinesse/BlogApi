using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);


            modelBuilder.Entity<Comment>()
              .HasOne(c => c.ParentComment)
              .WithMany(p => p.Replies)
              .HasForeignKey(c  => c.ParentCommentId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.PostId);

            modelBuilder.Entity<Comment>()
                .HasIndex(c => c.ParentCommentId);

            modelBuilder.Entity<Comment>()
               .HasIndex(c => c.CreatedAt);

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Post>()
                .Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(200);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
