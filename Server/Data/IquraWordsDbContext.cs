using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Reflection.Emit;

namespace Server.Data
{
    public class IquraWordsDbContext : DbContext
    {
        public IquraWordsDbContext()
        {
        }
        public IquraWordsDbContext(DbContextOptions<IquraWordsDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public virtual DbSet<Language> Language { get; set; } = null!;
        public virtual DbSet<Word> Word { get; set; } = null!;
        public virtual DbSet<WordMeaning> WordMeaning { get; set; } = null!;
        public virtual DbSet<Cluster> Cluster { get; set; } = null!;
        public virtual DbSet<Collection> Collection { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<UserWord> UserWord { get; set; } = null!;
        public virtual DbSet<LikeTable> LikeTable { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=postgres_container;Port=5432;Database=IquraWords;Username=root;Password=root;Pooling=true;Include Error Detail=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<Language>().HasData(
            new Language[]
            {
                new Language { Id = 1, Name="English"},
                new Language { Id = 2, Name="Ukrainian"},
                new Language { Id = 3, Name="French"}
            }) ;*/
        }
        public void MigrateDatabase()
        {
            Database.Migrate();
        }
    }
}
