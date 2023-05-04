using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class WordsDbContext : DbContext
    {
        public WordsDbContext(DbContextOptions<WordsDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public virtual DbSet<Language> Language { get; set; } = null!;
        public virtual DbSet<Word> Word { get; set; } = null!;
        public virtual DbSet<WordMeaning> WordMeaning { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=postgres_container;Port=5432;Database=IquraWords_Admin;Username=root;Password=root;Pooling=true;Include Error Detail=true;");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WordMeaning>()
                .Ignore(wm => wm.Cluster_Id)
                .Ignore(wm => wm.Cluster);
            modelBuilder.Ignore<Cluster>();
            modelBuilder.Ignore<Collection>();
            modelBuilder.Ignore<LikeTable>();
            modelBuilder.Ignore<User>();
            modelBuilder.Ignore<UserWord>();
        }

        public void MigrateDatabase()
        {
            Database.Migrate();
        }
    }
}
