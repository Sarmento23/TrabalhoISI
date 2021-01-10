using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cliente.Models
{
    public partial class ISITableContext : DbContext
    {
        public ISITableContext()
        {
        }

        public ISITableContext(DbContextOptions<ISITableContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Stadium> Stadiums { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ISITable;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.HasIndex(e => e.SectorId, "IX_events_SectorID");

                entity.HasIndex(e => e.StadiumId, "IX_events_StadiumID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SectorId).HasColumnName("SectorID");

                entity.Property(e => e.StadiumId).HasColumnName("StadiumID");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.SectorId);

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("sectors");

                entity.HasIndex(e => e.StadiumId, "IX_sectors_StadiumID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.StadiumId).HasColumnName("StadiumID");

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.Sectors)
                    .HasForeignKey(d => d.StadiumId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Stadium>(entity =>
            {
                entity.ToTable("stadiums");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.HasIndex(e => e.EventId, "IX_tickets_EventID");

                entity.HasIndex(e => e.StadiumId, "IX_tickets_StadiumID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdeptName).IsRequired();

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.SectorName).IsRequired();

                entity.Property(e => e.StadiumId).HasColumnName("StadiumID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Stadium)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.StadiumId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
