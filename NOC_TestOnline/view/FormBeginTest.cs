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
    public partial class FormBeginTest : Form
    {
        List<CauHoi> listCauHoi = new List<CauHoi>();
        int[] checkedList = new int[3];
        int questionPos = 0;
        int minuteCountdown = 3;
        int secondCountdown = 0;
        DeThi deThi = null;
        BaiThi baiThi = null;
        KetQua ketQua = null;
        int diemSo = 0;

        public FormBeginTest()
        {
            InitializeComponent();
            this.radioButton1.Tag = 1;
            this.radioButton2.Tag = 2;
            this.radioButton3.Tag = 3;

            this.MaximizeBox = false;

            FormPutExamId f = new FormPutExamId();
            f.ShowDialog();
            deThi = f.getDeThi();

            //Get question list
            listCauHoi.Add(Program.data.CauHois.Single(ch => ch.MaCauHoi == deThi.MaCauHoi1));
            listCauHoi.Add(Program.data.CauHois.Single(ch => ch.MaCauHoi == deThi.MaCauHoi2));
            listCauHoi.Add(Program.data.CauHois.Single(ch => ch.MaCauHoi == deThi.MaCauHoi3));
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("1");
            if (secondCountdown == -1)
            {
                secondCountdown = 59;
                minuteCountdown--;
            }
            if (secondCountdown == 0 && minuteCountdown == 0)
            {
                timer1.Stop();
                ShowResult();
                Dispose();
            }
            label1.Text = (minuteCountdown < 10 ? "0" + minuteCountdown.ToString() : minuteCountdown.ToString()) + ":" + (secondCountdown < 10 ? "0" + secondCountdown.ToString() : secondCountdown.ToString());
            secondCountdown--;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = true;

            var temp = this.groupBox2.Controls.OfType<RadioButton>().SingleOrDefault(n => n.Checked);
            if (temp != null)
            {
                int selectedRadionBtn = Int32.Parse(temp.Tag.ToString());
                checkedList[questionPos] = (selectedRadionBtn);
                temp.Checked = false;
            }
            FillData(listCauHoi[++questionPos], checkedList[questionPos]);

            if (questionPos == 2)
            {
                this.button1.Text = "End Exam";
                this.button1.Click += EndExam;
                this.button1.Click -= button1_Click;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (questionPos == 0)
            {
                this.button2.Enabled = false;
            }
            else
            {
                if (questionPos == 2)
                {
                    this.button1.Text = "Next";
                    this.button1.Click += button1_Click;
                    this.button1.Click -= EndExam;
                }

                var temp = this.groupBox2.Controls.OfType<RadioButton>().SingleOrDefault(n => n.Checked);
                if (temp != null)
                {
                    int selectedRadionBtn = Int32.Parse(temp.Tag.ToString());
                    checkedList[questionPos] = selectedRadionBtn;
                }
                FillData(listCauHoi[--questionPos], checkedList[questionPos]);

                if (questionPos == 0)
                {
                    this.button2.Enabled = false;
                }
            }
        }

        private void FormBeginTest_Load(object sender, EventArgs e)
        {
            FillData(listCauHoi[questionPos], checkedList[questionPos]);

            if (questionPos == 0)
            {
                this.button2.Enabled = false;
            }
        }

        private void EndExam(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure want to end exam?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                ShowResult();
                Dispose();
            }
        }

        private void FillData(CauHoi item, int selected)
        {
            this.richTextBox1.Text = item.CauHoi1;
            this.richTextBox2.Text = item.TraLoi1;
            this.richTextBox3.Text = item.TraLoi2;
            this.richTextBox4.Text = item.TraLoi3;

            //re-checked the radio button
            if (selected != 0)
            {
                (this.groupBox2.Controls.OfType<RadioButton>().Single(n => Int32.Parse(n.Tag.ToString()) == selected)).Checked = true;
            }

            //set all textboxes read-only
            this.richTextBox1.ReadOnly = true;
            this.richTextBox2.ReadOnly = true;
            this.richTextBox3.ReadOnly = true;
            this.richTextBox4.ReadOnly = true;
        }

        public void ShowResult()
        {
            string text = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                if (checkedList[i] == listCauHoi[i].DapAn)
                {
                    diemSo += 1;
                }
            }

            if (diemSo >= 2)
            {
                text = "Your mark is: " + diemSo + "/" + checkedList.Count() + ". " + "\nCongratulation. You pass the exam!";
            }
            else
            {
                text = "Your mark is: " + diemSo + "/" + checkedList.Count() + ". " + "\nYou're so good. But sorry, You're failed. See you soon!";
            }
            SaveResult();
            MessageBox.Show("Exam ended." + " " + text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormBeginTest_Activated(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you ready?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                timer1.Start();
            }
        }

        private void SaveResult()
        {
            baiThi = new BaiThi();
            baiThi.MaDeThi = deThi.MaDeThi;
            baiThi.NgayThi = DateTime.Now;
            baiThi.TenTaiKhoan = Program.currentUser.TenTaiKhoan;
            baiThi.IsDeleted = false;
            Program.data.BaiThis.Add(baiThi);

            ketQua = new KetQua();
            ketQua.MaBaiThi = baiThi.MaBaiThi;
            ketQua.DiemSo = diemSo;
            ketQua.IsDeleted = false;
            Program.data.KetQuas.Add(ketQua);

            Program.data.SaveChanges();
        }
    }
}
