using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cau5_QuanLyDatTour.Models
{
    [Table("Tour")]
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTour { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenTour { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string DiemDen { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "char(3)")]
        public string NgoaiTe { get; set; } = "USD";

        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaNgoaiTe { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayKhoiHanh { get; set; }

        public int SoChoToiDa { get; set; }

        public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();

        public override string ToString() => $"{TenTour} ({DiemDen} - {GiaNgoaiTe:N2} {NgoaiTe})";
    }

    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKH { get; set; }

        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string SoDienThoai { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();

        public override string ToString() => $"{HoTen} ({SoDienThoai})";
    }

    [Table("DatTour")]
    public class DatTour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDat { get; set; }

        public int MaKH { get; set; }

        public int MaTour { get; set; }

        public int SoNguoi { get; set; }

        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TyGia { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal TongTienVND { get; set; }

        [ForeignKey("MaKH")]
        public virtual KhachHang? KhachHang { get; set; }

        [ForeignKey("MaTour")]
        public virtual Tour? Tour { get; set; }
    }
}
