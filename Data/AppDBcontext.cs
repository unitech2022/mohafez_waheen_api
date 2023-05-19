using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mohafezApi.Models;

namespace mohafezApi.Data
{
    public class AppDBcontext : IdentityDbContext<User>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options)
        {


            
        }
      

      public DbSet<Teacher>? Teachers { get; set; }

     

        public DbSet<Table>? Tables { get; set; }

        // public DbSet<Category>? Categories { get; set; }

        // public DbSet<Field>? Fields { get; set; }

        // public DbSet<Offer>? Offers { get; set; }

        // public DbSet<Order>? Orders { get; set; }

        // public DbSet<OrderItem>? OrderItems { get; set; }

        // public DbSet<OrderItemOption>? OrderItemOptions { get; set; }

        // public DbSet<Product>? Products { get; set; }

        // public DbSet<ProductsOption>? ProductsOptions { get; set; }

        //  public DbSet<Market>? Markets { get; set; }

        //   public DbSet<Rate>? Rates { get; set; }

        //  public DbSet<Coupon>? Coupons { get; set; }
    
    
    
    }
}