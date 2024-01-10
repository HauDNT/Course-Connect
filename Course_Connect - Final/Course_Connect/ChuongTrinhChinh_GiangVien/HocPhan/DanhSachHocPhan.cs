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

namespace Course_Connect.ChuongTrinhChinh_GiangVien.HocPhan
{
    public partial class DanhSachHocPhan : Form
    {
        DataTable danhSachHP = new DataTable();
        string sourceString = "SELECT MaHP AS [Mã học phần], TenHP AS [Tên học phần], SoTC AS [Số tín chỉ] FROM HocPhan";

        public DanhSachHocPhan()
        {
            InitializeComponent();
            Loading(sourceString);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachHP.Clear();
            DatabaseInteract.adapter.Fill(danhSachHP);

            DGV_HocPhan.DataSource = danhSachHP;
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
            ConvertToExcel.Convert(DGV_HocPhan, Decentralization.thisUserAccountCode + " - Danh sách học phần", "Danh sách học phần");
        }
    }
}
