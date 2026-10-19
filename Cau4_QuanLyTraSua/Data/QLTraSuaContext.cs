using Microsoft.EntityFrameworkCore;
using Cau4_QuanLyTraSua.Config;
using Cau4_QuanLyTraSua.Models;

namespace Cau4_QuanLyTraSua.Data
{
    public class QLTraSuaContext : DbContext
    {
        public DbSet<DanhMucMon> DanhMucMons => Set<DanhMucMon>();
        public DbSet<MonUong> MonUongs => Set<MonUong>();
        public DbSet<HoaDon> HoaDons => Set<HoaDon>();
        public DbSet<ChiTietHoaDon> ChiTietHoaDons => Set<ChiTietHoaDon>();

        public QLTraSuaContext()
        {
        }

        public QLTraSuaContext(DbContextOptions<QLTraSuaContext> options) : base(options)
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
            modelBuilder.Entity<DanhMucMon>(entity =>
            {
                entity.ToTable("DanhMucMon");
                entity.HasKey(e => e.MaDM);
                entity.Property(e => e.TenDM).HasMaxLength(50);
            });

            modelBuilder.Entity<MonUong>(entity =>
            {
                entity.ToTable("MonUong");
                entity.HasKey(e => e.MaMon);
                entity.Property(e => e.TenMon).HasMaxLength(100);
                entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");

                entity.HasOne(e => e.DanhMucMon)
                      .WithMany(d => d.MonUongs)
                      .HasForeignKey(e => e.MaDM)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.ToTable("HoaDon");
                entity.HasKey(e => e.MaHD);
                entity.Property(e => e.TongTien).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.HinhThucTT).HasMaxLength(20);
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.ToTable("ChiTietHoaDon");
                entity.HasKey(e => new { e.MaHD, e.MaMon });
                entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");

                entity.HasOne(e => e.HoaDon)
                      .WithMany(h => h.ChiTietHoaDons)
                      .HasForeignKey(e => e.MaHD)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MonUong)
                      .WithMany()
                      .HasForeignKey(e => e.MaMon)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
