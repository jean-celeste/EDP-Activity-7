using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDP_GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginSignupForm());

            /*string connectionString = "Server=localhost;Database=books_db;User Id=root;Password=1234;";
            DatabaseConnection dbConn = new DatabaseConnection(connectionString);
            LoginSignupForm loginForm = new LoginSignupForm();
            Application.Run(loginForm);*/
        }
    }
}
