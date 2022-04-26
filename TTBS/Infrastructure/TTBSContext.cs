﻿using Microsoft.EntityFrameworkCore;
using TTBS.Core.Entities;

namespace TTBS.Infrastructure
{
    public partial class TTBSContext : DbContext
    {
        public static readonly LoggerFactory _myLoggerFactory = new LoggerFactory(new[] {
                                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
                        });
        public TTBSContext() { }
        public TTBSContext(DbContextOptions<TTBSContext> options) : base(options)
        {
          
        }

        public DbSet<DonemEntity> Donems { get; set; }

        public DbSet<StenoPlan> StenoPlans { get; set; }

        public DbSet<ClaimEntity> Claims { get; set; }
        public DbSet<RoleClaimEntity> RoleClaims { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyBaseEntityConfiguration();

            builder.Entity<DonemEntity>(ConfigureDonem);

            builder.Entity<StenoPlan>(ConfigureStenoPlan);            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory)  //tie-up DbContext with LoggerFactory object
                .EnableSensitiveDataLogging();
        }
    }
}
