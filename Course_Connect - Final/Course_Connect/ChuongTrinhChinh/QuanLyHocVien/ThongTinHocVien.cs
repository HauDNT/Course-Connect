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

namespace Course_Connect.ChuongTrinhChinh.QuanLyHocVien
{
    public partial class ThongTinHocVien : Form
    {
        DataTable danhSachHocVien = new DataTable();
        int trangThai = 0;
        string sourceString = "SELECT MaHV AS [Mã học viên], " +
                                    "HoTen AS [Họ và tên], " +
                                    "GioiTinh AS [Giới tính], " +
                                    "NgaySinh AS [Ngày sinh], " +
                                    "Lop AS [Lớp], " +
                                    "Email AS [Email], " +
                                    "SoDT AS [Số điện thoại] " +
                              "FROM ThongTinHocVien";

        public ThongTinHocVien()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_HocVien.Columns.Add(buttonColumn);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachHocVien.Clear();
            DatabaseInteract.adapter.Fill(danhSachHocVien);
            DGV_HocVien.DataSource = danhSachHocVien;
        }

        void ClearContent()
        {
            select_MaHV.DataSource = null;
            select_MaHV.Items.Clear();
            txt_TenHV.Clear();
            txt_GioiTinh.Clear();
            Select_NgaySinh.ResetText();
            txt_Lop.Clear();
            txt_Email.Clear();
            txt_SoDT.Clear();
        }

        void UnableEditInfo()
        {
            txt_TenHV.ReadOnly = true;
            txt_GioiTinh.ReadOnly = true;
            Select_NgaySinh.Enabled = false;
            txt_Lop.ReadOnly = true;
            txt_Email.ReadOnly = true;
            txt_SoDT.ReadOnly = true;
        }

        void EnableEditInfo()
        {
            select_MaHV.Enabled = true;
            txt_TenHV.ReadOnly = false;
            txt_GioiTinh.ReadOnly = false;
            Select_NgaySinh.Enabled = true;
            txt_Lop.ReadOnly = false;
            txt_Email.ReadOnly = false;
            txt_SoDT.ReadOnly = false;
        }

        void ImportToCombobox()
        {
            // Đưa danh sách mã HV vào Combobox:
            List<string> danhSachMaHV = new List<string>();

            DatabaseInteract.query = "SELECT maTK FROM TaiKhoanHocVien";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            DatabaseInteract.reader = DatabaseInteract.command.ExecuteReader();

            while (DatabaseInteract.reader.Read())
            {
                string maHV = DatabaseInteract.reader["maTK"].ToString();
                danhSachMaHV.Add(maHV);
            }

            DatabaseInteract.reader.Close();
            select_MaHV.DataSource = danhSachMaHV;
        }

        void Them()
        {
            if (txt_TenHV.Text == "" || txt_GioiTinh.Text == "" || txt_Lop.Text == "" || txt_Email.Text == "" || txt_SoDT.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else if (!Select_NgaySinh.Checked)
                MessageBox.Show("Hãy chọn ngày tháng năm sinh", "Thông báo");
            else
            {
                // Kiểm tra đã có tài khoản của học viên này trong bảng TaiKhoanHocVien chưa:
                DatabaseInteract.query = "SELECT COUNT(*) \nFROM TaiKhoanHocVien \nWHERE maTK = '" + select_MaHV.SelectedItem + "'";

                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                int count = (int)DatabaseInteract.command.ExecuteScalar();

                if (count > 0)
                {
                    // Kiểm tra người dùng có chọn vào mã học viên đã tồn tại trong bảng thông tin không:
                    DatabaseInteract.query = "SELECT COUNT(*) \nFROM ThongTinHocVien \nWHERE MaHV = '" + select_MaHV.SelectedItem + "'";
                    DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                    count = (int)DatabaseInteract.command.ExecuteScalar();

                    if (count == 0)
                    {
                        // Thêm thông tin học viên mới vào CSDL:
                        DatabaseInteract.insert = "INSERT INTO ThongTinHocVien VALUES ('" +
                                              select_MaHV.SelectedItem +
                                              "', N'" + FormValidate.FormatString(txt_TenHV.Text) +
                                              "', N'" + FormValidate.FormatString(txt_GioiTinh.Text) +
                                              "', '" + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh.Value) +
                                              "', '" + txt_Lop.Text.Trim().ToUpper() +
                                              "', '" + txt_Email.Text +
                                              "', '" + txt_SoDT.Text + "')";

                        DatabaseInteract.command = new SqlCommand(DatabaseInteract.insert, DatabaseInteract.connect);
                        DatabaseInteract.command.ExecuteNonQuery();

                        Loading(sourceString);
                        ClearContent();
                        UnableEditInfo();

                        trangThai = 0;
                    }
                    else
                        MessageBox.Show("Thông tin học viên này đã có trong hệ thống!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Bạn không thể thêm thông tin vì học viên này chưa có tài khoản!\nHãy tạo tài khoản",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        void Sua()
        {
            if (txt_TenHV.Text == "" || txt_GioiTinh.Text == "" || txt_Lop.Text == "" || txt_Email.Text == "" || txt_SoDT.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else if (select_MaHV.SelectedIndex == -1)
                MessageBox.Show("Bạn phải chọn một học viên trong danh sách để chỉnh sửa", "Thông báo");
            else
            {
                DatabaseInteract.update = "UPDATE ThongTinHocVien SET HoTen = N'" + txt_TenHV.Text +
                                "', GioiTinh = N'" + txt_GioiTinh.Text +
                                "', NgaySinh = '" + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh.Value) +
                                "', Lop = N'" + txt_Lop.Text +
                                "', Email = '" + txt_Email.Text +
                                "', SoDT = '" + txt_SoDT.Text + "' WHERE MaHV = '" + select_MaHV.SelectedItem + "'";

                DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();

                Loading(sourceString);
                ClearContent();
                UnableEditInfo();
                trangThai = 0;
            }
        }

        void Xoa()
        {
            if (select_MaHV.SelectedIndex == -1)
                MessageBox.Show("Bạn phải chọn một học viên trong danh sách để xóa", "Thông báo");
            else
            {
                DialogResult confirm = MessageBox.Show("Thông tin học viên: " +
                                                   "\n- Mã học viên: " + select_MaHV.SelectedItem +
                                                   "\n- Họ và tên: " + txt_TenHV +
                                                   "\n- Giới tính: " + txt_GioiTinh.Text +
                                                   "\n- Ngày sinh: " + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh) +
                                                   "\n- Lớp: " + txt_Lop.Text +
                                                   "\n- Email: " + txt_Email.Text +
                                                   "\n- Số điện thoại: " + txt_SoDT.Text +
                                                   "\nBạn chắc chắn muốn xóa thông tin học viên này?",
                                                   "Xác nhận",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    //Xóa thông tin học viên trong bảng DangKyHocPhan:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM DangKyHocPhan WHERE MaHV = '" + select_MaHV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    //Xóa thông tin học viên trong bảng ThongTinHocVien:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM ThongTinHocVien WHERE MaHV = '" + select_MaHV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    //Xóa thông tin tài khoản của học viên này trong bảng TaiKhoanHocVien:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM TaiKhoanHocVien WHERE maTK = '" + select_MaHV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();
                    
                    Loading(sourceString);
                    ClearContent();
                }
            }
        }

        private void DGV_HocVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_HocVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_HocVien.RowCount - 1)
            {
                ClearContent();
                DataGridViewRow rowSelected = new DataGridViewRow();

                rowSelected = DGV_HocVien.Rows[e.RowIndex];
                select_MaHV.Items.Add(Convert.ToString(rowSelected.Cells["Mã học viên"].Value));
                select_MaHV.SelectedIndex = 0;

                txt_TenHV.Text = Convert.ToString(rowSelected.Cells["Họ và tên"].Value);
                txt_GioiTinh.Text = Convert.ToString(rowSelected.Cells["Giới tính"].Value);

                Select_NgaySinh.Value = DateTime.Parse(String.Format("{0:MM/dd/yyyy}",
                                                      Convert.ToString(rowSelected.Cells["Ngày sinh"].Value)));

                txt_Lop.Text = Convert.ToString(rowSelected.Cells["Lớp"].Value);
                txt_Email.Text = Convert.ToString(rowSelected.Cells["Email"].Value);
                txt_SoDT.Text = Convert.ToString(rowSelected.Cells["Số điện thoại"].Value);
            }
        }

        private void btn_ThucHien_Click(object sender, EventArgs e)
        {
            switch (trangThai)
            {
                case 1:
                    Them();
                    break;
                case 2:
                    Sua();
                    break;
                default:
                    MessageBox.Show("Bạn chưa thực hiện thao tác nào", "Thông báo");
                    break;
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            EnableEditInfo();
            trangThai = 2;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            ClearContent();
            EnableEditInfo();
            ImportToCombobox();
            trangThai = 1;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Xoa();
            select_MaHV.Items.Clear();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = sourceString + " WHERE MaHV = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " WHERE HoTen = N'" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    findString = sourceString + " WHERE Lop = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 3:
                    Loading(sourceString);
                    break;
                default:
                    MessageBox.Show("Bạn chưa chọn hạng mục tìm kiếm", "Thông báo");
                    break;
            }
        }

        private void selectionFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHangMucTimKiem.Clear();
        }

        private void DGV_HocVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_HocVien.Rows.Count - 1 && !(DGV_HocVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_HocVien, "Danh sách thông tin học viên", "Danh sách thông tin học viên");
        }
    }
}
