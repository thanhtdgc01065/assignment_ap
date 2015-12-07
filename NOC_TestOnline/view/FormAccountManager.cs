using NOC_TestOnline.controller;
using NOC_TestOnline.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOC_TestOnline.view
{
    public partial class FormAccountManager : Form
    {
        public FormAccountManager()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Enabled = true;
            this.button3.Enabled = false;
            this.button4.Enabled = false;

            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox4.Text = string.Empty;
            this.richTextBox1.Text = string.Empty;

            button2.Click += Button2_Click1;
            button2.Click -= Button2_Click;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DonVi temp = Program.data.DonVis.Where(dv => dv.TenDoi.Equals(comboBox2.SelectedValue.ToString())).SingleOrDefault();
            comboBox2.Tag = (string)temp.MaDonVi;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.DataSource = Program.data.DonVis.Where(dv => dv.TenPhong.Equals(comboBox1.SelectedValue.ToString())).OrderBy(dv => dv.TenDoi).Select(dv => dv.TenDoi).ToList();
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }

        private void Button2_Click1(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to create new account?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    TaiKhoan taiKhoan = new TaiKhoan();
                    taiKhoan.TenTaiKhoan = textBox1.Text;
                    taiKhoan.MatKhau = EncryptData.MD5Hash("123");
                    taiKhoan.HoTen = textBox2.Text;
                    taiKhoan.Email = textBox3.Text;
                    taiKhoan.Mobile = textBox4.Text;
                    taiKhoan.DiaChi = richTextBox1.Text;
                    taiKhoan.MaDonVi = (string)comboBox2.Tag;
                    taiKhoan.IsDeleted = false;

                    Program.data.TaiKhoans.Add(taiKhoan);
                    Program.data.SaveChanges();

                    MessageBox.Show("Account " + textBox1.Text + " has been created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Create new account failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                button2.Click += Button2_Click;
                button2.Click -= Button2_Click1;
            }
        }

        private void FillData()
        {
            this.dataGridView1.DataSource = Program.data.vwFullAccountDetails.ToList();

            this.dataGridView1.Columns[0].HeaderText = "Username";
            this.dataGridView1.Columns[1].HeaderText = "Full name";
            this.dataGridView1.Columns[2].HeaderText = "Department";
            this.dataGridView1.Columns[3].HeaderText = "Team";
            this.dataGridView1.Columns[4].HeaderText = "Email";
            this.dataGridView1.Columns[5].HeaderText = "Mobile";

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            this.textBox1.Enabled = false;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
        }

        private void FormAccountManager_Load(object sender, EventArgs e)
        {
            FillData();
            this.button2.Click += Button2_Click;
            this.comboBox1.DataSource = Program.data.DonVis.Select(dv => dv.TenPhong).Distinct().ToList();
            comboBox1.SelectedValueChanged += ComboBox1_SelectedIndexChanged;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var userName = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TaiKhoan tk = Program.data.TaiKhoans.Single(i => i.TenTaiKhoan.Equals(userName));

            textBox1.Text = tk.TenTaiKhoan;
            textBox2.Text = tk.HoTen;
            textBox3.Text = tk.Email;
            textBox4.Text = tk.Mobile;
            richTextBox1.Text = tk.DiaChi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to delete this account?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string userName = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    TaiKhoan temp = Program.data.TaiKhoans.SingleOrDefault(tk => tk.TenTaiKhoan.Equals(userName));
                    Program.data.TaiKhoans.Remove(temp);
                    Program.data.SaveChanges();

                    MessageBox.Show("Delete successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                }
            }
            catch
            {
                MessageBox.Show("Delete failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to update this account?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string userName = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    TaiKhoan temp = Program.data.TaiKhoans.Single(tk => tk.TenTaiKhoan.Equals(userName));

                    temp.HoTen = textBox2.Text;
                    temp.Email = textBox3.Text;
                    temp.Mobile = textBox4.Text;
                    temp.DiaChi = richTextBox1.Text;
                    Program.data.SaveChanges();

                    MessageBox.Show("Update successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                }
            }
            catch
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
