using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KBS_FunEvents_Web_2024.ViewModels;

#nullable disable

namespace KBS_FunEvents_Web_2024.Models
{
    public partial class _dbContext : DbContext
    {
        public _dbContext()
        {
        }

        public _dbContext(DbContextOptions<_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBuchungen> TblBuchungens { get; set; }
        public virtual DbSet<TblEvKategorie> TblEvKategories { get; set; }
        public virtual DbSet<TblEvVeranstalter> TblEvVeranstalters { get; set; }
        public virtual DbSet<TblEvent> TblEvents { get; set; }
        public virtual DbSet<TblEventDaten> TblEventDatens { get; set; }
        public virtual DbSet<TblKunden> TblKundens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<TblBuchungen>(entity =>
            {
                entity.HasKey(e => e.BuBuchungsId)
                    .HasName("tbl_Buchungen_PK");

                entity.HasOne(d => d.EdEvDaten)
                    .WithMany(p => p.TblBuchungens)
                    .HasForeignKey(d => d.EdEvDatenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_EvDaten_tbl_Buchungen_FK1");

                entity.HasOne(d => d.KdKunden)
                    .WithMany(p => p.TblBuchungens)
                    .HasForeignKey(d => d.KdKundenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_Kunden_tbl_Buchungen_FK1");
            });

            modelBuilder.Entity<TblEvKategorie>(entity =>
            {
                entity.HasKey(e => e.EkEvKategorieId)
                    .HasName("tbl_EvKategorie_PK");
            });

            modelBuilder.Entity<TblEvVeranstalter>(entity =>
            {
                entity.HasKey(e => e.EvEvVeranstalterId)
                    .HasName("tbl_EvVeranstalter_PK");
            });

            modelBuilder.Entity<TblEvent>(entity =>
            {
                entity.HasKey(e => e.EtEventId)
                    .HasName("tbl_EvBeschreibung_PK");

                entity.HasOne(d => d.EkEvKategorie)
                    .WithMany(p => p.TblEvents)
                    .HasForeignKey(d => d.EkEvKategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_EvKategorie_tbl_Events_FK1");

                entity.HasOne(d => d.EvEvVeranstalter)
                    .WithMany(p => p.TblEvents)
                    .HasForeignKey(d => d.EvEvVeranstalterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_EvVeranstalter_tbl_Events_FK1");
            });

            modelBuilder.Entity<TblEventDaten>(entity =>
            {
                entity.HasKey(e => e.EdEvDatenId)
                    .HasName("tbl_Events_PK");

                entity.HasOne(d => d.EtEvent)
                    .WithMany(p => p.TblEventDatens)
                    .HasForeignKey(d => d.EtEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tbl_Events_tbl_EvDaten_FK1");
            });

            modelBuilder.Entity<TblKunden>(entity =>
            {
                entity.HasKey(e => e.KdKundenId)
                    .HasName("tbl_Kunden_PK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<KBS_FunEvents_Web_2024.ViewModel.BookingViewModel> BookingViewModel { get; set; }
        public DbSet<KBS_FunEvents_Web_2024.ViewModels.LoginModelView> LoginModelView { get; set; }
        public DbSet<KBS_FunEvents_Web_2024.ViewModels.RegistrationModelView> RegistrationModelView { get; set; }
    }
}
