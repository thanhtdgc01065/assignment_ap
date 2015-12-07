using NOC_TestOnline.model;
using NOC_TestOnline.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace NOC_TestOnline.view
{
    public partial class FormLogin : Form
    {
        private bool isLoggedIn = false;
        private TaiKhoan taiKhoan = null;
        ListDictionary listAccount = new ListDictionary();

        public FormLogin(bool newStart)
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            //Default button when press Enter key
            this.AcceptButton = button2;
            if (newStart)
            {
                //When login
                this.textBox3.Visible = this.label3.Visible = false;
            }
            else
            {
                //When change password
                this.Text = "Change Password.";
                this.label1.Text = "Current Password";
                this.textBox1.UseSystemPasswordChar = true;

                this.button2.Text = "Change";
                this.button2.Click += changePassClick;
                this.button2.Click -= button2_Click;
            }

            this.MaximizeBox = false;
        }

        private void changePassClick(object sender, EventArgs e)
        {
            /*
            try
            {
                if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
                {
                    MessageBox.Show("Please fill all text box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (EncryptData.MD5Hash(textBox1.Text).Equals(Program.currentUser.MatKhau))
                    {
                        if (textBox2.Text.Equals(textBox3.Text) && textBox2.Text != string.Empty)
                        {
                            Program.currentUser.MatKhau = EncryptData.MD5Hash(textBox2.Text);
                            Program.data.SaveChanges();
                            MessageBox.Show("Change password successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.button2.Click += button2_Click;
                            this.button2.Click -= changePassClick;

                            Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Two new password is not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Current password is not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Change password failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            if (textBox1.Text.Trim().Equals(string.Empty) || textBox2.Text.Trim().Equals(string.Empty) || textBox3.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please fill all textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    if (EncryptData.MD5Hash(textBox1.Text).Equals(Program.currentUser.MatKhau))
                    {
                        DialogResult dr = MessageBox.Show("Are you sure want to change password?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                Program.currentUser.MatKhau = EncryptData.MD5Hash(textBox2.Text);
                                Program.data.SaveChanges();

                                MessageBox.Show("Change password successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                MessageBox.Show("Change password failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                this.button2.Click += button2_Click;
                                this.button2.Click -= changePassClick;

                                Dispose();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("New password does not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                taiKhoan = Program.data.TaiKhoans.Single(tk => tk.TenTaiKhoan.Equals(textBox1.Text));
                if (taiKhoan != null)
                {
                    if (textBox2.Text == string.Empty)
                    {
                        MessageBox.Show("Please enter the password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (EncryptData.MD5Hash(textBox2.Text).Equals(taiKhoan.MatKhau))
                        {
                            isLoggedIn = true;
                            Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.textBox2.Text = string.Empty;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Username is not correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */

            if (listAccount.Contains(textBox1.Text))
            {
                if (EncryptData.MD5Hash(textBox2.Text).Equals(listAccount[textBox1.Text]))
                {
                    isLoggedIn = true;
                    taiKhoan = Program.data.TaiKhoans.Single(tk => tk.TenTaiKhoan.Equals(textBox1.Text));
                    Dispose();
                }
                else
                {
                    MessageBox.Show("Wrong password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.textBox2.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Username is not correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool checkLoggedIn()
        {
            return isLoggedIn;
        }

        public TaiKhoan getUser()
        {
            return taiKhoan;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var temp = Program.data.TaiKhoans.ToList();
            foreach (var i in temp)
            {
                listAccount.Add((string)i.TenTaiKhoan, (string)i.MatKhau);
            }
        }
    }
}
