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

namespace Course_Connect.DangNhap_DangKy
{
    public partial class ThongTinHocVien : Form
    {
        public ThongTinHocVien()
        {
            InitializeComponent();
        }

        bool checkThongTin()
        {
            if (txtHoTen.Text == "" || txt_Email.Text == "" || txt_SDT.Text == "" || txt_Lop.Text == "")
            {
                MessageBox.Show("Hãy điền đầy đủ các trường thông tin", "Thông báo");
                return false;
            }

            if (rdb_Nam.Checked == false && rdb_Nu.Checked == false)
            {
                MessageBox.Show("Hãy chọn giới tính của bạn", "Thông báo");
                return false;
            }

            if (!dateTime_NgaySinh.Checked)
            {
                MessageBox.Show("Hãy chọn ngày tháng năm sinh", "Thông báo");
                return false;
            }

            if (txt_SDT.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại vượt quá lượng ký tự cho phép (10 - 11 ký tự)", "Thông báo");
                return false;
            }

            return true;
        }

        void ThemTaiKhoan()
        {
            DatabaseInteract.connect.Open();

            string maTK = Program.setNewID("maTK", "TaiKhoanHocVien", "HV");

            // Thêm tài khoản vào bảng TaiKhoanHocVien:
            DatabaseInteract.insert = "INSERT INTO TaiKhoanHocVien VALUES ('"
                                    + maTK + "', '"
                                    + DangKy.username + "', '"
                                    + DangKy.password + "')";

            DatabaseInteract.command = new SqlCommand(DatabaseInteract.insert, DatabaseInteract.connect);
            DatabaseInteract.command.ExecuteNonQuery();

            // Thêm thông tin của học viên vào bảng ThongTinHocVien:
            DatabaseInteract.insert = "INSERT INTO ThongTinHocVien VALUES ('"
                 + maTK + "', N'"
                 + FormValidate.FormatString(txtHoTen.Text) + "', N'"
                 + (rdb_Nam.Checked ? "Nam" : "Nữ") + "', '"
                 + String.Format("{0:yyyy/MM/dd}", dateTime_NgaySinh.Value) + "', N'"
                 + txt_Lop.Text.Trim() + "', '"
                 + txt_Email.Text.Trim() + "', '"
                 + txt_SDT.Text.Trim() + "')";

            DatabaseInteract.command = new SqlCommand(DatabaseInteract.insert, DatabaseInteract.connect);
            DatabaseInteract.command.ExecuteNonQuery();

            DatabaseInteract.connect.Close();

            MessageBox.Show("Đăng ký thành công, bạn có thể đăng nhập vào ứng dụng ngay bây giờ!", "Thông báo");

            DangNhap newForm = new DangNhap();
            newForm.Show();
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            if (checkThongTin())
            {
                // Xem lại thông tin & xác nhận:
                DialogResult comfirmInfo = MessageBox.Show("Thông tin của bạn:" +
                                            "\n- Tên tài khoản: " + DangKy.username +
                                            "\n- Mật khẩu: " + DangKy.password +
                                            "\n- Họ và tên: " + FormValidate.FormatString(txtHoTen.Text) +
                                            "\n- Giới tính: " + (rdb_Nam.Checked ? "Nam" : "Nữ") +
                                            "\n- Ngày sinh: " + String.Format("{0:yyyy/MM/dd}", dateTime_NgaySinh.Value) +
                                            "\n- Lớp: " + txt_Lop.Text.Trim() +
                                            "\n- Email: " + txt_Email.Text.Trim() +
                                            "\n- Số điện thoại: " + txt_SDT.Text.Trim(),
                                    "Xem lại thông tin",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (comfirmInfo == DialogResult.Yes)
                    ThemTaiKhoan();
            }
        }
    }
}
