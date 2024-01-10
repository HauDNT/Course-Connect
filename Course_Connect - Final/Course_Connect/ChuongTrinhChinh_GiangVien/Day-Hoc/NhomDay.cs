using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect.ChuongTrinhChinh_GiangVien.Day_Hoc
{
    public partial class NhomDay : Form
    {
        DataTable danhSachNhom = new DataTable();
        string sourceString = "SELECT MaNhom AS [Mã nhóm], TenHP AS [Tên học phần] " +
                              "FROM NhomHoc, ThongTinGiangVien, HocPhan " +
                              "WHERE dbo.NhomHoc.GiangVienDay = dbo.ThongTinGiangVien.MaGV " +
                              "AND dbo.NhomHoc.MAHP = HocPhan.MaHP " +
                              "AND dbo.ThongTinGiangVien.MaGV = '" + Decentralization.LayMaNguoiDung(Decentralization.thisUsername, Decentralization.accountType) + "'";

        public NhomDay()
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
            ConvertToExcel.Convert(DGV_NhomHoc, Decentralization.thisUserAccountCode + " - Nhóm dạy", "Nhóm dạy");
        }
    }
}
