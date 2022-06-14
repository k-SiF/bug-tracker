using bug_tracker_web.Models;

namespace bug_tracker_web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
