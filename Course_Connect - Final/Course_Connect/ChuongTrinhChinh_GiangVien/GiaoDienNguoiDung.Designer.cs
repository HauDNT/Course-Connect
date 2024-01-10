namespace Course_Connect.ChuongTrinhChinh_GiangVien
{
    partial class GiaoDienNguoiDung
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiaoDienNguoiDung));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.GiangVien_Select = new System.Windows.Forms.ToolStripMenuItem();
            this.HocPhan_Select = new System.Windows.Forms.ToolStripMenuItem();
            this.DayHoc_Select = new System.Windows.Forms.ToolStripMenuItem();
            this.DayHoc_Select_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.DayHoc_Select_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelName = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GiangVien_Select,
            this.HocPhan_Select,
            this.DayHoc_Select});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(957, 36);
            this.menuStrip.TabIndex = 14;
            this.menuStrip.Text = "menuStrip1";
            // 
            // GiangVien_Select
            // 
            this.GiangVien_Select.Name = "GiangVien_Select";
            this.GiangVien_Select.Size = new System.Drawing.Size(117, 32);
            this.GiangVien_Select.Text = "Giảng viên";
            this.GiangVien_Select.Click += new System.EventHandler(this.GiangVien_Select_Click);
            // 
            // HocPhan_Select
            // 
            this.HocPhan_Select.Name = "HocPhan_Select";
            this.HocPhan_Select.Size = new System.Drawing.Size(108, 32);
            this.HocPhan_Select.Text = "Học phần";
            this.HocPhan_Select.Click += new System.EventHandler(this.HocPhan_Select_Click);
            // 
            // DayHoc_Select
            // 
            this.DayHoc_Select.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DayHoc_Select_2,
            this.DayHoc_Select_3});
            this.DayHoc_Select.Name = "DayHoc_Select";
            this.DayHoc_Select.Size = new System.Drawing.Size(111, 32);
            this.DayHoc_Select.Text = "Dạy - Học";
            // 
            // DayHoc_Select_2
            // 
            this.DayHoc_Select_2.Name = "DayHoc_Select_2";
            this.DayHoc_Select_2.Size = new System.Drawing.Size(270, 32);
            this.DayHoc_Select_2.Text = "Lịch giảng dạy";
            this.DayHoc_Select_2.Click += new System.EventHandler(this.DayHoc_Select_2_Click);
            // 
            // DayHoc_Select_3
            // 
            this.DayHoc_Select_3.Name = "DayHoc_Select_3";
            this.DayHoc_Select_3.Size = new System.Drawing.Size(270, 32);
            this.DayHoc_Select_3.Text = "Nhóm học phụ trách";
            this.DayHoc_Select_3.Click += new System.EventHandler(this.DayHoc_Select_3_Click);
            // 
            // LabelName
            // 
            this.LabelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelName.BorderColor = System.Drawing.Color.Red;
            this.LabelName.BorderThickness = 0;
            this.LabelName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LabelName.DefaultText = "";
            this.LabelName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.LabelName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.LabelName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.LabelName.DisabledState.Parent = this.LabelName;
            this.LabelName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.LabelName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.LabelName.FocusedState.Parent = this.LabelName;
            this.LabelName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelName.ForeColor = System.Drawing.Color.Black;
            this.LabelName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.LabelName.HoverState.Parent = this.LabelName;
            this.LabelName.Location = new System.Drawing.Point(417, 1);
            this.LabelName.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.LabelName.Name = "LabelName";
            this.LabelName.PasswordChar = '\0';
            this.LabelName.PlaceholderText = "";
            this.LabelName.ReadOnly = true;
            this.LabelName.SelectedText = "";
            this.LabelName.ShadowDecoration.Parent = this.LabelName;
            this.LabelName.Size = new System.Drawing.Size(539, 36);
            this.LabelName.TabIndex = 15;
            this.LabelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelBackground
            // 
            this.panelBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBackground.BackColor = System.Drawing.Color.White;
            this.panelBackground.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelBackground.BackgroundImage")));
            this.panelBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBackground.Location = new System.Drawing.Point(0, 28);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(957, 554);
            this.panelBackground.TabIndex = 16;
            // 
            // GiaoDienNguoiDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 580);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.panelBackground);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "GiaoDienNguoiDung";
            this.Text = "Course Connect";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GiaoDienNguoiDung_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem HocPhan_Select;
        private System.Windows.Forms.ToolStripMenuItem GiangVien_Select;
        private System.Windows.Forms.ToolStripMenuItem DayHoc_Select;
        private System.Windows.Forms.ToolStripMenuItem DayHoc_Select_2;
        private System.Windows.Forms.ToolStripMenuItem DayHoc_Select_3;
        private Guna.UI2.WinForms.Guna2TextBox LabelName;
        private System.Windows.Forms.Panel panelBackground;
    }
}