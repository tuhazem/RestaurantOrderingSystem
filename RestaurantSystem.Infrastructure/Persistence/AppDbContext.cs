using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.Domain.Entities;

namespace RestaurantSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options){ }


        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
       

    }
}
