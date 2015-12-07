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
    public partial class FormMain : Form
    {
        private FormAccountManager accountManager = null;
        private FormQuestionManager questionManager = null;
        private FormExamManager examManager = null;
        private FormBeginTest beginTest = null;
        private FormLogin login = null;
        private FormSkillManager skillManager = null;
        private FormResult result = null;

        public FormMain()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;

            if (!(Program.currentUser.MaDonVi.Equals("ADMIN")))
            {
                menuStrip1.Items[1].Visible = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void beginTestToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (beginTest == null)
            {
                beginTest = new FormBeginTest();
                beginTest.MdiParent = this;
            }
            beginTest.Show();
        }

        private void accountManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (accountManager == null)
            {
                accountManager = new FormAccountManager();
                accountManager.MdiParent = this;
            }
            accountManager.Show();
        }

        private void questionManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (questionManager == null)
            {
                questionManager = new FormQuestionManager();
                questionManager.MdiParent = this;
            }
            questionManager.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (login == null)
            {
                login = new FormLogin(false);
                login.MdiParent = this;
            }
            login.Show();
        }

        private void examManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (examManager == null)
            {
                examManager = new FormExamManager();
                examManager.MdiParent = this;
            }
            examManager.Show();
        }

        private void skillManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (skillManager == null)
            {
                skillManager = new FormSkillManager();
                skillManager.MdiParent = this;
            }
            skillManager.Show();
        }

        private void testResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (result == null)
            {
                result = new FormResult();
                result.MdiParent = this;
            }
            result.Show();
        }
    }
}
