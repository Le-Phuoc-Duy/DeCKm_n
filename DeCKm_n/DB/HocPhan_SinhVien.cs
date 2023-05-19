using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCKm_n.DB
{
    [Table("HocPhan_SinhVien")]
    public class HocPhan_SinhVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Mã sinh viên")]
        public string MSSV { get; set; }
        [Column("Mã học phần")]
        public string MaHP { get; set; }
        [Column("Điểm bài tập")]
        public double DBT { get; set; }
        [Column("Điểm giữa kỳ")]
        public double DGK { get; set; }
        [Column("Điểm cuối kỳ")]
        public double DCK { get; set; }
        [Column("Ngày thi")]
        public DateTime NgayThi { get; set; }

        [ForeignKey("MSSV")]  
        public virtual SinhVien SinhVien { get; set; }
        [ForeignKey("MaHP")]
        public virtual HocPhan HocPhan { get; set; }  
    }
}
