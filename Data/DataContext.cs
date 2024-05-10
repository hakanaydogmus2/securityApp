using Microsoft.EntityFrameworkCore;
using securityApp.Models;
namespace securityApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Scan> Scans { get; set; }
    }
}
