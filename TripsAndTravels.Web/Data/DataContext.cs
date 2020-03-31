using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TripsAndTravels.Web.Data.Entities
{
    public class DataContext : IdentityDbContext<UserEntity>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<TripDetailsEntity> TripDetails { get; set; }
        public DbSet<ExpensesEntity> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TripEntity>()
                .HasIndex(t => t.IdTrip)
                .IsUnique();
        }

    }

}