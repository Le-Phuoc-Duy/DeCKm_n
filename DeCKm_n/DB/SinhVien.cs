using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCKm_n.DB
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [Column("Mã sinh viên")]
        public string MSSV { get; set; }
        [Column("Tên sinh viên")]
        public string TenSV { get; set; }
        [Column("Lớp sinh hoạt")]
        public string LopSH { get; set; }
        [Column("Giới tính")]
        public bool GioiTinh { get; set; } 
        public virtual ICollection<HocPhan_SinhVien> HocPhan_SinhViens { get; set; }
        public SinhVien()
        {
            HocPhan_SinhViens = new HashSet<HocPhan_SinhVien>();
        } 
    }
}
