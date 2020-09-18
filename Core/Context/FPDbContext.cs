using System.Data;
using FoodPortal.API.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace FoodPortal.API.Core.Context{
    public class FPDbContext:DbContext
    {
        public FPDbContext(DbContextOptions<FPDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Customer> Customers{get;set;}
    }
}