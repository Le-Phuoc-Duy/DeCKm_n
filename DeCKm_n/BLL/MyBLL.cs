using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeCKm_n.DB;

namespace DeCKm_n.BLL
{
    public class MyBLL
    {
        private static MyBLL _instance;
        public static MyBLL Instance
        {
            get
            {
                if (_instance == null) _instance = new MyBLL();
                return _instance;
            }

            private set { }
        }
        public List<string> GetAllLopSH()
        {
            Model1 db = new Model1();
            List<string> distinctLSH = db.SinhViens.Select(s => s.LopSH).Distinct().ToList();
            List<string> list = new List<string>();
            foreach (string i in distinctLSH)
            {
                list.Add(i);
            }
            return list;
        }
        public List<CBBItem> GetAllHocPhan()
        {
            Model1 db = new Model1();
            List<CBBItem> list = new List<CBBItem>();
            foreach (HocPhan i in db.HocPhans.Distinct().ToList())
            {
                list.Add(new CBBItem { Text = i.TenHP, Value = i.MaHP });
            }
            return list;
        }
        public List<SinhVienDTO> GetListSVDGV(string MaHP, string Search)
        {  
            Model1 db = new Model1(); 
            var li = (from sv in db.SinhViens
                      join hpsv in db.HocPhan_SinhViens on sv.MSSV equals hpsv.MSSV
                      join hp in db.HocPhans on hpsv.MaHP equals hp.MaHP
                      where (MaHP == "0" || hp.MaHP == MaHP) &&
                      (Search == null || sv.MSSV.Contains(Search) || sv.TenSV.Contains(Search))
                      select new SinhVienDTO
                      { 
                          //IdHPSV = hpsv.Id,
                          MSSV = sv.MSSV,
                          MaHP = hp.MaHP,
                          TenSV = sv.TenSV,
                          LopSH = sv.LopSH,
                          TenHP = hp.TenHP,
                          DBT = hpsv.DBT,
                          DGK = hpsv.DGK,
                          DCK = hpsv.DCK,
                          DTK = hpsv.DBT*0.2 + hpsv.DGK*0.2 + hpsv.DCK*0.5,
                          NgayThi = hpsv.NgayThi,
                          GioiTinh = sv.GioiTinh, 
                      }).ToList();
            return li; 
        }

        public delegate bool Compare(SinhVienDTO s1, SinhVienDTO s2);
        public static void SortDelegate(List<SinhVienDTO> list, Compare cmp)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (cmp(list[i], list[j]))
                    {
                        SinhVienDTO tmp = list[i];
                        list[i] = list[j];
                        list[j] = tmp;
                    }
                }
            }
        }
        public static bool Compare_DBT(SinhVienDTO s1, SinhVienDTO s2)
        {
            return s1.DBT > s2.DBT ? true : false;
        }
        public static bool Compare_DGK(SinhVienDTO s1, SinhVienDTO s2)
        {
            return s1.DGK > s2.DGK ? true : false;
        }
        public static bool Compare_DCK(SinhVienDTO s1, SinhVienDTO s2)
        {
            return s1.DCK > s2.DCK ? true : false;
        }
        public static bool Compare_DTK(SinhVienDTO s1, SinhVienDTO s2)
        {
            return (s1.DBT * 0.2 + s1.DGK * 0.2 + s1.DCK * 0.3) > (s2.DBT * 0.2 + s2.DGK * 0.2 + s2.DCK * 0.3) ? true : false;
        }
        public static bool Compare_TenSV(SinhVienDTO s1, SinhVienDTO s2)
        {
            return String.Compare(s1.TenSV, s2.TenSV) > 0 ? true : false;
        }
        public static bool Compare_LopHP(SinhVienDTO s1, SinhVienDTO s2)
        {
            return String.Compare(s1.TenHP, s2.TenHP) > 0 ? true : false;
        }
        public static bool Compare_LopSH(SinhVienDTO s1, SinhVienDTO s2)
        {
            return String.Compare(s1.LopSH, s2.LopSH) > 0 ? true : false;
        }
        public static bool Compare_NgayThi(SinhVienDTO s1, SinhVienDTO s2)
        {
            return DateTime.Compare(s1.NgayThi, s2.NgayThi) > 0 ? true : false;
        }
        public List<SinhVienDTO> SortSV(List<string> liMSSV, List<string> liMaHP, string sortAtt)
        {
            Model1 db = new Model1();
            var liSinhVien = (from sv in db.SinhViens
                                join hpsv in db.HocPhan_SinhViens on sv.MSSV equals hpsv.MSSV
                                join hp in db.HocPhans on hpsv.MaHP equals hp.MaHP
                                where liMSSV.Contains(sv.MSSV) && liMaHP.Contains(hp.MaHP)
                              select new SinhVienDTO
                              {
                                  //IdHPSV = hpsv.Id,
                                  MSSV = sv.MSSV,
                                  MaHP = hp.MaHP,
                                  TenSV = sv.TenSV,
                                  LopSH = sv.LopSH,
                                  TenHP = hp.TenHP,
                                  DBT = hpsv.DBT,
                                  DGK = hpsv.DGK,
                                  DCK = hpsv.DCK,
                                  DTK = hpsv.DBT * 0.2 + hpsv.DGK * 0.2 + hpsv.DCK * 0.5,
                                  NgayThi = hpsv.NgayThi,
                                  GioiTinh = sv.GioiTinh,
                              }).ToList(); 
            switch (sortAtt)
            {
                case "tên sv":
                    SortDelegate(liSinhVien, Compare_TenSV);
                    //liSinhVien.Sort((a, b) => a.TenSV.CompareTo(b.TenSV));
                    break;
                case "lớp sh":
                    SortDelegate(liSinhVien, Compare_LopSH);
                    //liSinhVien.Sort((a, b) => a.LopSH.CompareTo(b.LopSH));
                    break;
                case "tên học phần":
                    SortDelegate(liSinhVien, Compare_LopHP);
                    //liSinhVien.Sort((a, b) => a.HocPhan.TenHP.CompareTo(b.HocPhan.TenHP));
                    break;
                case "điểm bt":
                    SortDelegate(liSinhVien, Compare_DBT);
                    liSinhVien.Sort((a, b) => a.DBT.CompareTo(b.DBT));
                    break;
                case "điểm gk":
                    SortDelegate(liSinhVien, Compare_DGK);
                    //liSinhVien.Sort((a, b) => a.DGK.CompareTo(b.DGK));
                    break;
                case "điểm ck":
                    SortDelegate(liSinhVien, Compare_DCK);
                    //liSinhVien.Sort((a, b) => a.DCK.CompareTo(b.DCK));
                    break;
                case "tổng kết":
                    SortDelegate(liSinhVien, Compare_DTK);
                    //liSinhVien.Sort((a, b) => (a.DBT * 0.2 + a.DGK * 0.2 + a.DCK * 0.3).CompareTo(b.DBT * 0.2 + b.DGK * 0.2 + b.DCK * 0.3));
                    break;
                case "ngày thi":
                    SortDelegate(liSinhVien, Compare_NgayThi);
                    //liSinhVien.Sort((a, b) => a.NgayThi.CompareTo(b.NgayThi));
                    break;
            } 
            return liSinhVien; 
        }
        public SinhVienDTO GetSV_HP(string MSSV, string MaHP)
        {
            Model1 db = new Model1();
            return (from sv in db.SinhViens
                              join hpsv in db.HocPhan_SinhViens on sv.MSSV equals hpsv.MSSV
                              join hp in db.HocPhans on hpsv.MaHP equals hp.MaHP
                              where sv.MSSV == MSSV && hp.MaHP == MaHP
                              select new SinhVienDTO
                              {
                                  //IdHPSV = hpsv.Id,
                                  MSSV = sv.MSSV,
                                  MaHP = hp.MaHP,
                                  TenSV = sv.TenSV,
                                  LopSH = sv.LopSH,
                                  TenHP = hp.TenHP,
                                  DBT = hpsv.DBT,
                                  DGK = hpsv.DGK,
                                  DCK = hpsv.DCK,
                                  DTK = hpsv.DBT * 0.2 + hpsv.DGK * 0.2 + hpsv.DCK * 0.5,
                                  NgayThi = hpsv.NgayThi,
                                  GioiTinh = sv.GioiTinh,
                              }).FirstOrDefault();
        }
        public void UpdateSV(string MSSV, string MaHP, SinhVienDTO sv)
        {
            Model1 db = new Model1();
            SinhVien svUpdate = db.SinhViens.Where(p => p.MSSV == MSSV).FirstOrDefault();
            svUpdate.MSSV = sv.MSSV;
            svUpdate.LopSH = sv.LopSH;
            svUpdate.TenSV = sv.TenSV;
            svUpdate.GioiTinh = sv.GioiTinh;

            HocPhan_SinhVien hpsvUpdate = db.HocPhan_SinhViens
                                .Where(hpsv => hpsv.MSSV == MSSV && hpsv.MaHP == MaHP).FirstOrDefault();
            hpsvUpdate.DBT = sv.DBT;
            hpsvUpdate.DGK = sv.DGK;
            hpsvUpdate.DCK = sv.DCK;
            hpsvUpdate.MSSV = sv.MSSV;
            hpsvUpdate.MaHP= sv.MaHP;
            hpsvUpdate.NgayThi= sv.NgayThi;

            db.SaveChanges();
        }
        public void AddSV(SinhVienDTO sv)
        {
            Model1 db = new Model1();
            if(db.SinhViens.Any(s => s.MSSV == sv.MSSV))
            {
                HocPhan_SinhVien hpsvAdd = new HocPhan_SinhVien()
                {
                    DBT = sv.DBT,
                    DGK = sv.DGK,
                    DCK = sv.DCK,
                    MSSV = sv.MSSV,
                    MaHP = sv.MaHP,
                    NgayThi = sv.NgayThi,
                }; 
                db.HocPhan_SinhViens.Add(hpsvAdd);
                db.SaveChanges();
            }
            else
            {
                SinhVien svAdd = new SinhVien()
                {
                    MSSV = sv.MSSV,
                    LopSH = sv.LopSH,
                    TenSV = sv.TenSV,
                    GioiTinh = sv.GioiTinh,
                };
                HocPhan_SinhVien hpsvAdd = new HocPhan_SinhVien()
                {
                    DBT = sv.DBT,
                    DGK = sv.DGK,
                    DCK = sv.DCK,
                    MSSV = sv.MSSV,
                    MaHP = sv.MaHP,
                    NgayThi = sv.NgayThi,
                };
                db.SinhViens.Add(svAdd);
                db.HocPhan_SinhViens.Add(hpsvAdd);
                db.SaveChanges();
            }
            
        }
        public void DelSV(string MSSV, string MaHP)
        {
            Model1 db = new Model1();
            var sv = db.HocPhan_SinhViens
                        .Single(hpsv => hpsv.MSSV == MSSV && hpsv.MaHP == MaHP);
            db.HocPhan_SinhViens.Remove(sv);
            db.SaveChanges();
        }
    }
}
