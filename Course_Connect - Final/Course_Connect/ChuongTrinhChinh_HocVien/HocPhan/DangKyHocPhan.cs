using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Connect.ChuongTrinhChinh_HocVien.HocPhan
{
    public partial class DangKyHocPhan : Form
    {
        DataTable ThongTin = new DataTable();
        string infoString = "SELECT MaHV, HoTen " +
                            "FROM TaiKhoanHocVien, ThongTinHocVien " +
                            "WHERE TaiKhoanHocVien.maTK = ThongTinHocVien.MaHV " +
                            "AND TaiKhoanHocVien.username = '" + Decentralization.thisUsername + "'";

        public DangKyHocPhan()
        {
            InitializeComponent();
            TakeData(infoString);
        }

        void TakeData(string selectString)
        {
            DatabaseInteract.command = new SqlCommand(selectString, DatabaseInteract.connect);
            DatabaseInteract.adapter = new SqlDataAdapter(DatabaseInteract.command);
            ThongTin.Clear();
            DatabaseInteract.adapter.Fill(ThongTin);

            MaHV_Confirm.Text = ThongTin.Rows[0][0].ToString();
            TenHV_Confirm.Text = ThongTin.Rows[0][1].ToString();
        }

        void LayThongTinHocPhan(string maHP)
        {
            // Lấy tên học phần:
            DatabaseInteract.query = "SELECT TenHP FROM HocPhan WHERE MaHP = '" + maHP + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            object getTenHP = DatabaseInteract.command.ExecuteScalar();

            // Lấy số tín chỉ của học phần:
            DatabaseInteract.query = "SELECT SoTC FROM HocPhan WHERE MaHP = '" + maHP + "'";
            DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);
            object getSoTC = DatabaseInteract.command.ExecuteScalar();

            if (getTenHP != null && getSoTC != null)
            {
                txt_TenHP.Text = getTenHP.ToString();
                txt_SoTC.Text = getSoTC.ToString();

                // Lấy danh sách giảng viên giảng dạy:
                List<string> danhSachGV = new List<string>();
                DatabaseInteract.query = "SELECT ThongTinGiangVien.HoTen " +
                                         "FROM GiangVien_HocPhan, ThongTinGiangVien " +
                                         "WHERE GiangVien_HocPhan.MaGV = ThongTinGiangVien.MaGV AND GiangVien_HocPhan.MaHP = '" + maHP + "'";
                DatabaseInteract.command = new SqlCommand(DatabaseInteract.query, DatabaseInteract.connect);

                DatabaseInteract.reader = DatabaseInteract.command.ExecuteReader();
                while (DatabaseInteract.reader.Read())
                {
                    string hoTenGV = DatabaseInteract.reader["HoTen"].ToString();
                    danhSachGV.Add(hoTenGV);
                }

                DatabaseInteract.reader.Close();
                select_GiangVien.DataSource = danhSachGV;
            }
            else
            {
                MessageBox.Show("Mã học phần bạn nhập không có trong hệ thống. Hãy kiểm tra lại!", "Thông báo");
            }
        }

        bool KiemTraTrongBangXacNhan(string maHP)
        {
            for (int i = 0; i < DGV_DangKy.Rows.Count; i++)
                if (DGV_DangKy.Rows[i].Cells["MaHP"].Value.ToString().Equals(maHP) == true)
                {
                    MessageBox.Show("Học phần này đã được bạn thêm vào rồi!", "Thông báo");
                    return false;
                }

            return true;
        }
        void DuaVaoBangXacNhan()
        {
            if (select_GiangVien.Items.Count > 0)
            {
                // Lấy dữ liệu nhập vào:
                string maHP = txt_MaHP.Text;
                string tenHP = txt_TenHP.Text;
                string giangVien = select_GiangVien.SelectedValue.ToString();
                string thoiGian = String.Format("{0:yyyy/MM/dd}", select_Date.Value);

                // Kiểm tra điều kiên => Đúng thì đưa vào DataGridView:
                if (KiemTraTrongBangXacNhan(maHP) == true)
                    DGV_DangKy.Rows.Insert(DGV_DangKy.Rows.Count, maHP, tenHP, giangVien, thoiGian);

                Reset_ThongTinHocPhan();
            }
            else
                MessageBox.Show("Không thể đăng ký học phần này, hãy xem lại thông tin!", "Thông báo");
        }

        void Reset_ThongTinHocPhan()
        {
            txt_MaHP.Text = "";
            txt_TenHP.Text = "";
            txt_SoTC.Text = "";
            select_GiangVien.DataSource = null;
        }

        void Reset_ThongTinHocVien()
        {
            MaHV_Confirm.Text = "";
            TenHV_Confirm.Text = "";
        }

        void Reset_Form()
        {
            Reset_ThongTinHocVien();
            Reset_ThongTinHocPhan();
            DGV_DangKy.Rows.Clear();
        }

        bool KiemTraDangKyTrungHocPhanTrongCSDL()
        {
            DatabaseInteract.command = new SqlCommand("SELECT COUNT(*) FROM DangKyHocPhan WHERE " +
                         "MaHV = '" + MaHV_Confirm.Text +
                         "' AND MaHP = '" + txt_MaHP.Text + "'", DatabaseInteract.connect);
            int count = (int)DatabaseInteract.command.ExecuteScalar();

            if (count == 0)
                return true;
            return false;
        }

        void DuaVaoCSDL()
        {
            for (int i = 0; i < DGV_DangKy.Rows.Count; i++)
            {
                DataGridViewRow thisRow = DGV_DangKy.Rows[i];

                string maHP = thisRow.Cells["MaHP"].Value.ToString();
                string tenGV = thisRow.Cells["GiangVien"].Value.ToString();
                string ngayDK = thisRow.Cells["ThoiGianDangKy"].Value.ToString();
                string maGV = "";

                DatabaseInteract.command = new SqlCommand("SELECT GiangVien_HocPhan.MaGV " +
                                         "FROM ThongTinGiangVien, GiangVien_HocPhan " +
                                         "WHERE HoTen = N'" + tenGV + "' " +
                                         "AND GiangVien_HocPhan.MaHP = '" + maHP + "'", DatabaseInteract.connect);
                maGV = DatabaseInteract.command.ExecuteScalar().ToString();

                string insert = "INSERT INTO DangKyHocPhan VALUES ('" + MaHV_Confirm.Text + "', '" +
                                                                         maHP + "', '" +
                                                                         maGV + "', '" +
                                                                         ngayDK + "')";

                DatabaseInteract.command = new SqlCommand(insert, DatabaseInteract.connect);
                DatabaseInteract.command.ExecuteNonQuery();
            }

            MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK);
            Reset_Form();
        }

        private void txt_MaHP_Leave(object sender, EventArgs e)
        {
            if (txt_MaHP.Text != "")
            {
                LayThongTinHocPhan(txt_MaHP.Text);
                btn_Perform.Enabled = true;
            }
        }

        private void txt_MaHP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LayThongTinHocPhan(txt_MaHP.Text);
                btn_Perform.Enabled = true;
            }
        }

        private void btn_Perform_Click(object sender, EventArgs e)
        {
            if (select_Date.Checked)
            {
                if (KiemTraDangKyTrungHocPhanTrongCSDL() == true)
                {
                    DuaVaoBangXacNhan();

                    btn_Perform.Enabled = false;
                    btn_Confirm.Enabled = true;
                }
                else
                    MessageBox.Show("Bạn đã đăng ký học phần này rồi!", "Thông báo");
            }
            else
                MessageBox.Show("Vui lòng chọn ngày đăng ký", "Thông báo");
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            DuaVaoCSDL();
            btn_Confirm.Enabled = false;
            select_Date.ResetText();
            select_Date.Checked = false;
        }
    }
}
