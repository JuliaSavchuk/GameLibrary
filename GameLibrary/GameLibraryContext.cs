using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class GameLibraryContext : DbContext
    {
        public DbSet<Game> Games { get; set; } // DbSet для зберігання ігор

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GameLibrary;Integrated Security=True;Connect Timeout=30;");
        }
    }

}
