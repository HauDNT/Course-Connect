using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Course_Connect.ChuongTrinhChinh.GiangDay_HocTap;

namespace Course_Connect.ChuongTrinhChinh.QuanLyHocPhan
{
    public partial class DangKyHocPhan : Form
    {
        public DangKyHocPhan()
        {
            InitializeComponent();
        }

        bool KiemTraThongTin()
        {
            string query = "SELECT COUNT(*) FROM ThongTinHocVien WHERE MaHV = '" + txt_MaHV.Text + "' AND HoTen = N'" + txt_TenHV.Text + "'";
            DatabaseInteract.command = new SqlCommand(query, DatabaseInteract.connect);
            int count = (int)DatabaseInteract.command.ExecuteScalar();

            if (count == 0)
            {
                MessageBox.Show("Mã học viên hoặc họ tên không đúng. Hãy kiểm tra lại.");
                return false;
            }
            
            return true;
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
            for(int i = 0; i < DGV_DangKy.Rows.Count; i++)
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
            txt_MaHV.Text = "";
            txt_TenHV.Text = "";
            MaHV_Confirm.Text = "";
            TenHV_Confirm.Text = "";
        }

        void Reset_Form()
        {
            Reset_ThongTinHocVien();
            Reset_ThongTinHocPhan();
            DGV_DangKy.Rows.Clear();
            txt_MaHV.ReadOnly = false;
            txt_TenHV.ReadOnly = false;
        }

        void Khoa_XuatThongTin()
        {
            // Khóa lại thông tin học viên:
            txt_MaHV.ReadOnly = true;
            txt_TenHV.ReadOnly = true;

            // Hiển thị thông tin tín chỉ và đăng ký:
            LayThongTinHocPhan(txt_MaHP.Text);

            btn_Perform.Enabled = true;
        }

        bool KiemTraDangKyTrungHocPhanTrongCSDL()
        {
            DatabaseInteract.command = new SqlCommand("SELECT COUNT(*) FROM DangKyHocPhan WHERE " +
                         "MaHV = '" + txt_MaHV.Text +
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

                NhomHoc goiHamLayMaGV = new NhomHoc();
                maGV = goiHamLayMaGV.LayMaGVTuHoTen(tenGV, maHP);

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
            if (txt_MaHP.Text != "" && KiemTraThongTin() == true)
                Khoa_XuatThongTin();
        }

        private void txt_MaHP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && KiemTraThongTin() == true)
                Khoa_XuatThongTin();
        }

        private void btn_Perform_Click(object sender, EventArgs e)
        {
            if (select_Date.Checked)
            {
                if (KiemTraDangKyTrungHocPhanTrongCSDL() == true)
                {
                    MaHV_Confirm.Text = txt_MaHV.Text;
                    TenHV_Confirm.Text = txt_TenHV.Text;
                    DuaVaoBangXacNhan();

                    btn_Perform.Enabled = false;
                    btn_Confirm.Enabled = true;
                }
                else
                    MessageBox.Show("Học viên này đã đăng ký học phần này rồi!", "Thông báo");
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
