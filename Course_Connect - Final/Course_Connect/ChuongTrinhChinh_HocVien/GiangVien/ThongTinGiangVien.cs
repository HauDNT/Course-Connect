using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect.ChuongTrinhChinh_HocVien.GiangVien
{
    public partial class ThongTinGiangVien : Form
    {
        DataTable danhSachGiangVien = new DataTable();
        string sourceString = "SELECT HoTen AS [Họ và tên], " +
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
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachGiangVien.Clear();
            DatabaseInteract.adapter.Fill(danhSachGiangVien);
            DGV_GiangVien.DataSource = danhSachGiangVien;
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = sourceString + " WHERE HoTen = N'" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " WHERE HocVi = N'" + txtHangMucTimKiem.Text + "'";
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

        private void DGV_GiangVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_GiangVien.Rows.Count - 1 && !(DGV_GiangVien.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_GiangVien, Decentralization.thisUserAccountCode + " - Danh sách thông tin giảng viên", "Danh sách thông tin giảng viên");
        }
    }
}
