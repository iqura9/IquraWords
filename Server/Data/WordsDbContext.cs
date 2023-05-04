using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class WordsDbContext : DbContext
    {
        public WordsDbContext(DbContextOptions<WordsDbContext> options) : base(options)
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;
        public virtual DbSet<WordMeaning> WordMeanings { get; set; } = null!;
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
                .Ignore(wm => wm.ClusterId)
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
