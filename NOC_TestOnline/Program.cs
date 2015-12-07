using NOC_TestOnline.model;
using NOC_TestOnline.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOC_TestOnline
{
    static class Program
    {
        public static DataModel data = new DataModel();
        public static TaiKhoan currentUser = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormLogin f = new FormLogin(true);
            f.ShowDialog();
            if (f.checkLoggedIn())
            {
                currentUser = f.getUser();
                Application.Run(new FormMain());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
