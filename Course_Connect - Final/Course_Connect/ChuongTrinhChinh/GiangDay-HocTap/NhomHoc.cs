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

namespace Course_Connect.ChuongTrinhChinh.GiangDay_HocTap
{
    public partial class NhomHoc : Form
    {
        DataTable danhSachNhom = new DataTable();
        int trangThai = 0;
        string sourceString = "SELECT HocPhan.MaHP AS [Mã học phần], " +
                                     "HocPhan.TenHP AS[Tên học phần], " +
                                     "NhomHoc.MaNhom AS[Nhóm], " +
                                     "ThongTinGiangVien.MaGV AS [Mã giảng viên], " +
                                     "ThongTinGiangVien.HoTen AS[Giảng viên giảng dạy] " +
                              "FROM NhomHoc, HocPhan, ThongTinGiangVien " +
                              "WHERE NhomHoc.MaHP = HocPhan.MaHP AND " +
                              "NhomHoc.GiangVienDay = ThongTinGiangVien.MaGV ";

        public NhomHoc()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_NhomHoc.Columns.Add(buttonColumn);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachNhom.Clear();
            DatabaseInteract.adapter.Fill(danhSachNhom);
            DGV_NhomHoc.DataSource = danhSachNhom;
        }

        string TaoMaNhom(string maHP)
        {
            string maNhomMoi = "";
            DatabaseInteract.query = "SELECT COUNT(*) FROM NhomHoc WHERE MaNhom LIKE '" + maHP + "%'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);

            int count = (int)DatabaseInteract.command.ExecuteScalar();
            maNhomMoi = maHP + "-" + (count + 1);
            return maNhomMoi;
        }

        void LayDanhSachGiangVienTuongUng(string maHP)
        {
            List<string> danhSachGV = new List<string>();

            DatabaseInteract.query = "SELECT ThongTinGiangVien.HoTen AS [Họ tên các giảng viên giảng dạy] " +
                                     "FROM ThongTinGiangVien, GiangVien_HocPhan " +
                                     "WHERE GiangVien_HocPhan.MaGV = ThongTinGiangVien.MaGV AND GiangVien_HocPhan.MaHP = '" + maHP + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);

            DatabaseInteract.reader = DatabaseInteract.command.ExecuteReader();
            while (DatabaseInteract.reader.Read())
            {
                string hoTen = DatabaseInteract.reader["Họ tên các giảng viên giảng dạy"].ToString();
                danhSachGV.Add(hoTen);
            }

            DatabaseInteract.reader.Close();

            select_GiangVien.DataSource = danhSachGV;
        }

        public string LayMaGVTuHoTen(string hoTen, string maHP)
        {
            string maGV;

            DatabaseInteract.query = "SELECT ThongTinGiangVien.MaGV " +
                                     "FROM ThongTinGiangVien, GiangVien_HocPhan " +
                                     "WHERE ThongTinGiangVien.HoTen = N'" + hoTen + "' " +
                                     "AND GiangVien_HocPhan.MaHP = '" + maHP + "'";

            SqlCommand command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            maGV = command.ExecuteScalar().ToString();

            return maGV;
        }

        void ThemNhomHoc()
        {
            DatabaseInteract.insert = "INSERT INTO NhomHoc VALUES ('" + txt_Nhom.Text + "', '" + txt_MaHP.Text + "', '" + LayMaGVTuHoTen(select_GiangVien.SelectedValue.ToString(), txt_MaHP.Text) + "')";

            SqlCommand command = new SqlCommand(DatabaseInteract.insert, DatabaseInteract.connect);
            command.ExecuteNonQuery();

            txt_MaHP.Text = "";
            txt_Nhom.Text = "";
            select_GiangVien.DataSource = null;
            select_GiangVien.Items.Clear();
            Loading(sourceString);
            trangThai = 0;
        }

        void SuaThongTin()
        {
            DatabaseInteract.update = "UPDATE NhomHoc SET GiangVienDay = '" + LayMaGVTuHoTen(select_GiangVien.SelectedValue.ToString(), txt_MaHP.Text) + "' WHERE MaNhom = '" + txt_Nhom.Text + "'";

            DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
            DatabaseInteract.command.ExecuteNonQuery();

            txt_MaHP.Text = "";
            select_GiangVien.DataSource = null;
            select_GiangVien.Items.Clear();
            txt_Nhom.Text = "";
            trangThai = 0;
            Loading(sourceString);
        }

        void XoaThongTin()
        {
            DatabaseInteract.delete = "DELETE FROM LichHoc WHERE MaNhom = '" + txt_Nhom.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.delete, DatabaseInteract.connect);
            DatabaseInteract.command.ExecuteNonQuery();

            DatabaseInteract.delete = "DELETE FROM NhomHoc WHERE MaNhom = '" + txt_Nhom.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.delete, DatabaseInteract.connect);
            DatabaseInteract.command.ExecuteNonQuery();
            
            txt_MaHP.Text = "";
            select_GiangVien.DataSource = null;
            select_GiangVien.Items.Clear();
            txt_Nhom.Text = "";
            Loading(sourceString);
            trangThai = 0;
        }

        private void DGV_NhomHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_NhomHoc.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_NhomHoc.RowCount - 1)
            {
                DataGridViewRow rowSelected = new DataGridViewRow();
                rowSelected = DGV_NhomHoc.Rows[e.RowIndex];
                txt_MaHP.Text = Convert.ToString(rowSelected.Cells["Mã học phần"].Value);
                txt_Nhom.Text = Convert.ToString(rowSelected.Cells["Nhóm"].Value);

                LayDanhSachGiangVienTuongUng(txt_MaHP.Text);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            txt_MaHP.Focus();
            txt_MaHP.Clear();
            txt_MaHP.ReadOnly = false;
            txt_Nhom.Clear();
            select_GiangVien.DataSource = null;
            select_GiangVien.Items.Clear();
            select_GiangVien.Enabled = true;
            trangThai = 1;
        }

        private void btn_Perform_Click(object sender, EventArgs e)
        {
            switch (trangThai)
            {
                case 1:
                    ThemNhomHoc();
                    break;
                case 2:
                    SuaThongTin();
                    break;
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            select_GiangVien.Enabled = true;
            trangThai = 2;
        }

        private void txt_MaHP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && trangThai == 1)
            {
                DatabaseInteract.query = "SELECT COUNT(*) FROM HocPhan WHERE MaHP = '" + txt_MaHP.Text + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);

                if ((int)DatabaseInteract.command.ExecuteScalar() == 0)
                    MessageBox.Show("Chứng chỉ không tồn tại", "Thông báo");
                else
                {
                    txt_Nhom.Text = TaoMaNhom(txt_MaHP.Text);
                    LayDanhSachGiangVienTuongUng(txt_MaHP.Text);
                }
            }
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString = "";

            switch (index)
            {
                case 0:
                    findString = sourceString + " AND HocPhan.MaHP = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " AND ThongTinGiangVien.MaGV = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    findString = sourceString + " AND MaNhom = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 3:
                    Loading(sourceString);
                    break;
                default:
                    MessageBox.Show("Hãy chọn hạng mục tìm kiếm phù hợp", "Thông báo");
                    break;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            XoaThongTin();
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
            ConvertToExcel.Convert(DGV_NhomHoc, "Danh sách nhóm học", "Danh sách nhóm học");
        }
    }
}
