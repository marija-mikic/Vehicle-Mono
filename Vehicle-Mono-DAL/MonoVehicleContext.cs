using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Mono_DAL.Entity;

namespace Vehicle_Mono_DAL
{
    public class MonoVehicleContext : DbContext
    {
        public MonoVehicleContext(DbContextOptions<MonoVehicleContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MakeEntity> Makess { get; set; }
        public DbSet<ModelEntity> Models { get; set; }

    }
}
