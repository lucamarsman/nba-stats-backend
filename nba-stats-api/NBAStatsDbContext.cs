using Microsoft.EntityFrameworkCore;

namespace nba_stats_api.Models
{
    public class NBAStatsDbContext : DbContext
    {
        public NBAStatsDbContext(DbContextOptions<NBAStatsDbContext> options) : base(options) { }

        // Define DbSets for models
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerStat> PlayerStats { get; set; }
        public DbSet<TeamStat> TeamStats { get; set; }
        public DbSet<Boxscore> Boxscores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .Property(p => p.PlayerId)
                .ValueGeneratedNever(); // Disable identity generation for PlayerId

            modelBuilder.Entity<Team>()
                .Property(t => t.TeamId)
                .ValueGeneratedNever(); // Disable identity generation for PlayerId

            modelBuilder.Entity<Game>()
                .Property(g => g.GameId)
                .ValueGeneratedNever(); // Disable identity generation for PlayerId

            modelBuilder.Entity<PlayerStat>()
                .Property(ps => ps.PlayerId)
                .ValueGeneratedNever(); // Disable identity generation for PlayerId
            modelBuilder.Entity<PlayerStat>()
                .HasKey(ps => new { ps.PlayerId, ps.PerMode, ps.Season }); // Composite key

            modelBuilder.Entity<TeamStat>()
                .Property(ts => ts.TeamId)
                .ValueGeneratedNever(); // Disable identity generation for PlayerId
            modelBuilder.Entity<TeamStat>()
                .HasKey(ts => new { ts.TeamId, ts.PerMode, ts.Season }); // Composite key

        }
    }
}
