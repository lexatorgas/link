using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Data
{
    public class DataBaseContext : DbContext
    {        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;UserId=root;Password=123usehon;database=newDB;");
        }
        public DbSet<Url> Urls { get; set; }

    }
}
