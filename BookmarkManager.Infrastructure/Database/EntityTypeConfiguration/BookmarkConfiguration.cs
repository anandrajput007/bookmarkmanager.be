using BookmarkManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookmarkManager.Infrastructure.Database.EntityTypeConfiguration
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable("Bookmark");
            builder.HasKey(x => x.BookmarkId);
            builder.Property(x => x.BookmarkId).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Icon).HasMaxLength(100);
            builder.Property(x => x.IsFav).HasDefaultValue(false);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.CreatedBy).HasDefaultValue(1);
            
            builder.HasOne(x => x.Collection)
                .WithMany(x => x.Bookmarks)
                .HasForeignKey(x => x.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 