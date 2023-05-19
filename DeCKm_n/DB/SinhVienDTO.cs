using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCKm_n.DB
{
    public class SinhVienDTO
    {  
        //public int IdHPSV { get; set; }
        public string MSSV { get; set; }
        public string MaHP { get; set; }
        public string TenSV { get; set; } 
        public string LopSH { get; set; }
        public string TenHP { get; set; } 
        public double DBT { get; set; } 
        public double DGK { get; set; } 
        public double DCK { get; set; }
        public double DTK { get; set; }
        public DateTime NgayThi { get; set; }
        public bool GioiTinh { get; set; } 
    }
}
