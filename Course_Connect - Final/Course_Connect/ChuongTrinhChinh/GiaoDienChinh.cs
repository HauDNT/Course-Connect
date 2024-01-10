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
using Course_Connect.ChuongTrinhChinh.QuanLyHocPhan;
using Course_Connect.ChuongTrinhChinh.QuanLyHocVien;
using Course_Connect.ChuongTrinhChinh.QuanLyGiangVien;
using Course_Connect.ChuongTrinhChinh.GiangDay_HocTap;

namespace Course_Connect.ChuongTrinhChinh
{
    public partial class GiaoDienChinh : Form
    {
        public GiaoDienChinh()
        {
            InitializeComponent();
            LabelName.Text = "Xin chào quản trị viên";
        }

        void Format_Show(Form thisForm)
        {
            panelBackground.Visible = false;
            thisForm.Text = "";
            thisForm.MdiParent = this;
            thisForm.ShowIcon = false;
            thisForm.ControlBox = false;
            thisForm.FormBorderStyle = FormBorderStyle.None;
            thisForm.WindowState = FormWindowState.Maximized;
            thisForm.Show();
        }
               
        private void HocPhan_Select_1_Click(object sender, EventArgs e)
        {
            DangKyHocPhan childForm = new DangKyHocPhan();
            Format_Show(childForm);
        }

        private void HocPhan_Select_2_Click(object sender, EventArgs e)
        {
            DanhSachHocPhan childForm = new DanhSachHocPhan();
            Format_Show(childForm);
        }

        private void HocPhan_Select_3_Click(object sender, EventArgs e)
        {
            ThongTinDangKy childForm = new ThongTinDangKy();
            Format_Show(childForm);
        }

        private void GiangVien_Select_1_Click(object sender, EventArgs e)
        {
            TaiKhoanGiangVien childForm = new TaiKhoanGiangVien();
            Format_Show(childForm);
        }

        private void GiangVien_Select_2_Click(object sender, EventArgs e)
        {
            ThongTinGiangVien childForm = new ThongTinGiangVien();
            Format_Show(childForm);
        }

        private void HocVien_Select_1_Click(object sender, EventArgs e)
        {
            TaiKhoanHocVien childForm = new TaiKhoanHocVien();
            Format_Show(childForm);
        }

        private void HocVien_Select_2_Click(object sender, EventArgs e)
        {
            ThongTinHocVien childForm = new ThongTinHocVien();
            Format_Show(childForm);
        }

        private void DayHoc_Select_1_Click(object sender, EventArgs e)
        {
            PhanCongGiangDay childForm = new PhanCongGiangDay();
            Format_Show(childForm);
        }

        private void DayHoc_Select_2_Click(object sender, EventArgs e)
        {
            LichHoc childForm = new LichHoc();
            Format_Show(childForm);
        }

        private void DayHoc_Select_3_Click(object sender, EventArgs e)
        {
            NhomHoc childForm = new NhomHoc();
            Format_Show(childForm);
        }

        private void GiaoDienChinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình?",
                               "Thông báo",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
            else if (result == DialogResult.Yes)
                Environment.Exit(0);
        }
    }
}
