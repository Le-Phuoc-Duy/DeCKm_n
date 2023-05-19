using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCKm_n.DB
{
    class CreateDB : CreateDatabaseIfNotExists<Model1>
    {
        protected override void Seed(Model1 context)
        {
            // Không cần add các thuộc tính quan hệ, tự động mapping
            context.HocPhan_SinhViens.AddRange(new HocPhan_SinhVien[]
            {
                new HocPhan_SinhVien {MaHP = "2", MSSV= "103", DBT = 10, DGK = 9, DCK = 10, NgayThi = DateTime.Now},
                new HocPhan_SinhVien {MaHP = "1", MSSV= "102", DBT = 10, DGK = 9.5, DCK = 10, NgayThi = DateTime.Now},
                new HocPhan_SinhVien {MaHP = "3", MSSV= "105",  DBT = 10, DGK = 9.5, DCK = 8, NgayThi = DateTime.Now},
                new HocPhan_SinhVien {MaHP = "4", MSSV= "104",  DBT = 9, DGK = 9.5, DCK = 10, NgayThi = DateTime.Now},
            });

            context.HocPhans.AddRange(new HocPhan[]
            {
                new HocPhan {MaHP = "1", TenHP = "CNPM"},
                new HocPhan {MaHP = "2", TenHP = "OOAD"},
                new HocPhan {MaHP = "3", TenHP = "Java"},
                new HocPhan {MaHP = "4", TenHP = "PBL"},
            });
            context.SinhViens.AddRange(new SinhVien[]
            {
                new SinhVien {MSSV = "103" ,TenSV = "Nguyễn Văn A", LopSH = "2", GioiTinh = true},
                new SinhVien {MSSV = "102" ,TenSV = "Lê Phước Duy", LopSH = "1", GioiTinh = true},
                new SinhVien {MSSV = "105" ,TenSV = "Trần Công C", LopSH = "2", GioiTinh = true},
                new SinhVien {MSSV = "104" ,TenSV = "Nguyễn Thị B", LopSH = "1", GioiTinh = false},

            });
        }
    }
}
