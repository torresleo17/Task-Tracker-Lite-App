using Microsoft.EntityFrameworkCore;
using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definición de las Tablas
        public DbSet<Board> Boards { get; set; }
        public DbSet<ListItem> Lists { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ListItem>()
                .HasOne(l => l.Board)
                .WithMany(b => b.Lists)
                .HasForeignKey(l => l.BoardId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.ListItem)
                .WithMany(l => l.TaskItem)
                .HasForeignKey(t => t.ListId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
