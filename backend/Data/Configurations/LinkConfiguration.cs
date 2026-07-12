using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.ToTable("Links")
                .HasKey(l => l.Id);

            builder.Property(l => l.Slug)
                .IsRequired()
                .HasMaxLength(6);
            builder.HasIndex(l => l.Slug)
                .IsUnique();
            builder.Property(l => l.Expired)
                .IsRequired();

            builder.HasMany(l => l.Files)
                .WithOne(f => f.Link)
                .HasForeignKey(f => f.LinkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
