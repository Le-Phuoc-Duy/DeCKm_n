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

namespace DeCKm_n
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
            ShowDGV("0", null);
            RenameDGV(); 
            initCBB(); 
        }
        public void initCBB()
        {
            cboLHP.Items.Add(new CBBItem { Text = "All", Value = "0" });
            cboLHP.Items.AddRange(MyBLL.Instance.GetAllHocPhan().ToArray());

            List<string> liSort = new List<string>() { "Tên SV", "Lớp SH", "Học phần", "Điểm BT", "Điểm GK", "Điểm CK", "Tổng kết", "Ngày thi" };
            cboSort.Items.AddRange(liSort.ToArray()); 
            cboLHP.SelectedIndex = 0;
        } 
        private void ShowDGV(string MaHP, string Search)
        { 
            dataGridView1.DataSource = MyBLL.Instance.GetListSVDGV(MaHP, Search);
        }
        private void RenameDGV()
        {  
            dataGridView1.Columns["MaHP"].Visible = false;
            dataGridView1.Columns["MSSV"].Visible = false;
            dataGridView1.Columns["GioiTinh"].Visible = false; 

            dataGridView1.Columns["TenSV"].HeaderText = "Tên SV";
            dataGridView1.Columns["LopSH"].HeaderText = "Lớp SH";
            dataGridView1.Columns["TenHP"].HeaderText = "Học phần";
            dataGridView1.Columns["DBT"].HeaderText = "Điểm BT";
            dataGridView1.Columns["DGK"].HeaderText = "Điểm GK";
            dataGridView1.Columns["DCK"].HeaderText = "Điểm CK";
            dataGridView1.Columns["DTK"].HeaderText = "Tổng kết";
            dataGridView1.Columns["NgayThi"].HeaderText = "Ngày thi"; 
            dataGridView1.Columns.Add("STT", "STT");
            dataGridView1.Columns["STT"].DisplayIndex = 0;
        }

        private void cboLHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MaHP = ((CBBItem)cboLHP.SelectedItem).Value;
            ShowDGV(MaHP, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string MaHP = ((CBBItem)cboLHP.SelectedItem).Value;
            ShowDGV(MaHP, txtSearch.Text);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            List<string> liMSSV = new List<string>();
            List<string> liMaHP = new List<string>();
            foreach (DataGridViewRow i in dataGridView1.Rows)
            { 
                liMSSV.Add(i.Cells["MSSV"].Value.ToString());
                liMaHP.Add(i.Cells["MaHP"].Value.ToString()); 
            } 
            dataGridView1.DataSource = MyBLL.Instance.SortSV(liMSSV, liMaHP, cboSort.Text.ToLower());
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.d += new Form2.MyDel(ShowDGV);
            form2.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            { 
                string MSSV = dataGridView1.SelectedRows[0].Cells["MSSV"].Value.ToString();
                string MaHP = dataGridView1.SelectedRows[0].Cells["MaHP"].Value.ToString();
                Form2 form2 = new Form2(MSSV, MaHP);
                form2.d += new Form2.MyDel(ShowDGV);
                form2.Show();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        { 
            foreach (DataGridViewRow i in dataGridView1.SelectedRows)
            {
                string MSSV = i.Cells["MSSV"].Value.ToString();
                string MaHP = i.Cells["MaHP"].Value.ToString();
                MyBLL.Instance.DelSV(MSSV, MaHP);
            }
            ShowDGV("0", null);
            initCBB();
        }
    }
}
