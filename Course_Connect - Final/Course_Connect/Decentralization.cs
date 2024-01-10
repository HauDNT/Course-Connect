using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Course_Connect
{
    class Decentralization
    {
        public static string thisUsername;
        public static string thisUserAccountCode;
        public static int accountType;
        // QTV - 1
        // GV - 2
        // HV - 3

        public static string LayHoTenNguoiDung(string username, int accountType)
        {
            string fullName = "";

            if (accountType == 2)
            {
                DatabaseInteract.query = "SELECT HoTen " +
                                         "FROM TaiKhoanGiangVien, ThongTinGiangVien " +
                                         "WHERE TaiKhoanGiangVien.maTK = ThongTinGiangVien.MaGV " +
                                         "AND TaiKhoanGiangVien.username = '" + username + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                fullName = DatabaseInteract.command.ExecuteScalar().ToString();
            }

            else if (accountType == 3)
            {
                DatabaseInteract.query = "SELECT HoTen " +
                                         "FROM TaiKhoanHocVien, ThongTinHocVien " +
                                         "WHERE TaiKhoanHocVien.maTK = ThongTinHocVien.MaHV " +
                                         "AND TaiKhoanHocVien.username = '" + username + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                fullName = DatabaseInteract.command.ExecuteScalar().ToString();
            }

            return fullName;
        }

        public static string LayMaNguoiDung(string username, int accountType)
        {
            string userCode = "";

            if (accountType == 2)
            {
                DatabaseInteract.query = "SELECT MaGV " +
                                         "FROM TaiKhoanGiangVien, ThongTinGiangVien " +
                                         "WHERE TaiKhoanGiangVien.maTK = ThongTinGiangVien.MaGV " +
                                         "AND TaiKhoanGiangVien.username = '" + username + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                userCode = DatabaseInteract.command.ExecuteScalar().ToString();
            }

            else if (accountType == 3)
            {
                DatabaseInteract.query = "SELECT MaHV " +
                                         "FROM TaiKhoanHocVien, ThongTinHocVien " +
                                         "WHERE TaiKhoanHocVien.maTK = ThongTinHocVien.MaHV " +
                                         "AND TaiKhoanHocVien.username = '" + username + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                userCode = DatabaseInteract.command.ExecuteScalar().ToString();
            }

            return userCode;
        }
    }
}
