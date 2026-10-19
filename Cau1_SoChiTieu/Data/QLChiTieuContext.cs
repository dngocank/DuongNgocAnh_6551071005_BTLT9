using System;
using Microsoft.EntityFrameworkCore;
using Cau1_SoChiTieu.Config;
using Cau1_SoChiTieu.Models;

namespace Cau1_SoChiTieu.Data
{
    public class QLChiTieuContext : DbContext
    {
        public DbSet<ChiTieu> ChiTieus => Set<ChiTieu>();

        public QLChiTieuContext()
        {
        }

        public QLChiTieuContext(DbContextOptions<QLChiTieuContext> options) : base(options)
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
            modelBuilder.Entity<ChiTieu>(entity =>
            {
                entity.ToTable("ChiTieu");
                entity.HasKey(e => e.MaCT);
                entity.Property(e => e.NgayChi).HasColumnType("date");
                entity.Property(e => e.NoiDung).HasMaxLength(100);
                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.LoaiChi).HasMaxLength(30);
                entity.Property(e => e.GhiChu).HasMaxLength(200);
            });
        }
    }
}
