using BookmarkManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookmarkManager.Infrastructure.Database
{
    public class BMDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BMDbContext(DbContextOptions<BMDbContext> options) : base(options)
        {
        }

        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Collection> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BMDbContext).Assembly);
        }
    }
} 