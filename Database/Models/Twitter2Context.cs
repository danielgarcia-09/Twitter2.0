using System;
using Database.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Models
{
    public partial class Twitter2Context : IdentityDbContext
    {
        public Twitter2Context()
        {
        }

        public Twitter2Context(DbContextOptions<Twitter2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Amigos> Amigos { get; set; }
        public virtual DbSet<Publicaciones> Publicaciones { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Comentarios> Comentarios { get; set; }
        public virtual DbSet<Replies> Replies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=LAPTOP-184T66BD\\SQLEXPRESS;Database=Twitter2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<Amigos>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Id);

                entity.Property(e => e.Amigo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comentarios>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Comentario)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Replies>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Texto)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Publicaciones>(entity =>
            {
                entity.ToTable("publicaciones");

                entity.Property(e => e.HoraPublicacion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Texto).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.HasIndex(e => e.Id);

                entity.Property(e => e.Nombre).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
