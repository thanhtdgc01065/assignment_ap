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
    public partial class FormPutExamId : Form
    {
        private DeThi deThi = null;

        public FormPutExamId()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AcceptButton = button1;

            this.MaximizeBox = false;
        }

        public DeThi getDeThi()
        {
            return deThi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deThi = Program.data.DeThis.SingleOrDefault(dt => dt.MaDeThi.Equals(textBox1.Text.ToUpper()));
            if (deThi == null)
            {
                MessageBox.Show("Wrong exam ID. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox1.Text = string.Empty;
            }
            else
            {
                Dispose();
            }
        }
    }
}
