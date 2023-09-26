using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class NbaDbContext : DbContext
{
    public NbaDbContext(DbContextOptions<NbaDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<PlayerStats> PlayerStats { get; set; }
    public DbSet<Standings> Standings { get; set; }
    public DbSet<BoxScore> BoxScores { get; set; }
}