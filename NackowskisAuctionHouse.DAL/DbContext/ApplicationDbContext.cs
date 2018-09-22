using NackowskisAuctionHouse.DAL.IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NackowskisAuctionHouse.DAL.ModelsEf;

namespace NackowskisAuctionHouse.DAL.DbContext
{
    public class NackowskisDBContext : IdentityDbContext<AppUser>
    {
        public NackowskisDBContext(DbContextOptions<NackowskisDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Regular", NormalizedName = "Regular".ToUpper() });
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = 1 , CategoryName = "Klockor" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Smycken" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Tavlor" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 4, CategoryName = "Ädelstenar" });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<AuctionCategory> AuctionCategories { get; set; }

    }
}
