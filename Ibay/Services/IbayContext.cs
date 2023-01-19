using ClassIbai;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IbayApi.Services
{
    public class IbayContext : DbContext
    {
        public DbSet<Cart> Classrooms { get; set; }
        public DbSet<CartItem> People { get; set; }
        public DbSet<Product> Students { get; set; }
        public DbSet<User> Teachers { get; set; }

        public IbayContext(DbContextOptions<IbayContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=Ibay;Integrated Security=True;Trust Server Certificate=True ");

    }
}
