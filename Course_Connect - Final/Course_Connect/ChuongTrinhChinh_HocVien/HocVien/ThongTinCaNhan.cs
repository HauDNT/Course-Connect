using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect.ChuongTrinhChinh_HocVien.HocVien
{
    public partial class ThongTinCaNhan : Form
    {
        DataTable ThongTin = new DataTable();
        string sourceString = "SELECT * " +
                              "FROM TaiKhoanHocVien, ThongTinHocVien " +
                              "WHERE TaiKhoanHocVien.maTK = ThongTinHocVien.MaHV " +
                              "AND TaiKhoanHocVien.username = '" + Decentralization.thisUsername + "'";

        public ThongTinCaNhan()
        {
            InitializeComponent();
            LoadDataToForm();
        }

        void TakeData(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            ThongTin.Clear();
            DatabaseInteract.adapter.Fill(ThongTin);
        }

        void LoadDataToForm()
        {
            // Gọi hàm lấy dữ liệu từ database:
            TakeData(sourceString);

            // Đổ dữ liệu vào các textboxes:
            txt_MaHV.Text = ThongTin.Rows[0][0].ToString();
            txt_TaiKhoan.Text = ThongTin.Rows[0][1].ToString();
            txt_MatKhau.Text = ThongTin.Rows[0][2].ToString();
            txt_HoTen.Text = ThongTin.Rows[0][4].ToString();
            txt_GioiTinh.Text = ThongTin.Rows[0][5].ToString();
            Select_NgaySinh.Value = DateTime.Parse(ThongTin.Rows[0][6].ToString());
            txt_Lop.Text = ThongTin.Rows[0][7].ToString();
            txt_Email.Text = ThongTin.Rows[0][8].ToString();
            txt_SoDT.Text = ThongTin.Rows[0][9].ToString();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            txt_MatKhau.ReadOnly = false;
            btn_Luu_TK.Enabled = true;
        }

        void UnableEditTT()
        {
            txt_HoTen.ReadOnly = true;
            txt_GioiTinh.ReadOnly = true;
            txt_Lop.ReadOnly = true;
            txt_Email.ReadOnly = true;
            txt_SoDT.ReadOnly = true;
            Select_NgaySinh.Enabled = false;
        }

        private void btn_Edit_TT_Click(object sender, EventArgs e)
        {
            btn_Luu_TT.Enabled = true;
            txt_HoTen.ReadOnly = false;
            txt_GioiTinh.ReadOnly = false;
            txt_Lop.ReadOnly = false;
            txt_Email.ReadOnly = false;
            txt_SoDT.ReadOnly = false;
            Select_NgaySinh.Enabled = true;
        }

        private void btn_Luu_TK_Click(object sender, EventArgs e)
        {
            if(txt_MatKhau.Text == "")
                MessageBox.Show("Bạn không được bỏ trống thông tin", "Thông báo");
            else
            {
                DatabaseInteract.update = "UPDATE TaiKhoanHocVien " +
                                          "SET password = '" + txt_MatKhau.Text.Trim() + "' " +
                                          "WHERE username = '" + txt_TaiKhoan.Text + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();

                MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông báo");
                txt_MatKhau.ReadOnly = true;
            }
            btn_Luu_TK.Enabled = false;
        }

        private void btn_Luu_TT_Click(object sender, EventArgs e)
        {
            if (txt_HoTen.Text == "" || txt_GioiTinh.Text == "" || txt_Lop.Text == "" || txt_Email.Text == "" || txt_SoDT.Text == "")
                MessageBox.Show("Bạn không được bỏ trống thông tin", "Thông báo");
            else
            {
                DatabaseInteract.update = "UPDATE ThongTinHocVien " +
                                          "SET HoTen = N'" + FormValidate.FormatString(txt_HoTen.Text) +
                                          "', GioiTinh = N'" + FormValidate.FormatString(txt_GioiTinh.Text) +
                                          "', NgaySinh = '" + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh.Value) +
                                          "', Lop = '" + txt_Lop.Text.Trim().ToUpper() +
                                          "', Email = '" + txt_Email.Text.Trim() +
                                          "', SoDT = '" + txt_SoDT.Text.Trim() + 
                                          "' WHERE MaHV = '" + Decentralization.LayMaNguoiDung(Decentralization.thisUsername, Decentralization.accountType) + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();

                MessageBox.Show("Thay đổi thông tin thành công!", "Thông báo");
                LoadDataToForm();
                UnableEditTT();
            }
            btn_Luu_TT.Enabled = false;
        }
    }
}
