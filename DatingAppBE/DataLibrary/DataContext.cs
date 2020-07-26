using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace DataLibrary
{
    public class DataContext : DbContext
    {
        #region Constructor

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        #endregion

        #region Properties

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        #endregion

    }

    
}
