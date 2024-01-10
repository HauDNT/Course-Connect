using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect.ChuongTrinhChinh_GiangVien.Day_Hoc
{
    public partial class LichDay : Form
    {
        DataTable lichHoc = new DataTable();
        string sourceString = "SELECT NhomHoc.MaNhom AS[Nhóm], " +
                                     "TenHP AS [Tên học phần], " +
                                     "ThoiGianBatDau AS [Thời gian bắt đầu], " +
                                     "ThoiGianKetThuc AS [Thời gian kết thúc], " +
                                     "LichHoc.GhiChuThem AS [Ghi chú] " +
                              "FROM LichHoc, NhomHoc, HocPhan " +
                              "WHERE NhomHoc.MaNhom = LichHoc.MaNhom AND HocPhan.MaHP = NhomHoc.MaHP " +
                              "AND NhomHoc.GiangVienDay = '" + Decentralization.LayMaNguoiDung(Decentralization.thisUsername, Decentralization.accountType) + "'";

        public LichDay()
        {
            InitializeComponent();
            Loading(sourceString);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            lichHoc.Clear();
            DatabaseInteract.adapter.Fill(lichHoc);
            DGV_LichHoc.DataSource = lichHoc;
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = sourceString + " AND NhomHoc.MaHP = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " AND NhomHoc.MaNhom = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    Loading(sourceString);
                    break;
                default:
                    MessageBox.Show("Bạn chưa chọn hạng mục tìm kiếm nào", "Thông báo");
                    break;
            }
        }

        private void selectionFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHangMucTimKiem.Clear();
        }

        private void DGV_LichHoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_LichHoc.Rows.Count - 1 && !(DGV_LichHoc.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_LichHoc, Decentralization.thisUserAccountCode + " - Lịch dạy", "Lịch dạy");
        }
    }
}
