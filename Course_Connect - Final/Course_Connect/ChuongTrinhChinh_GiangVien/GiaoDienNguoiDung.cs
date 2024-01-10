using System;
using System.Windows.Forms;
using Course_Connect.ChuongTrinhChinh_GiangVien.GiangVien;
using Course_Connect.ChuongTrinhChinh_GiangVien.HocPhan;
using Course_Connect.ChuongTrinhChinh_GiangVien.Day_Hoc;

namespace Course_Connect.ChuongTrinhChinh_GiangVien
{
    public partial class GiaoDienNguoiDung : Form
    {
        public GiaoDienNguoiDung()
        {
            InitializeComponent();
            LabelName.Text = "Xin chào giảng viên: " + Decentralization.LayHoTenNguoiDung(Decentralization.thisUsername, Decentralization.accountType);
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

        private void GiangVien_Select_Click(object sender, EventArgs e)
        {
            ThongTinCaNhan childForm = new ThongTinCaNhan();
            Format_Show(childForm);
        }

        private void HocPhan_Select_Click(object sender, EventArgs e)
        {
            DanhSachHocPhan childForm = new DanhSachHocPhan();
            Format_Show(childForm);
        }

        private void DayHoc_Select_2_Click(object sender, EventArgs e)
        {
            LichDay childForm = new LichDay();
            Format_Show(childForm);
        }

        private void DayHoc_Select_3_Click(object sender, EventArgs e)
        {
            NhomDay childForm = new NhomDay();
            Format_Show(childForm);
        }
    }
}
