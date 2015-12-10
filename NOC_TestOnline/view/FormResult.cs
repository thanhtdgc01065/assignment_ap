using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOC_TestOnline.view
{
    public partial class FormResult : Form
    {
        public FormResult()
        {
            InitializeComponent();
        }
    
        private void FormResult_Load(object sender, EventArgs e)
        {
            //this.dataGridView1.DataSource = Program.data.vwResults.ToList();

            var temp = from tk in Program.data.TaiKhoans
                       join dv in Program.data.DonVis on tk.MaDonVi equals dv.MaDonVi
                       join bt in Program.data.BaiThis on tk.TenTaiKhoan equals bt.TenTaiKhoan
                       join kq in Program.data.KetQuas on bt.MaBaiThi equals kq.MaBaiThi
                       join dt in Program.data.DeThis on bt.MaDeThi equals dt.MaDeThi
                       join nv in Program.data.NghiepVus on dt.MaNghiepVu equals nv.MaNghiepVu
                       orderby bt.NgayThi,tk.TenTaiKhoan
                       select new
                       {
                           tk.TenTaiKhoan,
                           tk.HoTen,
                           dv.TenPhong,
                           dv.TenDoi,
                           nv.TenNghiepVu,
                           bt.NgayThi,
                           kq.DiemSo
                       };
            this.dataGridView1.DataSource = temp.ToList();
            SetupTable();
        }

        private void SetupTable()
        {
            this.dataGridView1.Columns[0].HeaderText = "Username";
            this.dataGridView1.Columns[1].HeaderText = "Full Name";
            this.dataGridView1.Columns[2].HeaderText = "Department";
            this.dataGridView1.Columns[3].HeaderText = "Team";
            this.dataGridView1.Columns[4].HeaderText = "Skill";
            this.dataGridView1.Columns[5].HeaderText = "Test Date";
            this.dataGridView1.Columns[6].HeaderText = "Mark";

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
