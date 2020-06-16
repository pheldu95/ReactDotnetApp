using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }


        //Values will be the table name
        //we need to add this as a service to the app.
        public DbSet<Value> Values { get; set; }

        //new dbset for the migration. table with rows of type activity
        //from Domain namespace
        public DbSet<Activity> Activities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Value>()
                .HasData(
                    new Value {Id = 1, Name = "Value 101"},
                    new Value { Id = 2, Name = "Value 102" },
                    new Value { Id = 3, Name = "Value 103" }

                );
            
        }
    }
}
