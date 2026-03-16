using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetroGalerie.Models;

namespace RetroGalerie.Data
{
    public class ApplicationDbContext : IdentityDbContext<Gamer, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Console>   Consoles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGamer> GameGamers { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Console ---
            builder.Entity<Console>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Manufacturer)
                      .HasMaxLength(100);
            });

            // --- Game ---
            builder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.Id);

                entity.Property(g => g.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasOne(g => g.Console)
                      .WithMany(c => c.Games)
                      .HasForeignKey(g => g.ConsoleId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired();

                // relation 0..n : un jeu peut avoir 0 ou plusieurs screenshots
                entity.HasMany(g => g.Screenshots)
                      .WithOne(s => s.Game)
                      .HasForeignKey(s => s.GameId)
                      .OnDelete(DeleteBehavior.SetNull); // ou Restrict
            });

            // --- Screenshot ---
            builder.Entity<Screenshot>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.FilePath)
                      .IsRequired()
                      .HasMaxLength(255);
            });

            // --- Gamer ---
            builder.Entity<Gamer>(entity =>
            {
                entity.Property(g => g.Name)
                      .HasMaxLength(100);

                entity.Property(g => g.FisrtName)
                      .HasMaxLength(100);
            });

            // --- GameGamer (table de jointure N:N avec attribut Note) ---
            builder.Entity<GameGamer>(entity =>
            {
                entity.HasKey(gg => new { gg.UserId, gg.GameId }); // clé composite

                entity.HasOne(gg => gg.Gamer)
                      .WithMany(g => g.GameGamers)
                      .HasForeignKey(gg => gg.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(gg => gg.Game)
                      .WithMany()
                      .HasForeignKey(gg => gg.GameId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(gg => gg.Note)
                      .HasDefaultValue(0); // optionnel
            });
        }
    }
}
