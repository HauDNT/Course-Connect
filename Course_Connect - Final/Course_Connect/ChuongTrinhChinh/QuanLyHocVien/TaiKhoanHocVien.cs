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
    public partial class TaiKhoanHocVien : Form
    {
        DataTable danhSachTKHocVien = new DataTable();
        int trangThai = 0;
        string sourceString = "SELECT maTK AS [Mã tài khoản], " +
                                     "username AS [Tên tài khoản], " +
                                     "password AS [Mật khẩu] " +
                              "FROM TaiKhoanHocVien";

        public TaiKhoanHocVien()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_TKHocVien.Columns.Add(buttonColumn);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachTKHocVien.Clear();
            DatabaseInteract.adapter.Fill(danhSachTKHocVien);
            DGV_TKHocVien.DataSource = danhSachTKHocVien;
        }

        void EnableEdit()
        {
            txt_TenTK.ReadOnly = false;
            txt_MatKhau.ReadOnly = false;
        }

        void UnableEdit()
        {
            txt_TenTK.ReadOnly = true;
            txt_MatKhau.ReadOnly = true;
        }

        void ClearContent()
        {
            txt_MaHV.Clear();
            txt_TenTK.Clear();
            txt_MatKhau.Clear();
        }

        void Them()
        {
            if (txt_TenTK.Text == "" || txt_MatKhau.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else
            {
                DatabaseInteract.query = "SELECT COUNT(*) FROM TaiKhoanHocVien WHERE username = '" + txt_TenTK.Text + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                int count1 = (int)DatabaseInteract.command.ExecuteScalar();

                DatabaseInteract.query = "SELECT COUNT(*) FROM TaiKhoanGiangVien WHERE username = '" + txt_TenTK.Text + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                int count2 = (int)DatabaseInteract.command.ExecuteScalar();

                if (count1 > 0 || count2 > 0)
                    MessageBox.Show("Tên tài khoản này đã bị trùng, vui lòng thay đổi lại!");
                else
                {
                    string insertString = "INSERT INTO TaiKhoanHocVien " +
                                          "VALUES ('" + txt_MaHV.Text + "', '"
                                                      + txt_TenTK.Text + "', '"
                                                      + txt_MatKhau.Text + "')";
                    DatabaseInteract.command = new SqlCommand(insertString, DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    Loading(sourceString);
                    ClearContent();
                    UnableEdit();
                    trangThai = 0;
                }
            }
        }

        void Sua()
        {
            if (txt_TenTK.Text == "" || txt_MatKhau.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else
            {
                DatabaseInteract.update = "UPDATE TaiKhoanHocVien SET username = '" + txt_TenTK.Text + "', password = '" + txt_MatKhau.Text +
                                       "' WHERE maTK = '" + txt_MaHV.Text + "'";
                SqlCommand command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
                command.ExecuteNonQuery();

                Loading(sourceString);
                ClearContent();
                UnableEdit();
                trangThai = 0;
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
            EnableEdit();

            txt_MaHV.Text = Program.setNewID("maTK", "TaiKhoanHocVien", "HV");

            trangThai = 1;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            EnableEdit();
            trangThai = 2;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (txt_MaHV.Text == "")
                MessageBox.Show("Bạn phải chọn một học viên trong danh sách để xóa", "Thông báo");
            else
            {
                DialogResult confirm = MessageBox.Show("Việc xóa tài khoản " + txt_TenTK.Text +
                                                       " sẽ xóa tất cả thông tin của học viên này trong chương trình.\n" +
                                                       "Bạn chắc chắn thực hiện thao tác này?",
                                                       "Chú ý",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    // Xóa thông tin trong bảng DangKyHocPhan:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM DangKyHocPhan WHERE MaHV = '" + txt_MaHV.Text + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    // Xóa thông tin trong bảng ThongTinHocVien:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM ThongTinHocVien WHERE MaHV = '" + txt_MaHV.Text + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    // Xóa thông tin trong bảng TaiKhoanHocVien:
                    DatabaseInteract.command = new SqlCommand("DELETE FROM TaiKhoanHocVien WHERE maTK = '" + txt_MaHV.Text + "'", DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    Loading(sourceString);
                    ClearContent();
                }
            }
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = "SELECT maTK AS[Mã tài khoản], username AS[Tên tài khoản], password AS[Mật khẩu] FROM TaiKhoanHocVien WHERE maTK = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = "SELECT maTK AS[Mã tài khoản], username AS[Tên tài khoản], password AS[Mật khẩu] FROM TaiKhoanHocVien WHERE username = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
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

        private void DGV_TKHocVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_TKHocVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_TKHocVien.RowCount - 1)
            {
                ClearContent();
                DataGridViewRow rowSelected = new DataGridViewRow();

                rowSelected = DGV_TKHocVien.Rows[e.RowIndex];
                txt_MaHV.Text = Convert.ToString(rowSelected.Cells["Mã tài khoản"].Value);
                txt_TenTK.Text = Convert.ToString(rowSelected.Cells["Tên tài khoản"].Value);
                txt_MatKhau.Text = Convert.ToString(rowSelected.Cells["Mật khẩu"].Value);
            }
        }

        private void DGV_TKHocVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") && 
                (e.RowIndex < DGV_TKHocVien.Rows.Count - 1 && 
                 !(DGV_TKHocVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_TKHocVien, "Danh sách tài khoản học viên", "Danh sách tài khoản học viên");
        }
    }
}
