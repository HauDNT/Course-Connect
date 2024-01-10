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
    public partial class LichHoc : Form
    {
        int trangThai = 0;
        DataTable lichHoc = new DataTable();
        string sourceString = "SELECT NhomHoc.MaNhom AS[Nhóm], " +
                                     "TenHP AS [Tên học phần], " +
                                     "ThoiGianBatDau AS [Thời gian bắt đầu], " +
                                     "ThoiGianKetThuc AS [Thời gian kết thúc], " +
                                     "LichHoc.GhiChuThem AS [Ghi chú] " +
                              "FROM LichHoc, NhomHoc, HocPhan " +
                              "WHERE NhomHoc.MaNhom = LichHoc.MaNhom AND HocPhan.MaHP = NhomHoc.MaHP ";

        public LichHoc()
        {
            InitializeComponent();
            Loading(sourceString);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Chọn";
            buttonColumn.Name = "btnColumn";
            DGV_LichHoc.Columns.Add(buttonColumn);
        }

        void Loading(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            lichHoc.Clear();
            DatabaseInteract.adapter.Fill(lichHoc);
            DGV_LichHoc.DataSource = lichHoc;
        }

        void ClearContent()
        {
            txt_Nhom.Clear();
            txt_GhiChu.Clear();
            SelectTime_Start.ResetText();
            SelectTime_End.ResetText();
        }

        void EnableEdit()
        {
            txt_Nhom.ReadOnly = false;
            txt_GhiChu.ReadOnly = false;
            SelectTime_Start.Enabled = true;
            SelectTime_End.Enabled = true;
        }

        void UnableEdit()
        {
            txt_Nhom.ReadOnly = true;
            txt_GhiChu.ReadOnly = true;
            SelectTime_Start.Enabled = false;
            SelectTime_End.Enabled = false;
        }

        void Them()
        {
            DatabaseInteract.query = "SELECT COUNT(*) FROM NhomHoc WHERE MaNhom = '" + txt_Nhom.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            int check = (int)DatabaseInteract.command.ExecuteScalar();

            if (check == 1)
            {
                DatabaseInteract.query = "SELECT COUNT(*) FROM LichHoc WHERE MaNhom = '" + txt_Nhom.Text + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
                check = (int)DatabaseInteract.command.ExecuteScalar();

                if (check == 0)
                {
                    DatabaseInteract.insert = "INSERT INTO LichHoc VALUES ('" + txt_Nhom.Text + "', '" +
                                               String.Format("{0:yyyy/MM/dd hh:mm}", SelectTime_Start.Value) + "', '" +
                                               String.Format("{0:yyyy/MM/dd hh:mm}", SelectTime_End.Value) + "', N'" +
                                               txt_GhiChu.Text + "')";
                    DatabaseInteract.command = new SqlCommand(DatabaseInteract.insert, DatabaseInteract.connect);
                    DatabaseInteract.command.ExecuteNonQuery();

                    Loading(sourceString);
                    ClearContent();
                    UnableEdit();
                }
                else
                    MessageBox.Show("Nhóm học này đã được lên lịch học.", "Thông báo");
            }
            else
                MessageBox.Show("Mã nhóm học không tồn tại, hãy thử lại.", "Thông báo");
        }

        void Sua()
        {
            DatabaseInteract.update = "UPDATE LichHoc SET ThoiGianBatDau = '" + String.Format("{0:yyyy/MM/dd hh:mm}", SelectTime_Start.Value) +
                                      "', ThoiGianKetThuc = '" + String.Format("{0:yyyy/MM/dd hh:mm}", SelectTime_End.Value) +
                                      "', GhiChuThem = N'" + txt_GhiChu.Text +
                                      "' WHERE MaNhom = '" + txt_Nhom.Text + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.update, DatabaseInteract.connect);
            DatabaseInteract.command.ExecuteNonQuery();

            Loading(sourceString);
            ClearContent();
            UnableEdit();
        }

        private void DGV_LichHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow rowSelected = new DataGridViewRow();
            rowSelected = DGV_LichHoc.Rows[e.RowIndex];

            if (DGV_LichHoc.Columns[e.ColumnIndex] is DataGridViewButtonColumn && 
                e.RowIndex >= 0 && e.RowIndex < DGV_LichHoc.RowCount - 1)
            {
                ClearContent();

                txt_Nhom.Text = Convert.ToString(rowSelected.Cells["Nhóm"].Value);
                txt_GhiChu.Text = Convert.ToString(rowSelected.Cells["Ghi chú"].Value);

                if (Convert.ToString(rowSelected.Cells["Thời gian bắt đầu"].Value) != "" &&
                    Convert.ToString(rowSelected.Cells["Thời gian kết thúc"].Value) != "")
                {
                    SelectTime_Start.Value = DateTime.Parse(String.Format("{0:dddd, dd/MM/yyyy HH:mm}", Convert.ToString(rowSelected.Cells["Thời gian bắt đầu"].Value)));
                    SelectTime_End.Value = DateTime.Parse(String.Format("{0:dddd, dd/MM/yyyy HH:mm}", Convert.ToString(rowSelected.Cells["Thời gian kết thúc"].Value)));
                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            EnableEdit();
            ClearContent();
            trangThai = 1;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            txt_GhiChu.ReadOnly = false;
            SelectTime_Start.Enabled = true;
            SelectTime_End.Enabled = true;
            trangThai = 2;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DatabaseInteract.delete = "DELETE FROM LichHoc WHERE MaNhom = '" + txt_Nhom.Text + "'";
            DialogResult confirm = MessageBox.Show(
                                   "Bạn chắc chắn muốn xóa lịch dạy-học của nhóm " + txt_Nhom.Text + "?",
                                   "Xác nhận",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.delete, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();
                Loading(sourceString);
                ClearContent();
            }
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
                    findString = sourceString + " AND NhomHoc.GiangVienDay = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 2:
                    findString = sourceString + " AND NhomHoc.MaNhom = '" + txtHangMucTimKiem.Text + "'";
                    Loading(findString);
                    break;
                case 3:
                    Loading(sourceString);
                    break;
                default:
                    MessageBox.Show("Bạn chưa chọn hạng mục tìm kiếm nào", "Thông báo");
                    break;
            }
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
            }
        }

        private void DGV_LichHoc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.Value == null || e.Value == DBNull.Value || e.Value.ToString() == "") &&
                (e.RowIndex < DGV_LichHoc.Rows.Count - 1 && !(DGV_LichHoc.Columns[e.ColumnIndex] is DataGridViewButtonColumn)))
                    e.Value = "";
        }

        private void btn_ConvertExcel_Click(object sender, EventArgs e)
        {
            ConvertToExcel.Convert(DGV_LichHoc, "Danh sách lịch học", "Danh sách lịch học");
        }
    }
}
