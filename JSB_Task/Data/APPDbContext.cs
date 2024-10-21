using JSB_Task.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JSB_Task.Data
{
    public class APPDbContext : DbContext
    {
        public APPDbContext(DbContextOptions<APPDbContext> options) : base(options)
        {

        }


        DbSet<Order> Orders { get; set; }
        DbSet<Product> products { get; set; }
        DbSet<OrderProduct> OrderProduct { get; set; }
    }
}
