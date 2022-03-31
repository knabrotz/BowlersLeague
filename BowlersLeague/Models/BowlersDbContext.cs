using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlersLeague.Models
{
    public class BowlersDbContext : DbContext
    {
        public BowlersDbContext(DbContextOptions<BowlersDbContext> options) : base(options)
        {

        }

        public DbSet<Bowler> Bowlers { get; set; }

        public DbSet<Team> Teams { get; set; }
    }
}
