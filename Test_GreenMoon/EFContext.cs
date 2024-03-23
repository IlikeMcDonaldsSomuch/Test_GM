using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Test_GreenMoon.Models;

namespace Test_GreenMoon
{
    public class EFContext : DbContext
    {
        public DbSet<PersonModel> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDatabaseName");
        }

    }
}

