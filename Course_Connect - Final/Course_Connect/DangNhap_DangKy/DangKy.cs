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
    public partial class DangKy : Form
    {
        public static string username;
        public static string password;

        public DangKy()
        {
            InitializeComponent();
        }

        bool checkThongTin()
        {
            if (txtTaiKhoan.Text == "" || txtMatKhau.Text == "" || txtNhapLaiMatKhau.Text == "")
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu không trùng khớp, hãy kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtTaiKhoan.Text.Length < 8)
            {
                MessageBox.Show("Độ dài tên tài khoản phải từ 8 ký tự trở lên", "Thông báo");
                return false;
            }
            else if (txtMatKhau.Text.Length < 8)
            {
                MessageBox.Show("Độ dài mật khẩu phải từ 8 ký tự trở lên", "Thông báo");
                return false;
            }
            else if (!select_HocVien.Checked && !select_GiangVien.Checked)
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản", "Thông báo");
                return false;
            }

            // Kiểm tra trùng lặp tên TK trong CSDL:
            else
            {
                // Nếu chọn tài khoản giảng viên:
                if (select_GiangVien.Checked)
                {
                    string checkAccount = "SELECT COUNT(*) FROM TaiKhoanGiangVien WHERE username = '" + txtTaiKhoan.Text + "'";

                    DatabaseInteract.connect.Open();
                    DatabaseInteract.command = new SqlCommand(checkAccount, DatabaseInteract.connect);

                    int countAccount = (int)DatabaseInteract.command.ExecuteScalar();

                    DatabaseInteract.connect.Close();

                    if (countAccount == 1)
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // Nếu chọn tài khoản học viên:
                else if (select_HocVien.Checked)
                {
                    string checkAccount = "SELECT COUNT(*) FROM TaiKhoanHocVien WHERE username = '" + txtTaiKhoan.Text + "'";

                    DatabaseInteract.connect.Open();
                    DatabaseInteract.command = new SqlCommand(checkAccount, DatabaseInteract.connect);

                    int countAccount = (int)DatabaseInteract.command.ExecuteScalar();

                    DatabaseInteract.connect.Close();

                    if (countAccount == 1)
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return true;
            }
        }

        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            if (checkThongTin())
            {
                username = txtTaiKhoan.Text;
                password = txtMatKhau.Text;

                this.Hide();
                if (select_GiangVien.Checked)
                {
                    ThongTinGiangVien newForm = new ThongTinGiangVien();
                    newForm.ShowDialog();
                }

                else if (select_HocVien.Checked)
                {
                    ThongTinHocVien newForm = new ThongTinHocVien();
                    newForm.ShowDialog();
                }

                this.Close();
            }
        }
    }
}
