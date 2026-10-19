using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cau3_KeHoachDaNgoai.Models
{
    [Table("ChuyenDi")]
    public class ChuyenDi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChuyen { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenChuyen { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DiaDiem { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime NgayDi { get; set; }

        public int SoNguoi { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal NganSach { get; set; }

        [Required]
        [MaxLength(30)]
        public string PhuongTien { get; set; } = string.Empty;

        public bool DaHoanThanh { get; set; }

        [MaxLength(200)]
        public string? GhiChu { get; set; }
    }
}
