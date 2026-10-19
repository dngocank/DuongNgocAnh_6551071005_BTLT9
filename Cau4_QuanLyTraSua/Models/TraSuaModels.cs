using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cau4_QuanLyTraSua.Models
{
    [Table("DanhMucMon")]
    public class DanhMucMon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDM { get; set; }

        [Required]
        [MaxLength(50)]
        public string TenDM { get; set; } = string.Empty;

        public virtual ICollection<MonUong> MonUongs { get; set; } = new List<MonUong>();

        public override string ToString() => TenDM;
    }

    [Table("MonUong")]
    public class MonUong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMon { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenMon { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 0)")]
        public decimal GiaBan { get; set; }

        public bool DangBan { get; set; } = true;

        public int MaDM { get; set; }

        [ForeignKey("MaDM")]
        public virtual DanhMucMon? DanhMucMon { get; set; }

        public override string ToString() => $"{TenMon} - {GiaBan:N0} đ";
    }

    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHD { get; set; }

        public DateTime NgayLap { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18, 0)")]
        public decimal TongTien { get; set; }

        [Required]
        [MaxLength(20)]
        public string HinhThucTT { get; set; } = "Tiền mặt";

        public bool DaThanhToan { get; set; } = true;

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }

    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        public int MaHD { get; set; }
        public int MaMon { get; set; }
        public int SoLy { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal DonGia { get; set; }

        [ForeignKey("MaHD")]
        public virtual HoaDon? HoaDon { get; set; }

        [ForeignKey("MaMon")]
        public virtual MonUong? MonUong { get; set; }
    }
}
