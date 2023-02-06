using ClassIbay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IbayApi.Services
{
    public class IbayContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public IbayContext(DbContextOptions<IbayContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-A1LDGC95\SQLEXPRESS01;Initial Catalog=Ibay;Integrated Security=True;Trust Server Certificate=True ");

    }
}
