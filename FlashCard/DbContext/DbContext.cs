using FlashCard.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Model
{
    public class FlashCardDbContext : DbContext
    {
        public FlashCardDbContext(DbContextOptions<FlashCardDbContext> options) : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasKey(l => l.LanguageId);
            modelBuilder.Entity<Level>().HasKey(l => l.LevelId);
            modelBuilder.Entity<Word>().HasKey(w => w.WordId);
            modelBuilder.Entity<Translation>().HasKey(t => t.TranslationId);

            modelBuilder.Entity<Word>()
                .HasOne(w => w.Language)
                .WithMany()
                .HasForeignKey(w => w.LanguageId);

            modelBuilder.Entity<Word>()
                .HasOne(w => w.Level)
                .WithMany()
                .HasForeignKey(w => w.LevelId);

            modelBuilder.Entity<Translation>()
    .HasOne(t => t.SourceWord)
    .WithMany()
    .HasForeignKey(t => t.SourceWordId)
    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Translation>()
                .HasOne(t => t.TargetWord)
                .WithMany()
                .HasForeignKey(t => t.TargetWordId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
