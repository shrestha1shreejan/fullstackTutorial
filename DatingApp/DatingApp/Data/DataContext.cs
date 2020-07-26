using DatingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class DataContext : DbContext
    {
        #region Constructor

        public DataContext(DbContextOptions<DataContext> options ) : base(options){}

        #endregion

        #region Properties

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // defining primary key for Likes table ( as a combination of Liker and Likee Id)
            // so that one person can like another person once
            modelBuilder.Entity<Like>().HasKey(k => new { k.LikerId, k.LikeeId });

            // building many to many relationship using one to many relationship using fluent API
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Likee)
                .WithMany(l => l.Likers)
                .HasForeignKey(l => l.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
               .HasOne(l => l.Liker)
               .WithMany(l => l.Likees)
               .HasForeignKey(l => l.LikerId)
               .OnDelete(DeleteBehavior.Restrict);
        }

        #endregion
    }
}
