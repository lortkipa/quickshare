using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configurations
{
    public class LinkFileConfiguration : IEntityTypeConfiguration<LinkFile>
    {
        public void Configure(EntityTypeBuilder<LinkFile> builder)
        {
            builder.ToTable("LinkFiles")
                .HasKey(f => f.Id);

            builder.Property(f => f.LinkId)
                .IsRequired();
            builder.Property(f => f.Path)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(f => f.Link)
                .WithMany(l => l.Files)
                .HasForeignKey(f => f.LinkId);
        }
    }
}
