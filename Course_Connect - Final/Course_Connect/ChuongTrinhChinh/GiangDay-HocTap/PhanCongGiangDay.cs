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
    public partial class PhanCongGiangDay : Form
    {
        DataTable danhSachDay = new DataTable();
        string sourceString = "SELECT MaGV AS [Mã giảng viên], MaHP AS [Mã học phần] FROM GiangVien_HocPhan";

        public PhanCongGiangDay()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_PhanCongGiangDay.Columns.Add(buttonColumn);
        }

        void Loading (string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            danhSachDay.Clear();
            DatabaseInteract.adapter.Fill(danhSachDay);
            DGV_PhanCongGiangDay.DataSource = danhSachDay;
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            int index = selectionFind.SelectedIndex;
            string findString;

            switch (index)
            {
                case 0:
                    findString = sourceString + " WHERE GiangVien_HocPhan.MaHP = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 1:
                    findString = sourceString + " WHERE GiangVien_HocPhan.MaGV = '" + txtHangMucTimKiem.Text + "'";
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

        void ClearContent()
        {
            txt_MaGV.Clear();
            txt_MaHP.Clear();
        }

        bool KiemTraMaHPNhapVao(string maHP)
        {
            DatabaseInteract.query = "SELECT COUNT(*) FROM HocPhan WHERE MaHP = '" + maHP + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int count = (int)DatabaseInteract.command.ExecuteScalar();

            if (count == 0)
            {
                MessageBox.Show("Mã học phần mà bạn nhập không tồn tại!", "Thông báo");
                return false;
            }
            else
                return true;
        }

        bool KiemTraMaGVNhapVao(string maGV)
        {
            DatabaseInteract.query = "SELECT COUNT(*) FROM ThongTinGiangVien WHERE MaGV = '" + maGV + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int count = (int)DatabaseInteract.command.ExecuteScalar();

            if (count == 0)
            {
                MessageBox.Show("Mã giảng viên mà bạn nhập không tồn tại!", "Thông báo");
                return false;
            }
            else
                return true;
        }

        bool KiemTraTrungLichDay(string maGV, string maHP)
        {
            DatabaseInteract.query = "SELECT COUNT(*) FROM GiangVien_HocPhan WHERE MaGV = '" + maGV + "' AND MaHP = '" + maHP + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int count = (int)DatabaseInteract.command.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Lịch phân công này đã có rồi!", "Thông báo");
                return false;
            }
            else
                return true;
        }

        private void DGV_PhanCongGiangDay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_PhanCongGiangDay.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.RowIndex < DGV_PhanCongGiangDay.RowCount - 1)
            {
                ClearContent();
                DataGridViewRow rowSelected = new DataGridViewRow();

                rowSelected = DGV_PhanCongGiangDay.Rows[e.RowIndex];
                txt_MaGV.Text = Convert.ToString(rowSelected.Cells["Mã giảng viên"].Value);
                txt_MaHP.Text = Convert.ToString(rowSelected.Cells["Mã học phần"].Value);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            txt_MaGV.ReadOnly = false;
            txt_MaHP.ReadOnly = false;
        }

        private void btn_Perform_Click(object sender, EventArgs e)
        {
            if (KiemTraMaGVNhapVao(txt_MaGV.Text) == true &&
                KiemTraMaHPNhapVao(txt_MaHP.Text) == true &&
                KiemTraTrungLichDay(txt_MaGV.Text, txt_MaHP.Text) == true)
            {
                DatabaseInteract.insert = "INSERT INTO GiangVien_HocPhan VALUES ('" + txt_MaGV.Text + "', '" + txt_MaHP.Text + "')";

                DatabaseInteract.command = new SqlCommand(DatabaseInteract.insert, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();

                ClearContent();
                txt_MaGV.ReadOnly = true;
                txt_MaHP.ReadOnly = true;
                Loading(sourceString);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_MaHP.Text == "" || txt_MaGV.Text == "")
                MessageBox.Show("Hãy chọn cặp mã giảng viên - mã học phần muốn xóa.", "Thông báo");
            else
            {
                DialogResult confirm = MessageBox.Show("Thao tác này sẽ xóa lịch phân công giảng dạy của giảng viên đối với môn học này.\n" +
                                       "Bạn chắc chắn thực hiện thao tác này?",
                                       "Chú ý",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    // Thay thế thông tin trong bảng NhomHoc:
                    DatabaseInteract.update = "UPDATE NhomHoc SET GiangVienDay = NULL WHERE GiangVienDay = '" + txt_MaGV.Text + "'";
                    DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    // Xóa thông tin trong bảng GiangVien_HocPhan:
                    DatabaseInteract.delete = "DELETE FROM GiangVien_HocPhan WHERE MaGV = '" + txt_MaGV.Text + "' AND MaHP = '" + txt_MaHP.Text + "'";
                    DatabaseInteract.command = new SqlCommand(DatabaseInteract.delete, DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();
                }

                Loading(sourceString);
                ClearContent();
            }
        }

        private void DGV_PhanCongGiangDay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_PhanCongGiangDay.Rows.Count - 1 && !(DGV_PhanCongGiangDay.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_PhanCongGiangDay, "Phân công giảng dạy", "Phân công giảng dạy");
        }
    }
}
