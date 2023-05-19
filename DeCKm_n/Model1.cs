using System;
using System.Data.Entity;
using System.Linq;
using DeCKm_n.DB;

namespace DeCKm_n
{
    public class Model1 : DbContext
    { 
        public Model1()
            : base("name=Model1")
        {
            Database.SetInitializer<Model1>(new CreateDB());
        } 
        public virtual DbSet<HocPhan> HocPhans { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<HocPhan_SinhVien> HocPhan_SinhViens { get; set; }
    } 
}