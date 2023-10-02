using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class NbaDbContext : DbContext
    {
        // Constructor that takes DbContextOptions as a parameter.
        public NbaDbContext(DbContextOptions<NbaDbContext> options) : base(options)
        {
            // Initialize the context 
        }

        // Allows querying and interacting with the tables in the database.
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Standings> Standings { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSeasonAverages> PlayerSeasonAverages { get; set; }
    }
}
