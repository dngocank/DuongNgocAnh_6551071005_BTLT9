using Microsoft.EntityFrameworkCore;
using Cau5_QuanLyDatTour.Config;
using Cau5_QuanLyDatTour.Models;

namespace Cau5_QuanLyDatTour.Data
{
    public class QLDatTourContext : DbContext
    {
        public DbSet<Tour> Tours => Set<Tour>();
        public DbSet<KhachHang> KhachHangs => Set<KhachHang>();
        public DbSet<DatTour> DatTours => Set<DatTour>();

        public QLDatTourContext()
        {
        }

        public QLDatTourContext(DbContextOptions<QLDatTourContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connStr = AppConfig.Instance.ConnectionStrings.DefaultConnection;
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");
                entity.HasKey(e => e.MaTour);
                entity.Property(e => e.TenTour).HasMaxLength(100);
                entity.Property(e => e.DiemDen).HasMaxLength(50);
                entity.Property(e => e.NgoaiTe).HasColumnType("char(3)");
                entity.Property(e => e.GiaNgoaiTe).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.NgayKhoiHanh).HasColumnType("date");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.ToTable("KhachHang");
                entity.HasKey(e => e.MaKH);
                entity.Property(e => e.HoTen).HasMaxLength(100);
                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
                entity.Property(e => e.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<DatTour>(entity =>
            {
                entity.ToTable("DatTour");
                entity.HasKey(e => e.MaDat);
                entity.Property(e => e.TyGia).HasColumnType("decimal(18, 4)");
                entity.Property(e => e.TongTienVND).HasColumnType("decimal(18, 0)");

                entity.HasOne(e => e.KhachHang)
                      .WithMany(k => k.DatTours)
                      .HasForeignKey(e => e.MaKH)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tour)
                      .WithMany(t => t.DatTours)
                      .HasForeignKey(e => e.MaTour)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
