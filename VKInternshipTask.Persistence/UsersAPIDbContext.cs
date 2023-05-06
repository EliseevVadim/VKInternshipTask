﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Persistence
{
    public class UsersAPIDbContext : DbContext, IUsersAPIDbContext
    {
        public UsersAPIDbContext()
            :base() { }

        public UsersAPIDbContext(DbContextOptions<UsersAPIDbContext> options)
            :base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserGroup> UsersGroups { get; set; } = null!;
        public DbSet<UserState> UsersStates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory().Replace("Persistence", "WebApi"))
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgsql(configuration["ConnectionString"]);
        }
    }
}
