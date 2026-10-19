using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cau1_SoChiTieu.Models
{
    [Table("ChiTieu")]
    public class ChiTieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCT { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime NgayChi { get; set; }

        [Required]
        [MaxLength(100)]
        public string NoiDung { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal SoTien { get; set; }

        [Required]
        [MaxLength(30)]
        public string LoaiChi { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? GhiChu { get; set; }
    }
}
