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

namespace Course_Connect.ChuongTrinhChinh.QuanLyHocPhan
{
    public partial class DanhSachHocPhan : Form
    {
        DataTable danhSachHP = new DataTable();
        string sourceString = "SELECT MaHP AS [Mã học phần], TenHP AS [Tên học phần], SoTC AS [Số tín chỉ] FROM HocPhan";
        int trangThai = 0;

        public DanhSachHocPhan()
        {
            InitializeComponent();
            Loading(sourceString);

            // Tạo một cột button "Chọn" ở cuối DataGridView:
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_HocPhan.Columns.Add(buttonColumn);
        }

        void Loading(string selectString)
        {
            // Đưa dữ liệu của bảng vào DataGridView:
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachHP.Clear();
            DatabaseInteract.adapter.Fill(danhSachHP);

            DGV_HocPhan.DataSource = danhSachHP;
        }

        void ClearContent()
        {
            txtMaHP.Clear();
            txtTenHP.Clear();
            txtSoTC.Clear();
        }

        void Reset()
        {
            txtTenHP.ReadOnly = true;
            txtSoTC.ReadOnly = true;
            txtTenHP.PlaceholderText = string.Empty;
            txtSoTC.PlaceholderText = string.Empty;
        }

        void Them()
        {
            if (txtTenHP.Text == "" || txtSoTC.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else if (FormValidate.IsNumeric(txtSoTC.Text.Trim()) == false)
                MessageBox.Show("Số tín chỉ phải là số!", "Thông báo");
            else
            {
                string kiemTraTrungTenHP = "SELECT COUNT(*) FROM HocPhan WHERE TenHP = N'" + txtTenHP.Text + "'";
                DatabaseInteract.command = new SqlCommand(kiemTraTrungTenHP, DatabaseInteract.connect);
                
                int count = (int)DatabaseInteract.command.ExecuteScalar();

                if (count > 0)
                    MessageBox.Show("Học phần này đã có trong hệ thống");
                else
                {
                    string MaHP = txtMaHP.Text.Trim().ToUpper();
                    string TenHP = FormValidate.FormatString(txtTenHP.Text);
                    string SoTC = txtSoTC.Text.Trim();

                    string insertString = "INSERT INTO HocPhan VALUES ('" + MaHP + "', N'" + TenHP + "', " + SoTC + ")";
                    DatabaseInteract.command = new SqlCommand(insertString, DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    Loading(sourceString);
                    ClearContent();
                    Reset();
                }
                
                trangThai = 0;
            }
        }

        void Sua()
        {
            if (txtTenHP.Text == "" || txtSoTC.Text == "")
                MessageBox.Show("Bạn phải điền đầy đủ thông tin", "Thông báo");
            else if (FormValidate.IsNumeric(txtSoTC.Text.Trim()) == false)
                MessageBox.Show("Số tín chỉ phải là số!", "Thông báo");
            else if (txtMaHP.Text == "")
                MessageBox.Show("Bạn phải chọn một học phần trong danh sách để chỉnh sửa", "Thông báo");
            else
            {
                string TenHP = FormValidate.FormatString(txtTenHP.Text);
                string SoTC = txtSoTC.Text.Trim();

                DatabaseInteract.update = "UPDATE HocPhan SET TenHP = N'" + TenHP + "', SoTC = " + SoTC + " WHERE MaHP = '" + txtMaHP.Text + "'";

                DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();

                Loading(sourceString);
                ClearContent();
                Reset();

                trangThai = 0;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = "SELECT MaHP AS [Mã học phần], TenHP AS [Tên học phần], SoTC AS [Số tín chỉ] FROM HocPhan WHERE MaHP = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = "SELECT MaHP AS [Mã học phần], TenHP AS [Tên học phần], SoTC AS [Số tín chỉ] FROM HocPhan WHERE TenHP = N'" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    findString = "SELECT MaHP AS [Mã học phần], TenHP AS [Tên học phần], SoTC AS [Số tín chỉ] FROM HocPhan WHERE SoTC = '" + txtHangMucTimKiem.Text + "'";
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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txtMaHP.Text == "")
                MessageBox.Show("Bạn phải chọn một học phần trong danh sách để xóa", "Thông báo");
            else
            {
                DialogResult confirm = MessageBox.Show("Thông tin học phần: " +
                                                      "\n- Mã học phần: " + txtMaHP.Text +
                                                      "\n- Tên học phần: " + txtTenHP.Text +
                                                      "\n- Số tín chỉ: " + txtSoTC.Text +
                                                      "\nBạn chắc chắn muốn xóa học phần này khỏi chương trình?",
                                                      "Xác nhận",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    // Xóa học phần trong bang LichHoc
                    SqlCommand command = new SqlCommand("DELETE FROM LichHoc WHERE MaNhom LIKE '" + txtMaHP.Text + "%'", DatabaseInteract.connect);
                    command.ExecuteNonQuery();

                    // Xóa học phần trong bang NhomHoc
                    command = new SqlCommand("DELETE FROM NhomHoc WHERE MaHP = '" + txtMaHP.Text + "'", DatabaseInteract.connect);
                    command.ExecuteNonQuery();

                    // Xóa học phần trong bang GiangVien_HocPhan
                    command = new SqlCommand("DELETE FROM GiangVien_HocPhan WHERE MaHP = '" + txtMaHP.Text + "'", DatabaseInteract.connect);
                    command.ExecuteNonQuery();

                    // Xóa học phần trong bang DangKyHocPhan
                    command = new SqlCommand("DELETE FROM DangKyHocPhan WHERE MaHP = '" + txtMaHP.Text + "'", DatabaseInteract.connect);
                    command.ExecuteNonQuery();

                    // Xóa học phần trong bảng HocPhan
                    command = new SqlCommand("DELETE FROM HocPhan WHERE MaHP = '" + txtMaHP.Text + "'", DatabaseInteract.connect);
                    command.ExecuteNonQuery();
                    
                    Loading(sourceString);
                    ClearContent();
                    Reset();
                }
            }
        }

        private void DGV_HocPhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_HocPhan.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_HocPhan.RowCount - 1)
            {
                DataGridViewRow rowSelected = new DataGridViewRow();
                rowSelected = DGV_HocPhan.Rows[e.RowIndex];
                txtMaHP.Text = Convert.ToString(rowSelected.Cells["Mã học phần"].Value);
                txtTenHP.Text = Convert.ToString(rowSelected.Cells["Tên học phần"].Value);
                txtSoTC.Text = Convert.ToString(rowSelected.Cells["Số tín chỉ"].Value);
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            txtTenHP.ReadOnly = false;
            txtSoTC.ReadOnly = false;

            trangThai = 2;
        }

        private void btn_Perform_Click(object sender, EventArgs e)
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            ClearContent();

            txtTenHP.ReadOnly = false;
            txtSoTC.ReadOnly = false;

            txtMaHP.Text = Program.setNewID("MaHP", "HocPhan", "HP");

            trangThai = 1;
        }

        private void selectionFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHangMucTimKiem.Clear();
        }

        private void DGV_HocPhan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_HocPhan.Rows.Count - 1 && !(DGV_HocPhan.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_HocPhan, "Danh sách học phần", "Danh sách học phần");
        }
    }
}
