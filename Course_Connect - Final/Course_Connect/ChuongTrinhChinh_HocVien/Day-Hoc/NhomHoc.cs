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

namespace Course_Connect.ChuongTrinhChinh_HocVien.Day_Hoc
{
    public partial class NhomHoc : Form
    {
        DataTable danhSachNhom = new DataTable();
        string sourceString = "SELECT NhomHoc.MaNhom AS[Nhóm], " +
                                     "HocPhan.MaHP AS [Mã học phần], " +
                                     "HocPhan.TenHP AS[Tên học phần], " +
                                     "ThongTinGiangVien.MaGV AS [Mã giảng viên], " +
                                     "ThongTinGiangVien.HoTen AS[Giảng viên giảng dạy] " +
                              "FROM NhomHoc, HocPhan, ThongTinGiangVien " +
                              "WHERE NhomHoc.MaHP = HocPhan.MaHP AND " +
                              "NhomHoc.GiangVienDay = ThongTinGiangVien.MaGV ";

        public NhomHoc()
        {
            InitializeComponent();
            Loading(sourceString);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachNhom.Clear();
            DatabaseInteract.adapter.Fill(danhSachNhom);
            DGV_NhomHoc.DataSource = danhSachNhom;
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString = "";

            switch (index)
            {
                case 0:
                    findString = sourceString + " AND MaNhom = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " AND HocPhan.TenHP = N'" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    findString = sourceString + " AND ThongTinGiangVien.HoTen = N'" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 3:
                    Loading(sourceString);
                    break;
                default:
                    MessageBox.Show("Hãy chọn hạng mục tìm kiếm", "Thông báo");
                    break;
            }
        }

        private void selectionFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHangMucTimKiem.Clear();
        }

        private void DGV_NhomHoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_NhomHoc.Rows.Count - 1 && !(DGV_NhomHoc.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_NhomHoc, Decentralization.thisUserAccountCode + " - Danh sách nhóm học", "Danh sách nhóm học");
        }
    }
}
