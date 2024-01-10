using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course_Connect.ChuongTrinhChinh_HocVien.HocVien;
using Course_Connect.ChuongTrinhChinh_HocVien.HocPhan;
using Course_Connect.ChuongTrinhChinh_HocVien.GiangVien;
using Course_Connect.ChuongTrinhChinh_HocVien.Day_Hoc;

namespace Course_Connect.ChuongTrinhChinh_HocVien
{
    public partial class GiaoDienNguoiDung : Form
    {
        public GiaoDienNguoiDung()
        {
            InitializeComponent();
            LabelName.Text = "Xin chào học viên: " + Decentralization.LayHoTenNguoiDung(Decentralization.thisUsername, Decentralization.accountType);
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

        private void GiaoDienNguoiDung_FormClosing(object sender, FormClosingEventArgs e)
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

        private void HocVien_Select_Click(object sender, EventArgs e)
        {
            ThongTinCaNhan childForm = new ThongTinCaNhan();
            Format_Show(childForm);
        }

        private void HocPhan_Select_2_Click(object sender, EventArgs e)
        {
            DanhSachHocPhan childForm = new DanhSachHocPhan();
            Format_Show(childForm);
        }

        private void HocPhan_Select_1_Click(object sender, EventArgs e)
        {
            DangKyHocPhan childForm = new DangKyHocPhan();
            Format_Show(childForm);
        }

        private void GiangVien_Select_Click(object sender, EventArgs e)
        {
            ThongTinGiangVien childForm = new ThongTinGiangVien();
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

        private void HocPhan_Select_3_Click(object sender, EventArgs e)
        {
            ThongTinDangKy childForm = new ThongTinDangKy();
            Format_Show(childForm);
        }
    }
}
