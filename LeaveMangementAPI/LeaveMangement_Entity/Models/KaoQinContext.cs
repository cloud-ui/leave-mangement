using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeaveMangement_Entity.Models
{
    public partial class KaoQinContext : DbContext
    {
        public virtual DbSet<Apply> Apply { get; set; }
        public virtual DbSet<ApplyFoJob> ApplyFoJob { get; set; }
        public virtual DbSet<Clock> Clock { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Deparment> Deparment { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Inform> Inform { get; set; }
        public virtual DbSet<Journal> Journal { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<PaperType> PaperType { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning
                //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=134.175.14.68;Initial Catalog=KaoQin;User ID=sa;Password=314705487@qq.com");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apply>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(100);
            });

            modelBuilder.Entity<ApplyFoJob>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Clock>(entity =>
            {
                entity.Property(e => e.ClockDay)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.CellphoneNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.Corporation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DeparmentCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WokerCount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Deparment>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.WorkerCount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.OwnerType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Inform>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ParentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Url).HasMaxLength(30);
            });

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaperType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PaperNumber).HasMaxLength(30);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });
        }
    }
}
