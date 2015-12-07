using NOC_TestOnline.model;
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
    public partial class FormSkillManager : Form
    {
        List<string> listDepartment = new List<string>();
        List<string> listTeam = new List<string>();

        public FormSkillManager()
        {
            InitializeComponent();

            this.comboBox1.Enabled = this.comboBox2.Enabled = false;
            this.textBox1.Enabled = textBox2.Enabled = false;
            this.listBox2.Enabled = this.listBox3.Enabled = false;
            this.button3.Enabled = this.button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FormSkillManager_Load(object sender, EventArgs e)
        {
            listDepartment = Program.data.DonVis.Select(dv => dv.TenPhong).Distinct().ToList();

            this.listBox1.DataSource = listDepartment;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listTeam = Program.data.DonVis.Where(dv => dv.TenPhong.Equals(listBox1.SelectedValue.ToString())).OrderBy(dv => dv.TenDoi).Select(dv => dv.TenDoi).ToList();

                this.listBox2.Enabled = true;
                this.listBox2.DataSource = listTeam;
                this.listBox2.SelectedIndex = -1;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox2.SelectedIndex != -1)
            {
                this.listBox3.Enabled = true;
                this.groupBox4.Enabled = true;
                string maDonvi = (Program.data.DonVis.SingleOrDefault(dv => dv.TenDoi.Equals(listBox2.SelectedValue.ToString()))).MaDonVi;
                this.listBox3.DataSource = Program.data.NghiepVus.Where(nv => nv.MaDonVi.Equals(maDonvi) & nv.IsDeleted == false).Select(nv => nv.TenNghiepVu).ToList();
                this.listBox3.SelectedIndex = -1;
            }
        }

        private void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox3.SelectedIndex != -1)
            {
                this.button3.Enabled = this.button4.Enabled = true;
                NghiepVu temp = Program.data.NghiepVus.SingleOrDefault(nv => nv.TenNghiepVu.Equals(listBox3.SelectedValue.ToString()));
                FillData(temp);
            }
            else
            {
                this.button3.Enabled = this.button4.Enabled = false;
                FillData();
            }
        }

        private void FillData(NghiepVu item = null)
        {
            if (item != null)
            {
                this.textBox1.Text = item.TenNghiepVu;
                this.textBox2.Text = item.MaNghiepVu;
            }
            else
            {
                this.textBox1.Text = string.Empty;
                this.textBox2.Text = string.Empty;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                NghiepVu temp = Program.data.NghiepVus.SingleOrDefault(nv => nv.TenNghiepVu.Equals(listBox3.SelectedValue.ToString()));
                DialogResult dr = MessageBox.Show("Do you want to delete this skill?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    temp.IsDeleted = true;
                    Program.data.SaveChanges();

                    MessageBox.Show("Delete skill successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Delete failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.comboBox1.Enabled = true;
            this.comboBox1.DataSource = listDepartment;

            this.textBox1.Enabled = this.textBox2.Enabled = true;
            this.groupBox1.Enabled = this.groupBox2.Enabled = this.groupBox3.Enabled = false;
            this.button1.Enabled = this.button2.Enabled = this.button4.Enabled = false;
            this.button3.Text = "Save";

            this.button3.Click += Button3_Click;
            this.button3.Click -= button3_Click;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty && textBox2.Text.Trim() != string.Empty)
            {
                try
                {

                    DialogResult dr = MessageBox.Show("Do you want to save?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        NghiepVu temp = Program.data.NghiepVus.SingleOrDefault(nv => nv.MaNghiepVu.Equals(textBox2.Text));
                        temp.MaNghiepVu = textBox2.Text;
                        temp.MaDonVi = (string)comboBox2.Tag;
                        temp.TenNghiepVu = textBox1.Text;
                        temp.IsDeleted = false;
                        Program.data.SaveChanges();

                        MessageBox.Show("Save successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Save failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.button3.Click -= Button3_Click;
                    this.button3.Click += button3_Click;

                    this.groupBox1.Enabled = this.groupBox2.Enabled = this.groupBox3.Enabled = true;
                    this.button1.Enabled = this.button2.Enabled = this.button4.Enabled = true;
                    this.button3.Text = "Update";

                    string maDonvi = (Program.data.DonVis.SingleOrDefault(dv => dv.TenDoi.Equals(listBox2.SelectedValue.ToString()))).MaDonVi;
                    this.listBox3.DataSource = Program.data.NghiepVus.Where(nv => nv.MaDonVi.Equals(maDonvi) & nv.IsDeleted == false).Select(nv => nv.TenNghiepVu).ToList();
                    this.listBox3.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Please fill all textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listTeam = Program.data.DonVis.Where(dv => dv.TenPhong.Equals(comboBox1.SelectedValue.ToString())).OrderBy(dv => dv.TenDoi).Select(dv => dv.TenDoi).ToList();
            this.comboBox2.DataSource = listTeam;
            this.comboBox2.SelectedIndex = -1;
            this.comboBox2.Enabled = true;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                this.comboBox2.Tag = (string)(Program.data.DonVis.SingleOrDefault(dv => dv.TenDoi.Equals(comboBox2.SelectedValue.ToString()))).MaDonVi;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.comboBox1.Enabled = true;
            this.comboBox1.DataSource = listDepartment;

            this.groupBox1.Enabled = this.groupBox2.Enabled = this.groupBox3.Enabled = false;
            this.button1.Enabled = this.button3.Enabled = this.button4.Enabled = false;
            this.comboBox1.Enabled = this.comboBox2.Enabled = true;
            this.button2.Text = "Save";

            this.textBox1.Enabled = this.textBox2.Enabled = true;
            FillData();

            this.button2.Click += Button2_Click;
            this.button2.Click -= button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty && textBox2.Text.Trim() != string.Empty)
            {

                try
                {
                    DialogResult dr = MessageBox.Show("Are you want to add new skill?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        NghiepVu temp = new NghiepVu();
                        temp.MaNghiepVu = textBox2.Text;
                        temp.MaDonVi = (string)comboBox2.Tag;
                        temp.TenNghiepVu = textBox1.Text;
                        temp.IsDeleted = false;
                        Program.data.NghiepVus.Add(temp);
                        Program.data.SaveChanges();

                        MessageBox.Show("Add new skill is successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Add failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.button2.Click -= Button2_Click;
                    this.button2.Click += button2_Click;

                    this.groupBox1.Enabled = this.groupBox2.Enabled = this.groupBox3.Enabled = true;
                    this.button1.Enabled = this.button3.Enabled = this.button4.Enabled = true;
                    this.comboBox1.Enabled = this.comboBox2.Enabled = false;
                    this.textBox1.Enabled = textBox2.Enabled = false;
                    this.button2.Text = "Add";

                    string maDonvi = (Program.data.DonVis.SingleOrDefault(dv => dv.TenDoi.Equals(listBox2.SelectedValue.ToString()))).MaDonVi;
                    this.listBox3.DataSource = Program.data.NghiepVus.Where(nv => nv.MaDonVi.Equals(maDonvi) & nv.IsDeleted == false).Select(nv => nv.TenNghiepVu).ToList();
                    this.listBox3.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Please fill all textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
