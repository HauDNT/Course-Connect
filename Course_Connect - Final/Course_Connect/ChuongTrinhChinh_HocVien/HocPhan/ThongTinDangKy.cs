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

namespace Course_Connect.ChuongTrinhChinh_HocVien.HocPhan
{
    public partial class ThongTinDangKy : Form
    {
        DataTable danhSachDangKy = new DataTable();
        string sourceString = "SELECT TenHP AS [Tên học phần], MaGV AS [Mã giảng viên], NgayDK AS [Ngày đăng ký] " +
                              "FROM dbo.DangKyHocPhan, dbo.HocPhan " +
                              "WHERE dbo.DangKyHocPhan.MaHP = dbo.HocPhan.MaHP " +
                              "AND dbo.DangKyHocPhan.MaHV = '" + Decentralization.thisUserAccountCode + "' ";

        public ThongTinDangKy()
        {
            InitializeComponent();
            Loading(sourceString);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachDangKy.Clear();
            DatabaseInteract.adapter.Fill(danhSachDangKy);

            DGV_ChiTietDangKy.DataSource = danhSachDangKy;
        }

        private void btn_Perform_Click(object sender, EventArgs e)
        {
            if (txt_TenHP.Text == "")
                MessageBox.Show("Vui lòng nhập tên học phần cần tìm", "Thông báo");
            else
            {
                string findString = sourceString + "AND TenHP = N'" + txt_TenHP.Text + "'";
                Loading(findString);
            }
        }

        private void DGV_ChiTietDangKy_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_ChiTietDangKy.Rows.Count - 1 && !(DGV_ChiTietDangKy.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_ChiTietDangKy, Decentralization.thisUserAccountCode + " - Chi tiết đăng ký học phần", "Chi tiết đăng ký học phần");
        }
    }
}
