using Microsoft.EntityFrameworkCore;
using ORM_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_CRUD
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6KFQ39N;Database=ProductsCA;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
    }
}
