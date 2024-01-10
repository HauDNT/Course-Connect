using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect
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
            Application.Run(new DangNhap_DangKy.DangNhap());
        }

        public static string setNewID(string ID, string tableName, string LoaiTK)
        {
            string oldID = "";
            string newID = "";

            string select = "SELECT " + ID + " FROM " + tableName + " ORDER BY " + ID + " DESC";

            DatabaseInteract.command = new SqlCommand(select, DatabaseInteract.connect);
            object result = DatabaseInteract.command.ExecuteScalar();

            if (result == null)
                newID = LoaiTK + "001";
            else
            {
                oldID = new string(result.ToString().Where(char.IsDigit).ToArray());
                newID = LoaiTK + (Int32.Parse(oldID) + 1).ToString("D3");
            }

            return newID;
        }
    }
}
