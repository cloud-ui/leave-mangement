using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeaveMangement_Entity.Models
{
    public partial class KaoQinContext : DbContext
    {
        public virtual DbSet<AdminUser> AdminUser { get; set; }
        public virtual DbSet<Apply> Apply { get; set; }
        public virtual DbSet<Authorization> Authorization { get; set; }
        public virtual DbSet<Clock> Clock { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Deparment> Deparment { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Inform> Inform { get; set; }
        public virtual DbSet<Journal> Journal { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionOfAuth> PositionOfAuth { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning  To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-BD1U6I5;Database=KaoQin;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Apply>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.HandleTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Clock>(entity =>
            {
                entity.Property(e => e.ClockDay).HasColumnType("date");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SrartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CellphoneNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.Corporation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeparmentCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.WokerCount).HasDefaultValueSql("((0))");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
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

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ParentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Url).HasMaxLength(30);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Brith).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PaperNumber).HasMaxLength(30);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Password).IsRequired();
            });
        }
    }
}
