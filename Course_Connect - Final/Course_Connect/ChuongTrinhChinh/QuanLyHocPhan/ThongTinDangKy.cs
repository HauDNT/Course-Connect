using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect.ChuongTrinhChinh.QuanLyHocPhan
{
    public partial class ThongTinDangKy : Form
    {
        DataTable danhSachDangKy = new DataTable();
        string sourceString = "SELECT MaHV AS [Mã học viên], MaHP AS [Mã học phần], MaGV AS [Mã giảng viên], " +
                              "NgayDK AS [Ngày đăng ký] " +
                              "FROM DangKyHocPhan";
        string MaHV, MaHP, MaGV, NgayDK;

        public ThongTinDangKy()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_ChiTietDangKy.Columns.Add(buttonColumn);
        }


        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachDangKy.Clear();
            DatabaseInteract.adapter.Fill(danhSachDangKy);

            DGV_ChiTietDangKy.DataSource = danhSachDangKy;
        }

        private void DGV_ChiTietDangKy_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_ChiTietDangKy.Rows.Count - 1 && !(DGV_ChiTietDangKy.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_ChiTietDangKy, "Chi tiết đăng ký học phần", "Chi tiết đăng ký học phần");
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Thông tin đăng ký học phần: \n" +
                                   "Mã học viên: " + MaHV + " - Đăng ký học phần: " + MaHP +
                                   "\nBạn chắc chắn muốn xóa thông tin đăng ký này?",
                                   "Xác nhận",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                DatabaseInteract.command = new SqlCommand("DELETE FROM DangKyHocPhan WHERE MaHV = '" + MaHV +
                                                      "' AND MaHP = '" + MaHP + "'",
                                                      DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();
                Loading(sourceString);
            }
        }

        private void DGV_ChiTietDangKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ChiTietDangKy.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_ChiTietDangKy.RowCount - 1)
            {
                DataGridViewRow rowSelected = new DataGridViewRow();
                rowSelected = DGV_ChiTietDangKy.Rows[e.RowIndex];

                MaHV = rowSelected.Cells["Mã học viên"].Value.ToString();
                MaHP = rowSelected.Cells["Mã học phần"].Value.ToString();
                MaGV = rowSelected.Cells["Mã giảng viên"].Value.ToString();
                NgayDK = rowSelected.Cells["Ngày đăng ký"].Value.ToString();
            }
        }

        private void btn_Perform_Click(object sender, EventArgs e)
        {
            if (txt_MaHV.Text == "")
                MessageBox.Show("Vui lòng nhập mã học viên", "Thông báo");
            else
            {
                string findString = "SELECT MaHV AS [Mã học viên], " +
                                                    "MaHP AS [Mã học phần], " +
                                                    "MaGV AS [Mã giảng viên], " +
                                                    "NgayDK AS [Ngày đăng ký] " +
                                                    "FROM DangKyHocPhan " +
                                                    "WHERE MaHV = '" + txt_MaHV.Text + "'";

                Loading(findString);
            }
        }
    }
}
