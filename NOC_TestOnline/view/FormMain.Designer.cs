namespace NOC_TestOnline.view
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ẽitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountManagerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.examManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionManagerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.skillManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.testResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.departmentTeamManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.managementToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator1,
            this.ẽitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // ẽitToolStripMenuItem
            // 
            this.ẽitToolStripMenuItem.Name = "ẽitToolStripMenuItem";
            this.ẽitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ẽitToolStripMenuItem.Text = "Exit";
            this.ẽitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // managementToolStripMenuItem
            // 
            this.managementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountManagerToolStripMenuItem1,
            this.departmentTeamManagerToolStripMenuItem,
            this.skillManagerToolStripMenuItem,
            this.toolStripSeparator3,
            this.examManagerToolStripMenuItem,
            this.questionManagerToolStripMenuItem1});
            this.managementToolStripMenuItem.Name = "managementToolStripMenuItem";
            this.managementToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.managementToolStripMenuItem.Text = "Management";
            // 
            // accountManagerToolStripMenuItem1
            // 
            this.accountManagerToolStripMenuItem1.Name = "accountManagerToolStripMenuItem1";
            this.accountManagerToolStripMenuItem1.Size = new System.Drawing.Size(242, 22);
            this.accountManagerToolStripMenuItem1.Text = "Account Manager";
            this.accountManagerToolStripMenuItem1.Click += new System.EventHandler(this.accountManagerToolStripMenuItem1_Click);
            // 
            // examManagerToolStripMenuItem
            // 
            this.examManagerToolStripMenuItem.Name = "examManagerToolStripMenuItem";
            this.examManagerToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.examManagerToolStripMenuItem.Text = "Exam Manager";
            this.examManagerToolStripMenuItem.Click += new System.EventHandler(this.examManagerToolStripMenuItem_Click);
            // 
            // questionManagerToolStripMenuItem1
            // 
            this.questionManagerToolStripMenuItem1.Name = "questionManagerToolStripMenuItem1";
            this.questionManagerToolStripMenuItem1.Size = new System.Drawing.Size(242, 22);
            this.questionManagerToolStripMenuItem1.Text = "Question Manager";
            this.questionManagerToolStripMenuItem1.Click += new System.EventHandler(this.questionManagerToolStripMenuItem1_Click);
            // 
            // skillManagerToolStripMenuItem
            // 
            this.skillManagerToolStripMenuItem.Name = "skillManagerToolStripMenuItem";
            this.skillManagerToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.skillManagerToolStripMenuItem.Text = "Skill Manager";
            this.skillManagerToolStripMenuItem.Click += new System.EventHandler(this.skillManagerToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginTestToolStripMenuItem,
            this.toolStripSeparator2,
            this.testResultToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // beginTestToolStripMenuItem
            // 
            this.beginTestToolStripMenuItem.Name = "beginTestToolStripMenuItem";
            this.beginTestToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.beginTestToolStripMenuItem.Text = "Begin Test";
            this.beginTestToolStripMenuItem.Click += new System.EventHandler(this.beginTestToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(127, 6);
            // 
            // testResultToolStripMenuItem
            // 
            this.testResultToolStripMenuItem.Name = "testResultToolStripMenuItem";
            this.testResultToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.testResultToolStripMenuItem.Text = "Test Result";
            this.testResultToolStripMenuItem.Click += new System.EventHandler(this.testResultToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(239, 6);
            // 
            // departmentTeamManagerToolStripMenuItem
            // 
            this.departmentTeamManagerToolStripMenuItem.Name = "departmentTeamManagerToolStripMenuItem";
            this.departmentTeamManagerToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.departmentTeamManagerToolStripMenuItem.Text = "Department and Team Manager";
            this.departmentTeamManagerToolStripMenuItem.Click += new System.EventHandler(this.departmentTeamManagerToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "NOC TEST ONLINE";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ẽitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountManagerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem questionManagerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem examManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skillManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testResultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem departmentTeamManagerToolStripMenuItem;
    }
}