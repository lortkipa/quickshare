using Data.Configurations;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ProjectDBContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<LinkFile> LinkFiles { get; set; }

        public ProjectDBContext(DbContextOptions<ProjectDBContext> context) : base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LinkConfiguration());
            modelBuilder.ApplyConfiguration(new LinkFileConfiguration());
        }
    }
}
