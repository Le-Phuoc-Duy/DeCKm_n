using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCKm_n.DB
{
    [Table("HocPhan")]
    public class HocPhan
    {
        
        [Key]
        [Column("Mã học phần")]
        public string MaHP { get; set; }
        [Column("Tên học phần")]
        public string TenHP { get; set; }
        public virtual ICollection<HocPhan_SinhVien> HocPhan_SinhViens { get; set; }
        public HocPhan()
        {
            HocPhan_SinhViens = new HashSet<HocPhan_SinhVien>();
        }
    }
}
