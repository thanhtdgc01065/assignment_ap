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
    public partial class FormQuestionManager : Form
    {
        List<string> listNghiepVu = new List<string>();

        public FormQuestionManager()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void SetupTable()
        {
            this.dataGridView1.Columns[0].HeaderText = "Question ID";
            this.dataGridView1.Columns[1].HeaderText = "Skill";
            this.dataGridView1.Columns[2].HeaderText = "Question";
            this.dataGridView1.Columns[3].HeaderText = "Choice 1";
            this.dataGridView1.Columns[4].HeaderText = "Choice 2";
            this.dataGridView1.Columns[5].HeaderText = "Choise 3";
            this.dataGridView1.Columns[6].HeaderText = "Answer";

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void FillTextBox(CauHoi temp = null)
        {
            if (temp != null)
            {
                this.richTextBox1.Text = temp.CauHoi1;
                this.richTextBox2.Text = temp.TraLoi1;
                this.richTextBox3.Text = temp.TraLoi2;
                this.richTextBox4.Text = temp.TraLoi3;
                this.textBox1.Text = temp.DapAn.ToString();
            }
            else
            {
                this.richTextBox1.Text = string.Empty;
                this.richTextBox2.Text = string.Empty;
                this.richTextBox3.Text = string.Empty;
                this.richTextBox4.Text = string.Empty;
                this.textBox1.Text = string.Empty;
            }
        }

        private void ViewAllQuestion()
        {
            var allQuestionInfo = Program.data.vwFullQuestionDetails.Select(ch => new
            {
                maCauHoi = ch.MaCauHoi,
                nghiepVu = ch.TenNghiepVu,
                cauHoi = ch.CauHoi,
                traLoi1 = ch.TraLoi1,
                traLoi2 = ch.TraLoi2,
                traLoi3 = ch.TraLoi3,
                dapAn = ch.DapAn
            });
            this.dataGridView1.DataSource = allQuestionInfo.ToList();
            SetupTable();
        }

        private void FormQuestionManager_Load(object sender, EventArgs e)
        {
            listNghiepVu.Add("All");
            listNghiepVu.AddRange(Program.data.NghiepVus.Where(nv => nv.IsDeleted == false).Select(nv => nv.TenNghiepVu).ToList());
            this.comboBox1.DataSource = listNghiepVu;

            this.button4.Enabled = false;
            this.button4.Click += Button4_Click;
        }

        private void ClearAllText()
        {
            this.richTextBox1.Text = string.Empty;
            this.richTextBox2.Text = string.Empty;
            this.richTextBox3.Text = string.Empty;
            this.richTextBox4.Text = string.Empty;
            this.textBox1.Text = string.Empty;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;
            this.button3.Enabled = false;

            ClearAllText();

            this.button4.Click += Button4_Click1;
            this.button4.Click -= Button4_Click;
        }

        private void Button4_Click1(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Do you want to create new question?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (richTextBox1.Text.Equals(string.Empty) || richTextBox2.Text.Equals(string.Empty) ||
                        richTextBox3.Text.Equals(string.Empty) || richTextBox4.Text.Equals(string.Empty) ||
                        textBox1.Text.Equals(string.Empty))
                    {
                        MessageBox.Show("Please fill all text box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        CauHoi item = new CauHoi();
                        string maNghiepVu = (Program.data.NghiepVus.SingleOrDefault(nv => nv.TenNghiepVu.Equals(comboBox1.SelectedValue.ToString()))).MaNghiepVu;
                        item.MaNghiepVu = maNghiepVu;
                        item.CauHoi1 = richTextBox1.Text;
                        item.TraLoi1 = richTextBox2.Text;
                        item.TraLoi2 = richTextBox3.Text;
                        item.TraLoi3 = richTextBox4.Text;
                        if (Int32.Parse(textBox1.Text) > 3 || Int32.Parse(textBox1.Text) <= 0)
                        {
                            MessageBox.Show("Invalid data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            item.DapAn = Int32.Parse(textBox1.Text);
                            item.IsDeleted = false;

                            Program.data.CauHois.Add(item);
                            Program.data.SaveChanges();

                            SetupTable();

                            MessageBox.Show("Create successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ClearAllText();
                            this.button4.Click += Button4_Click;
                            this.button4.Click -= Button4_Click1;
                            this.button2.Enabled = true;
                            this.button3.Enabled = true;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Create new question failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int questionId = Int32.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            CauHoi temp = Program.data.CauHois.Single(ch => ch.MaCauHoi == questionId);
            FillTextBox(temp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure want to delete this question?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int questionId = Int32.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    CauHoi cauHoi = Program.data.CauHois.SingleOrDefault(ch => ch.MaCauHoi == questionId);
                    cauHoi.IsDeleted = true;
                    Program.data.SaveChanges();

                    MessageBox.Show("Delete question successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetupTable();
                    FillTextBox();
                }
            }
            catch
            {
                MessageBox.Show("Delete question failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int questionId = Int32.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                CauHoi cauHoi = Program.data.CauHois.SingleOrDefault(ch => ch.MaCauHoi == questionId);

                cauHoi.CauHoi1 = richTextBox1.Text;
                cauHoi.TraLoi1 = richTextBox2.Text;
                cauHoi.TraLoi2 = richTextBox3.Text;
                cauHoi.TraLoi3 = richTextBox4.Text;
                cauHoi.DapAn = Int32.Parse(textBox1.Text);
                Program.data.SaveChanges();

                MessageBox.Show("Update question successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetupTable();
                FillTextBox(cauHoi);
            }
            catch
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                ViewAllQuestion();
            }
            else
            {
                var item = Program.data.NghiepVus.SingleOrDefault(nv => nv.TenNghiepVu.Equals(comboBox1.SelectedValue.ToString()));
                this.dataGridView1.DataSource = Program.data.vwFullQuestionDetails.Where(ch => ch.MaNghiepVu.Equals(item.MaNghiepVu)).Select(ch => new
                {
                    maCauHoi = ch.MaCauHoi,
                    nghiepVu = ch.TenNghiepVu,
                    cauHoi = ch.CauHoi,
                    traLoi1 = ch.TraLoi1,
                    traLoi2 = ch.TraLoi2,
                    traLoi3 = ch.TraLoi3,
                    dapAn = ch.DapAn
                }).ToList();
                SetupTable();
                this.button4.Enabled = true;
            }
        }
    }
}
