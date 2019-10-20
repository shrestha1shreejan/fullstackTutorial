using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary
{
    public class DataContext : DbContext
    {
        #region Constructor

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        #endregion

        #region Properties

        public DbSet<Value> Values { get; set; }

        #endregion
    }
}
