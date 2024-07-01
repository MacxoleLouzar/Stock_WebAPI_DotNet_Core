using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
            builder.Entity<Portfolio>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.portfolios)
            .HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>()
                      .HasOne(p => p.Stock)
                      .WithMany(u => u.portfolios)
                      .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole{Name = "User", NormalizedName = "USER"},
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}