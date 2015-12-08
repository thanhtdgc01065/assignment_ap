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
    public partial class FormDeptTeamManager : Form
    {
        private List<string> listDepartment = new List<string>();

        public FormDeptTeamManager()
        {
            InitializeComponent();
            this.MinimumSize = new Size(800, 450);
        }

        private void FillData(DonVi item)
        {
            this.textBox1.Text = item.TenDoi;
            this.textBox2.Text = item.MaDonVi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FormDeptTeamManager_Load(object sender, EventArgs e)
        {
            listDepartment = Program.data.DonVis
                .Where(dv => dv.IsDeleted == false)
                .OrderBy(dv => dv.TenPhong)
                .Select(dv => dv.TenPhong)
                .Distinct().ToList();
            this.listBox1.DataSource = listDepartment;
            this.comboBox1.DataSource = listDepartment;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBox2.DataSource = Program.data.DonVis
                    .Where(dv => dv.TenPhong.Equals(listBox1.SelectedValue.ToString()) & dv.IsDeleted == false)
                    .OrderBy(dv => dv.TenDoi)
                    .Select(dv => dv.TenDoi).ToList();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DonVi temp = Program.data.DonVis.SingleOrDefault(dv => dv.TenDoi.Equals(listBox2.SelectedValue.ToString()));
            FillData(temp);
            this.listBox2.Tag = temp.MaDonVi;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DonVi temp = Program.data.DonVis.SingleOrDefault(dv => dv.MaDonVi.Equals(listBox2.Tag.ToString()));
            if (temp != null)
            {
                DialogResult dr = MessageBox.Show("Are you sure to delete this team?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        temp.IsDeleted = true;
                        Program.data.SaveChanges();

                        MessageBox.Show("Team delete successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Delete failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = this.groupBox2.Enabled = false;
            this.button1.Enabled = this.button2.Enabled = this.button3.Enabled = false;
            this.button4.Text = "Save";

            this.button4.Click += Button4_Click;
            this.button4.Click -= button4_Click;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to update this team?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DonVi temp = Program.data.DonVis.SingleOrDefault(dv => dv.MaDonVi.Equals(listBox2.Tag));
                    temp.TenPhong = this.comboBox1.SelectedValue.ToString();
                    temp.TenDoi = this.textBox1.Text;
                    temp.MaDonVi = this.textBox2.Text;
                    Program.data.SaveChanges();

                    MessageBox.Show("Update successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.button4.Click -= Button4_Click;
                this.button4.Click += button4_Click;
                this.button4.Text = "Update";
                this.groupBox1.Enabled = this.groupBox2.Enabled = true;
                this.button1.Enabled = this.button2.Enabled = this.button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = this.button4.Enabled = false;
            this.groupBox1.Enabled = this.groupBox2.Enabled = false;

            this.button2.Text = "OK";
            this.button2.Click += Button2_Click;
            this.button2.Click -= button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to add new team?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DonVi temp = new DonVi();
                    temp.TenPhong = this.comboBox1.SelectedValue.ToString();
                    temp.TenDoi = this.textBox1.Text;
                    temp.MaDonVi = this.textBox2.Text;
                    Program.data.DonVis.Add(temp);
                    Program.data.SaveChanges();

                    MessageBox.Show("Add new team successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Add new team failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.button3.Enabled = this.button4.Enabled = true;
                this.groupBox1.Enabled = this.groupBox2.Enabled = true;

                this.button2.Text = "Add";
                this.button2.Click -= Button2_Click;
                this.button2.Click += button2_Click;
            }
        }
    }
}
