﻿using System.Data.Entity;

namespace EFStudiiCaz.StudiuCaz2
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductContext() : base("name=EFStudiiCaz")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Map(m =>
                {
                    m.Properties(p => new { p.SKU, p.Description, p.Price });
                    m.ToTable("Product", "BazaDeDate");
                })
                .Map(m =>
                {
                    m.Properties(p => new { p.SKU, p.ImageURL });
                    m.ToTable("ProductWebInfo", "BazaDeDate");
                });
        }
    }
}