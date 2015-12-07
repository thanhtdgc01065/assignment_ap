using NOC_TestOnline.controller;
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
    public partial class FormExamManager : Form
    {
        public FormExamManager()
        {
            InitializeComponent();
            FillData();
            this.button1.Enabled = false;
            this.button3.Enabled = false;

            this.MaximizeBox = false;
        }

        private void FillData()
        {
            //List of skill
            this.comboBox1.DataSource = Program.data.vwFullExams.Select(nv => nv.TenNghiepVu).Distinct().ToList();

            //this.comboBox1.SelectedIndex = -1;

            //Set all richtextbox read-only
            this.richTextBox1.ReadOnly = true;
            this.richTextBox2.ReadOnly = true;
            this.richTextBox3.ReadOnly = true;
            this.richTextBox4.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FormExamManager_Load(object sender, EventArgs e)
        {
            this.listBox1.DataSource = Program.data.vwFullExams.Select(n => n.MaDeThi).ToList();

            this.listBox1.SelectedIndex = -1;
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var tempDeThi = Program.data.vwFullExams.SingleOrDefault(dt => dt.MaDeThi.Equals(listBox1.SelectedItem.ToString()));

                List<CauHoi> listCauHoi = new List<CauHoi>();
                listCauHoi.Add(Program.data.CauHois.SingleOrDefault(ch => ch.MaCauHoi == tempDeThi.MaCauHoi1));
                listCauHoi.Add(Program.data.CauHois.SingleOrDefault(ch => ch.MaCauHoi == tempDeThi.MaCauHoi2));
                listCauHoi.Add(Program.data.CauHois.SingleOrDefault(ch => ch.MaCauHoi == tempDeThi.MaCauHoi3));

                listBox2.DataSource = listCauHoi.Select(ch => ch.MaCauHoi).ToList();

                listBox2.SelectedIndex = -1;

                this.button3.Enabled = true;
            }
            catch
            {

            }
        }

        private void FillQuestion(CauHoi item)
        {
            this.label7.Text = item.MaCauHoi.ToString();
            this.richTextBox1.Text = item.CauHoi1;
            this.richTextBox2.Text = item.TraLoi1;
            this.richTextBox3.Text = item.TraLoi2;
            this.richTextBox4.Text = item.TraLoi3;
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int idCauHoi = Int32.Parse(listBox2.SelectedValue.ToString());
                CauHoi temp = Program.data.CauHois.SingleOrDefault(ch => ch.MaCauHoi == idCauHoi);
                FillQuestion(temp);
            }
            catch
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var temp = Program.data.vwFullExams.Where(n => n.TenNghiepVu.Equals(comboBox1.SelectedValue.ToString())).Select(n => n.MaDeThi);
                this.listBox1.DataSource = temp.ToList();

                this.listBox1.SelectedIndex = -1;

                this.button1.Enabled = true;
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var temp = Program.data.NghiepVus.SingleOrDefault(n => n.TenNghiepVu.Equals(comboBox1.SelectedValue.ToString()));
                DialogResult dr = MessageBox.Show("Do you want to add new Exam to Skill: " + temp.TenNghiepVu + "?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DeThi tempDeThi = new DeThi();
                    tempDeThi.MaDeThi = temp.MaNghiepVu + (listBox1.Items.Count + 1).ToString();
                    tempDeThi.MaNghiepVu = temp.MaNghiepVu;

                    List<int> listRandomId = RandomPosition.getRandomQuestionPosition(Program.data.CauHois.Where(ch => ch.MaNghiepVu.Equals(tempDeThi.MaNghiepVu)).Select(ma => ma.MaCauHoi).ToList());
                    tempDeThi.MaCauHoi1 = listRandomId[0];
                    tempDeThi.MaCauHoi2 = listRandomId[1];
                    tempDeThi.MaCauHoi3 = listRandomId[2];
                    tempDeThi.IsDeleted = false;

                    Program.data.DeThis.Add(tempDeThi);
                    Program.data.SaveChanges();

                    MessageBox.Show("Insert new Exam for skill " + temp.TenNghiepVu + " is successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                }
            }
            catch
            {
                MessageBox.Show("Insert new Exam is not successfully. Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DeThi temp = Program.data.DeThis.SingleOrDefault(dt => dt.MaDeThi.Equals(listBox1.SelectedValue.ToString()));
                DialogResult dr = MessageBox.Show("Do you want to delete this Exam?", "Delete Exam", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Program.data.DeThis.Remove(temp);
                    Program.data.SaveChanges();

                    MessageBox.Show("Delete successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillData();
                }
            }
            catch
            {
                MessageBox.Show("Delete failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
