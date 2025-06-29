using BookmarkManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookmarkManager.Infrastructure.Database.EntityTypeConfiguration
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("Collection");
            builder.HasKey(x => x.CollectionId);
            builder.Property(x => x.CollectionId).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Icon).HasMaxLength(100);
            builder.Property(x => x.IsFav).HasDefaultValue(false);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.CreatedBy).HasDefaultValue(1);
        }
    }
} 