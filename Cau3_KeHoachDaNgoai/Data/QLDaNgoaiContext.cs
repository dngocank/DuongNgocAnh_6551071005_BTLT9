using Microsoft.EntityFrameworkCore;
using Cau3_KeHoachDaNgoai.Config;
using Cau3_KeHoachDaNgoai.Models;

namespace Cau3_KeHoachDaNgoai.Data
{
    public class QLDaNgoaiContext : DbContext
    {
        public DbSet<ChuyenDi> ChuyenDis => Set<ChuyenDi>();

        public QLDaNgoaiContext()
        {
        }

        public QLDaNgoaiContext(DbContextOptions<QLDaNgoaiContext> options) : base(options)
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
            modelBuilder.Entity<ChuyenDi>(entity =>
            {
                entity.ToTable("ChuyenDi");
                entity.HasKey(e => e.MaChuyen);
                entity.Property(e => e.TenChuyen).HasMaxLength(100);
                entity.Property(e => e.DiaDiem).HasMaxLength(100);
                entity.Property(e => e.NgayDi).HasColumnType("date");
                entity.Property(e => e.NganSach).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.PhuongTien).HasMaxLength(30);
                entity.Property(e => e.GhiChu).HasMaxLength(200);
            });
        }
    }
}
