using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeCKm_n.BLL;
using DeCKm_n.DB;

namespace DeCKm_n
{
    public partial class Form2 : Form
    {
        public delegate void MyDel(string MaHP, string name);
        public MyDel d { get; set; }
        private string MSSV;
        private string MAHP; 
        public Form2(string mssv = null, string mahp = null)
        {
            InitializeComponent();
            MSSV= mssv;
            MAHP= mahp;
            initCBB();
            SetGUI();
        }
        public void initCBB()
        {
            cboLHP.Items.AddRange(MyBLL.Instance.GetAllHocPhan().ToArray());
            cboLSH.Items.AddRange(MyBLL.Instance.GetAllLopSH().ToArray());
        }
        private void SetGUI()
        {
            if (MSSV != null && MAHP != null)
            {
                SinhVienDTO sv = MyBLL.Instance.GetSV_HP(MSSV, MAHP);
                txtMssv.Text = sv.MSSV;
                txtName.Text = sv.TenSV.ToString();
                txtDBT.Text = sv.DBT.ToString();
                txtDGK.Text = sv.DGK.ToString();
                txtDCK.Text = sv.DCK.ToString();
                if (sv.GioiTinh == true) rdoMale.Checked = true;
                else rdoFemale.Checked = true;
                cboLHP.Text = sv.TenHP;
                cboLSH.Text = sv.LopSH;
                txtTK.Text = (sv.DBT * 0.2 + sv.DGK * 0.2 + sv.DCK * 0.3).ToString();
            }
        } 
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            double sum = 0;
            if (double.TryParse(txtDCK.Text, out double value1))
            {
                sum += value1 * 0.2;
            }
            if (double.TryParse(txtDBT.Text, out double value2))
            {
                sum += value2 * 0.2;
            }
            if (double.TryParse(txtDCK.Text, out double value3))
            {
                sum += value3 * 0.3;
            }
            txtTK.Text = sum.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (MSSV != null && MAHP != null)
            { 
                SinhVienDTO sv = new SinhVienDTO
                {
                    MSSV = txtMssv.Text,
                    MaHP = ((CBBItem)cboLHP.SelectedItem).Value, 
                    TenSV = txtName.Text,
                    LopSH = cboLSH.Text, 
                    DBT = Convert.ToDouble(txtDBT.Text),
                    DGK = Convert.ToDouble(txtDGK.Text),
                    DCK = Convert.ToDouble(txtDCK.Text),
                    GioiTinh = rdoMale.Checked,
                    NgayThi = dateTimePicker1.Value
                };
                MyBLL.Instance.UpdateSV(MSSV, MAHP,sv);
            }
            else
            {
                SinhVienDTO sv = new SinhVienDTO
                {
                    MSSV = txtMssv.Text,
                    MaHP = ((CBBItem)cboLHP.SelectedItem).Value, 
                    TenSV = txtName.Text,
                    LopSH = cboLSH.Text, 
                    DBT = Convert.ToDouble(txtDBT.Text),
                    DGK = Convert.ToDouble(txtDGK.Text),
                    DCK = Convert.ToDouble(txtDCK.Text),
                    GioiTinh = rdoMale.Checked,
                    NgayThi = dateTimePicker1.Value
                };
                MyBLL.Instance.AddSV(sv);
            }
            d("0", null);
            this.Dispose();
        }
    }
}
