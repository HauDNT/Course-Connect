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

namespace Course_Connect.ChuongTrinhChinh.QuanLyGiangVien
{
    public partial class ThongTinGiangVien : Form
    {
        int trangThai = 0;
        DataTable danhSachGiangVien = new DataTable();
        string sourceString = "SELECT MaGV AS [Mã giảng viên], " +
                                    "HoTen AS [Họ và tên], " +
                                    "GioiTinh AS [Giới tính], " +
                                    "NgaySinh AS [Ngày sinh], " +
                                    "HocVi AS [Học vị], " +
                                    "Email AS [Email], " +
                                    "SoDT AS [Số điện thoại] " +
                              "FROM ThongTinGiangVien";

        public ThongTinGiangVien()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_GiangVien.Columns.Add(buttonColumn);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachGiangVien.Clear();
            DatabaseInteract.adapter.Fill(danhSachGiangVien);
            DGV_GiangVien.DataSource = danhSachGiangVien;
        }

        void ClearContent()
        {
            select_MaGV.DataSource = null;
            select_MaGV.Items.Clear();
            txt_TenGV.Clear();
            txt_GioiTinh.Clear();
            Select_NgaySinh.ResetText();
            txt_HocVi.Clear();
            txt_Email.Clear();
            txt_SoDT.Clear();
        }

        void UnableEditInfo()
        {
            txt_TenGV.ReadOnly = true;
            txt_GioiTinh.ReadOnly = true;
            Select_NgaySinh.Enabled = false;
            txt_HocVi.ReadOnly = true;
            txt_Email.ReadOnly = true;
            txt_SoDT.ReadOnly = true;
        }

        void EnableEditInfo()
        {
            select_MaGV.Enabled = true;
            txt_TenGV.ReadOnly = false;
            txt_GioiTinh.ReadOnly = false;
            Select_NgaySinh.Enabled = true;
            txt_HocVi.ReadOnly = false;
            txt_Email.ReadOnly = false;
            txt_SoDT.ReadOnly = false;
        }

        void ImportToCombobox()
        {
            // Đưa danh sách mã GV vào Combobox:
            List<string> danhSachMaGV = new List<string>();

            DatabaseInteract.query = "SELECT maTK FROM TaiKhoanGiangVien";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            DatabaseInteract.reader = DatabaseInteract.command.ExecuteReader();

            while (DatabaseInteract.reader.Read())
            {
                string maGV = DatabaseInteract.reader["maTK"].ToString();
                danhSachMaGV.Add(maGV);
            }

            DatabaseInteract.reader.Close();
            select_MaGV.DataSource = danhSachMaGV;
        }

        void Them()
        {
            if (txt_TenGV.Text == "" || txt_GioiTinh.Text == "" || txt_HocVi.Text == "" || txt_Email.Text == "" || txt_SoDT.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else if (!Select_NgaySinh.Checked)
                MessageBox.Show("Hãy chọn ngày tháng năm sinh", "Thông báo");
            else
            {
                // Kiểm tra đã có tài khoản của giảng viên này trong bảng TaiKhoanGiangVien chưa:
                DatabaseInteract.query = "SELECT COUNT(*) \nFROM TaiKhoanGiangVien \nWHERE maTK = '" + select_MaGV.SelectedItem + "'";

                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                int count = (int)DatabaseInteract.command.ExecuteScalar();

                if (count > 0)
                {
                    // Kiểm tra người dùng có chọn vào mã giảng viên đã tồn tại trong bảng thông tin không:
                    DatabaseInteract.query = "SELECT COUNT(*) \nFROM ThongTinGiangVien \nWHERE MaGV = '" + select_MaGV.SelectedItem + "'";
                    DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                    count = (int)DatabaseInteract.command.ExecuteScalar();

                    if (count == 0)
                    {
                        // Thêm thông tin giảng viên mới vào CSDL:
                        DatabaseInteract.insert = "INSERT INTO ThongTinGiangVien VALUES ('" +
                                              select_MaGV.SelectedItem +
                                              "', N'" + txt_TenGV.Text +
                                              "', N'" + txt_GioiTinh.Text +
                                              "', '" + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh.Value) +
                                              "', N'" + txt_HocVi.Text +
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
                        MessageBox.Show("Thông tin giảng viên này đã có trong hệ thống!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Bạn không thể thêm thông tin vì giảng viên này chưa có tài khoản!\nHãy tạo tài khoản",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        void Sua()
        {
            if (txt_TenGV.Text == "" || txt_GioiTinh.Text == "" || txt_HocVi.Text == "" || txt_Email.Text == "" || txt_SoDT.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else if (select_MaGV.SelectedIndex == -1)
                MessageBox.Show("Bạn phải chọn một giảng viên trong danh sách để chỉnh sửa", "Thông báo");
            else
            {
                DatabaseInteract.update = "UPDATE ThongTinGiangVien SET HoTen = N'" + txt_TenGV.Text +
                                "', GioiTinh = N'" + txt_GioiTinh.Text +
                                "', NgaySinh = '" + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh.Value) +
                                "', HocVi = N'" + txt_HocVi.Text +
                                "', Email = '" + txt_Email.Text +
                                "', SoDT = '" + txt_SoDT.Text + "' WHERE MaGV = '" + select_MaGV.SelectedItem + "'";

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
            if (select_MaGV.SelectedIndex == -1)
                MessageBox.Show("Bạn phải chọn một giảng viên trong danh sách để xóa", "Thông báo");
            else
            {
                DialogResult confirm = MessageBox.Show("Thông tin giảng viên: " +
                                                   "\n- Mã học viên: " + select_MaGV.SelectedItem +
                                                   "\n- Họ và tên: " + txt_TenGV +
                                                   "\n- Giới tính: " + txt_GioiTinh.Text +
                                                   "\n- Ngày sinh: " + String.Format("{0:yyyy/MM/dd}", Select_NgaySinh) +
                                                   "\n- Học vị: " + txt_HocVi.Text +
                                                   "\n- Email: " + txt_Email.Text +
                                                   "\n- Số điện thoại: " + txt_SoDT.Text +
                                                   "\nBạn chắc chắn muốn xóa thông tin giảng viên này?",
                                                   "Xác nhận",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    //Xóa thông tin giảng viên trong bảng DangKyHocPhan:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM DangKyHocPhan WHERE MaGV = '" + select_MaGV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    //Xóa thông tin giảng viên trong bảng GiangVien_HocPhan:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM GiangVien_HocPhan WHERE MaGV = '" + select_MaGV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    //Xóa thông tin giảng viên trong bảng ThongTinGiangVien:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM ThongTinGiangVien WHERE MaGV = '" + select_MaGV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    //Xóa thông tin tài khoản của giảng viên này trong bảng TaiKhoanGiangVien:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM TaiKhoanGiangVien WHERE maTK = '" + select_MaGV.SelectedItem + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    Loading(sourceString);
                    ClearContent();
                }
            }
        }

        private void DGV_GiangVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_GiangVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_GiangVien.RowCount - 1)
            {
                ClearContent();
                DataGridViewRow rowSelected = new DataGridViewRow();

                rowSelected = DGV_GiangVien.Rows[e.RowIndex];
                select_MaGV.Items.Add(Convert.ToString(rowSelected.Cells["Mã giảng viên"].Value));
                select_MaGV.SelectedIndex = 0;

                txt_TenGV.Text = Convert.ToString(rowSelected.Cells["Họ và tên"].Value);
                txt_GioiTinh.Text = Convert.ToString(rowSelected.Cells["Giới tính"].Value);

                Select_NgaySinh.Value = DateTime.Parse(String.Format("{0:MM/dd/yyyy}",
                                                      Convert.ToString(rowSelected.Cells["Ngày sinh"].Value)));

                txt_HocVi.Text = Convert.ToString(rowSelected.Cells["Học vị"].Value);
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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            ClearContent();
            EnableEditInfo();
            ImportToCombobox();
            trangThai = 1;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            EnableEditInfo();
            trangThai = 2;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Xoa();
            select_MaGV.Items.Clear();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = sourceString + " WHERE MaGV = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " WHERE HoTen = N'" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    findString = sourceString + " WHERE HocVi = N'" + txtHangMucTimKiem.Text + "'";
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

        private void DGV_GiangVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_GiangVien.Rows.Count - 1 && !(DGV_GiangVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_GiangVien, "Danh sách thông tin giảng viên", "Danh sách thông tin giảng viên");
        }
    }
}
