using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Connect
{
    internal class DatabaseInteract
    {
        // DESKTOP-H83VP1E\SQLEXPRESS
        public static string connectString = @"Data Source=DESKTOP-H83VP1E\SQLEXPRESS;Initial Catalog=ConnectCourse;Integrated Security=True";
        public static SqlConnection connect = new SqlConnection(connectString);
        public static SqlCommand command;
        public static SqlDataReader reader;
        public static SqlDataAdapter adapter;
        public static string query;
        public static string insert;
        public static string update;
        public static string delete;
    }
}
