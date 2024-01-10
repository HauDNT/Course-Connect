using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Course_Connect.ChuongTrinhChinh;

namespace Course_Connect.DangNhap_DangKy
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Mở cổng 1 lần - Xài cho toàn chương trình:
            DatabaseInteract.connect.Open();

            // Lấy tài khoản trùng khớp trong bảng quản trị viên (nếu có):
            DatabaseInteract.query = "SELECT COUNT(*) FROM TaiKhoanQTV WHERE TenTK = '" + txtTaiKhoan.Text + "' AND MatKhau = '" + txtMatKhau.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int countAccountQTV = (int)DatabaseInteract.command.ExecuteScalar();

            // Lấy tài khoản trùng khớp trong bảng giảng viên (nếu có):
            DatabaseInteract.query = "SELECT COUNT(*) FROM TaiKhoanGiangVien WHERE username = '" + txtTaiKhoan.Text + "' AND password = '" + txtMatKhau.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int countAccountGV = (int)DatabaseInteract.command.ExecuteScalar();

            // Lấy tài khoản trùng khớp trong bảng học viên (nếu có):
            DatabaseInteract.query = "SELECT COUNT(*) FROM TaiKhoanHocVien WHERE username = '" + txtTaiKhoan.Text + "' AND password = '" + txtMatKhau.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int countAccountHV = (int)DatabaseInteract.command.ExecuteScalar();

            // Kiểm tra & xử lý:
            if (countAccountQTV == 1)
            {
                this.Hide();
                Decentralization.thisUsername = txtTaiKhoan.Text;
                Decentralization.accountType = 1;

                GiaoDienChinh newForm = new GiaoDienChinh();
                newForm.ShowDialog();
            }

            else if (countAccountGV == 1)
            {
                this.Hide();
                Decentralization.thisUsername = txtTaiKhoan.Text;
                Decentralization.accountType = 2;
                Decentralization.thisUserAccountCode = Decentralization.LayMaNguoiDung(Decentralization.thisUsername, Decentralization.accountType);

                ChuongTrinhChinh_GiangVien.GiaoDienNguoiDung newForm = new ChuongTrinhChinh_GiangVien.GiaoDienNguoiDung();
                newForm.ShowDialog();
            }

            else if (countAccountHV == 1)
            {
                this.Hide();

                Decentralization.thisUsername = txtTaiKhoan.Text;
                Decentralization.accountType = 3;
                Decentralization.thisUserAccountCode = Decentralization.LayMaNguoiDung(Decentralization.thisUsername, Decentralization.accountType);

                ChuongTrinhChinh_HocVien.GiaoDienNguoiDung newForm = new ChuongTrinhChinh_HocVien.GiaoDienNguoiDung();
                newForm.ShowDialog();
            }
            else
                MessageBox.Show("Hãy kiểm tra lại tài khoản và mật khẩu!");

            DatabaseInteract.connect.Close();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            this.Hide();

            DangKy newForm = new DangKy();
            newForm.ShowDialog();

            this.Show();
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình?",
                                                  "Thông báo",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
            else if (result == DialogResult.Yes)
                Application.Exit();
        }
    }
}
