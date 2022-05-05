namespace YardScanAPI.Data
{
    public class DataContext : DbContext
    {
        // Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // Database tables
        public DbSet<Unit>? Units { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<TrackInUnitHistory> TrackInUnitHistories { get; set; }

    }
}
