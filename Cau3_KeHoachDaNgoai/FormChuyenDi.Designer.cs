namespace Cau3_KeHoachDaNgoai
{
    partial class FormChuyenDi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpLang = new System.Windows.Forms.GroupBox();
            this.radVi = new System.Windows.Forms.RadioButton();
            this.radEn = new System.Windows.Forms.RadioButton();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaChuyen = new System.Windows.Forms.Label();
            this.txtMaChuyen = new System.Windows.Forms.TextBox();
            this.lblTenChuyen = new System.Windows.Forms.Label();
            this.txtTenChuyen = new System.Windows.Forms.TextBox();
            this.lblDiaDiem = new System.Windows.Forms.Label();
            this.txtDiaDiem = new System.Windows.Forms.TextBox();
            this.lblNgayDi = new System.Windows.Forms.Label();
            this.dtpNgayDi = new System.Windows.Forms.DateTimePicker();
            this.lblSoNguoi = new System.Windows.Forms.Label();
            this.nudSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblNganSach = new System.Windows.Forms.Label();
            this.nudNganSach = new System.Windows.Forms.NumericUpDown();
            this.lblPhuongTien = new System.Windows.Forms.Label();
            this.cboPhuongTien = new System.Windows.Forms.ComboBox();
            this.chkHoanThanh = new System.Windows.Forms.CheckBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.chkChuaHoanThanh = new System.Windows.Forms.CheckBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnBoLoc = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvChuyenDi = new System.Windows.Forms.DataGridView();
            this.pnlThoiTiet = new System.Windows.Forms.Panel();
            this.btnXemThoiTiet = new System.Windows.Forms.Button();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.lblThoiTietIcon = new System.Windows.Forms.Label();
            this.lblThoiTietMoTa = new System.Windows.Forms.Label();
            this.lblNhietDo = new System.Windows.Forms.Label();
            this.lblXacSuatMua = new System.Windows.Forms.Label();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.grpLang.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNganSach)).BeginInit();
            this.grpTimKiem.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuyenDi)).BeginInit();
            this.pnlThoiTiet.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.pnlTop.Controls.Add(this.grpLang);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1080, 52);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(434, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KẾ HOẠCH DÃ NGOẠI & DỰ BÁO THỜI TIẾT";
            // 
            // grpLang
            // 
            this.grpLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLang.Controls.Add(this.radEn);
            this.grpLang.Controls.Add(this.radVi);
            this.grpLang.ForeColor = System.Drawing.Color.White;
            this.grpLang.Location = new System.Drawing.Point(880, 3);
            this.grpLang.Name = "grpLang";
            this.grpLang.Size = new System.Drawing.Size(188, 44);
            this.grpLang.TabIndex = 1;
            this.grpLang.TabStop = false;
            this.grpLang.Text = "Language / Ngôn ngữ";
            // 
            // radVi
            // 
            this.radVi.AutoSize = true;
            this.radVi.Checked = true;
            this.radVi.Location = new System.Drawing.Point(10, 18);
            this.radVi.Name = "radVi";
            this.radVi.Size = new System.Drawing.Size(83, 21);
            this.radVi.TabIndex = 0;
            this.radVi.TabStop = true;
            this.radVi.Text = "Tiếng Việt";
            this.radVi.UseVisualStyleBackColor = true;
            // 
            // radEn
            // 
            this.radEn.AutoSize = true;
            this.radEn.Location = new System.Drawing.Point(100, 18);
            this.radEn.Name = "radEn";
            this.radEn.Size = new System.Drawing.Size(68, 21);
            this.radEn.TabIndex = 1;
            this.radEn.Text = "English";
            this.radEn.UseVisualStyleBackColor = true;
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblMaChuyen);
            this.grpThongTin.Controls.Add(this.txtMaChuyen);
            this.grpThongTin.Controls.Add(this.lblTenChuyen);
            this.grpThongTin.Controls.Add(this.txtTenChuyen);
            this.grpThongTin.Controls.Add(this.lblDiaDiem);
            this.grpThongTin.Controls.Add(this.txtDiaDiem);
            this.grpThongTin.Controls.Add(this.lblNgayDi);
            this.grpThongTin.Controls.Add(this.dtpNgayDi);
            this.grpThongTin.Controls.Add(this.lblSoNguoi);
            this.grpThongTin.Controls.Add(this.nudSoNguoi);
            this.grpThongTin.Controls.Add(this.lblNganSach);
            this.grpThongTin.Controls.Add(this.nudNganSach);
            this.grpThongTin.Controls.Add(this.lblPhuongTien);
            this.grpThongTin.Controls.Add(this.cboPhuongTien);
            this.grpThongTin.Controls.Add(this.chkHoanThanh);
            this.grpThongTin.Controls.Add(this.lblGhiChu);
            this.grpThongTin.Controls.Add(this.txtGhiChu);
            this.grpThongTin.Controls.Add(this.btnThem);
            this.grpThongTin.Controls.Add(this.btnSua);
            this.grpThongTin.Controls.Add(this.btnXoa);
            this.grpThongTin.Controls.Add(this.btnLamMoi);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpThongTin.Location = new System.Drawing.Point(12, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(680, 235);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chuyến đi";
            // 
            // lblMaChuyen
            // 
            this.lblMaChuyen.AutoSize = true;
            this.lblMaChuyen.Location = new System.Drawing.Point(12, 28);
            this.lblMaChuyen.Name = "lblMaChuyen";
            this.lblMaChuyen.Size = new System.Drawing.Size(73, 17);
            this.lblMaChuyen.TabIndex = 0;
            this.lblMaChuyen.Text = "Mã chuyến:";
            // 
            // txtMaChuyen
            // 
            this.txtMaChuyen.Location = new System.Drawing.Point(92, 25);
            this.txtMaChuyen.Name = "txtMaChuyen";
            this.txtMaChuyen.ReadOnly = true;
            this.txtMaChuyen.Size = new System.Drawing.Size(65, 24);
            this.txtMaChuyen.TabIndex = 1;
            // 
            // lblTenChuyen
            // 
            this.lblTenChuyen.AutoSize = true;
            this.lblTenChuyen.Location = new System.Drawing.Point(165, 28);
            this.lblTenChuyen.Name = "lblTenChuyen";
            this.lblTenChuyen.Size = new System.Drawing.Size(75, 17);
            this.lblTenChuyen.TabIndex = 2;
            this.lblTenChuyen.Text = "Tên chuyến:";
            // 
            // txtTenChuyen
            // 
            this.txtTenChuyen.Location = new System.Drawing.Point(245, 25);
            this.txtTenChuyen.Name = "txtTenChuyen";
            this.txtTenChuyen.Size = new System.Drawing.Size(225, 24);
            this.txtTenChuyen.TabIndex = 3;
            // 
            // lblDiaDiem
            // 
            this.lblDiaDiem.AutoSize = true;
            this.lblDiaDiem.Location = new System.Drawing.Point(475, 28);
            this.lblDiaDiem.Name = "lblDiaDiem";
            this.lblDiaDiem.Size = new System.Drawing.Size(62, 17);
            this.lblDiaDiem.TabIndex = 4;
            this.lblDiaDiem.Text = "Địa điểm:";
            // 
            // txtDiaDiem
            // 
            this.txtDiaDiem.Location = new System.Drawing.Point(542, 25);
            this.txtDiaDiem.Name = "txtDiaDiem";
            this.txtDiaDiem.Size = new System.Drawing.Size(125, 24);
            this.txtDiaDiem.TabIndex = 5;
            // 
            // lblNgayDi
            // 
            this.lblNgayDi.AutoSize = true;
            this.lblNgayDi.Location = new System.Drawing.Point(12, 65);
            this.lblNgayDi.Name = "lblNgayDi";
            this.lblNgayDi.Size = new System.Drawing.Size(57, 17);
            this.lblNgayDi.TabIndex = 6;
            this.lblNgayDi.Text = "Ngày đi:";
            // 
            // dtpNgayDi
            // 
            this.dtpNgayDi.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayDi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDi.Location = new System.Drawing.Point(92, 62);
            this.dtpNgayDi.Name = "dtpNgayDi";
            this.dtpNgayDi.Size = new System.Drawing.Size(115, 24);
            this.dtpNgayDi.TabIndex = 7;
            // 
            // lblSoNguoi
            // 
            this.lblSoNguoi.AutoSize = true;
            this.lblSoNguoi.Location = new System.Drawing.Point(220, 65);
            this.lblSoNguoi.Name = "lblSoNguoi";
            this.lblSoNguoi.Size = new System.Drawing.Size(63, 17);
            this.lblSoNguoi.TabIndex = 8;
            this.lblSoNguoi.Text = "Số người:";
            // 
            // nudSoNguoi
            // 
            this.nudSoNguoi.Location = new System.Drawing.Point(288, 62);
            this.nudSoNguoi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoNguoi.Name = "nudSoNguoi";
            this.nudSoNguoi.Size = new System.Drawing.Size(60, 24);
            this.nudSoNguoi.TabIndex = 9;
            this.nudSoNguoi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNganSach
            // 
            this.lblNganSach.AutoSize = true;
            this.lblNganSach.Location = new System.Drawing.Point(365, 65);
            this.lblNganSach.Name = "lblNganSach";
            this.lblNganSach.Size = new System.Drawing.Size(73, 17);
            this.lblNganSach.TabIndex = 10;
            this.lblNganSach.Text = "Ngân sách:";
            // 
            // nudNganSach
            // 
            this.nudNganSach.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudNganSach.Location = new System.Drawing.Point(442, 62);
            this.nudNganSach.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudNganSach.Name = "nudNganSach";
            this.nudNganSach.Size = new System.Drawing.Size(110, 24);
            this.nudNganSach.TabIndex = 11;
            this.nudNganSach.ThousandsSeparator = true;
            // 
            // lblPhuongTien
            // 
            this.lblPhuongTien.AutoSize = true;
            this.lblPhuongTien.Location = new System.Drawing.Point(12, 102);
            this.lblPhuongTien.Name = "lblPhuongTien";
            this.lblPhuongTien.Size = new System.Drawing.Size(81, 17);
            this.lblPhuongTien.TabIndex = 12;
            this.lblPhuongTien.Text = "Phương tiện:";
            // 
            // cboPhuongTien
            // 
            this.cboPhuongTien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongTien.FormattingEnabled = true;
            this.cboPhuongTien.Items.AddRange(new object[] {
            "Xe máy",
            "Ô tô",
            "Xe khách",
            "Tàu hỏa",
            "Máy bay"});
            this.cboPhuongTien.Location = new System.Drawing.Point(92, 99);
            this.cboPhuongTien.Name = "cboPhuongTien";
            this.cboPhuongTien.Size = new System.Drawing.Size(115, 25);
            this.cboPhuongTien.TabIndex = 13;
            // 
            // chkHoanThanh
            // 
            this.chkHoanThanh.AutoSize = true;
            this.chkHoanThanh.Location = new System.Drawing.Point(223, 101);
            this.chkHoanThanh.Name = "chkHoanThanh";
            this.chkHoanThanh.Size = new System.Drawing.Size(107, 21);
            this.chkHoanThanh.TabIndex = 14;
            this.chkHoanThanh.Text = "Đã hoàn thành";
            this.chkHoanThanh.UseVisualStyleBackColor = true;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(12, 140);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(55, 17);
            this.lblGhiChu.TabIndex = 15;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(92, 137);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(575, 42);
            this.txtGhiChu.TabIndex = 16;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(92, 190);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(85, 32);
            this.btnThem.TabIndex = 17;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(190, 190);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(85, 32);
            this.btnSua.TabIndex = 18;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(288, 190);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(85, 32);
            this.btnXoa.TabIndex = 19;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(386, 190);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(85, 32);
            this.btnLamMoi.TabIndex = 20;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // pnlThoiTiet
            // 
            this.pnlThoiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlThoiTiet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlThoiTiet.Controls.Add(this.btnXemThoiTiet);
            this.pnlThoiTiet.Controls.Add(this.prgLoading);
            this.pnlThoiTiet.Controls.Add(this.lblThoiTietIcon);
            this.pnlThoiTiet.Controls.Add(this.lblThoiTietMoTa);
            this.pnlThoiTiet.Controls.Add(this.lblNhietDo);
            this.pnlThoiTiet.Controls.Add(this.lblXacSuatMua);
            this.pnlThoiTiet.Controls.Add(this.lblCanhBao);
            this.pnlThoiTiet.Location = new System.Drawing.Point(702, 65);
            this.pnlThoiTiet.Name = "pnlThoiTiet";
            this.pnlThoiTiet.Size = new System.Drawing.Size(366, 228);
            this.pnlThoiTiet.TabIndex = 2;
            // 
            // btnXemThoiTiet
            // 
            this.btnXemThoiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnXemThoiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemThoiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemThoiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXemThoiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemThoiTiet.Location = new System.Drawing.Point(15, 12);
            this.btnXemThoiTiet.Name = "btnXemThoiTiet";
            this.btnXemThoiTiet.Size = new System.Drawing.Size(335, 36);
            this.btnXemThoiTiet.TabIndex = 0;
            this.btnXemThoiTiet.Text = "Xem thời tiết điểm đến";
            this.btnXemThoiTiet.UseVisualStyleBackColor = false;
            // 
            // prgLoading
            // 
            this.prgLoading.Location = new System.Drawing.Point(15, 54);
            this.prgLoading.MarqueeAnimationSpeed = 30;
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(335, 10);
            this.prgLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgLoading.TabIndex = 1;
            this.prgLoading.Visible = false;
            // 
            // lblThoiTietIcon
            // 
            this.lblThoiTietIcon.AutoSize = true;
            this.lblThoiTietIcon.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblThoiTietIcon.Location = new System.Drawing.Point(15, 70);
            this.lblThoiTietIcon.Name = "lblThoiTietIcon";
            this.lblThoiTietIcon.Size = new System.Drawing.Size(94, 65);
            this.lblThoiTietIcon.TabIndex = 2;
            this.lblThoiTietIcon.Text = "🌤️";
            // 
            // lblThoiTietMoTa
            // 
            this.lblThoiTietMoTa.AutoSize = true;
            this.lblThoiTietMoTa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblThoiTietMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblThoiTietMoTa.Location = new System.Drawing.Point(120, 75);
            this.lblThoiTietMoTa.Name = "lblThoiTietMoTa";
            this.lblThoiTietMoTa.Size = new System.Drawing.Size(155, 20);
            this.lblThoiTietMoTa.TabIndex = 3;
            this.lblThoiTietMoTa.Text = "Chọn chuyến để xem";
            // 
            // lblNhietDo
            // 
            this.lblNhietDo.AutoSize = true;
            this.lblNhietDo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNhietDo.Location = new System.Drawing.Point(120, 100);
            this.lblNhietDo.Name = "lblNhietDo";
            this.lblNhietDo.Size = new System.Drawing.Size(107, 17);
            this.lblNhietDo.TabIndex = 4;
            this.lblNhietDo.Text = "Nhiệt độ: -- / -- °C";
            // 
            // lblXacSuatMua
            // 
            this.lblXacSuatMua.AutoSize = true;
            this.lblXacSuatMua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblXacSuatMua.Location = new System.Drawing.Point(120, 122);
            this.lblXacSuatMua.Name = "lblXacSuatMua";
            this.lblXacSuatMua.Size = new System.Drawing.Size(126, 17);
            this.lblXacSuatMua.TabIndex = 5;
            this.lblXacSuatMua.Text = "Xác suất mưa: -- %";
            // 
            // lblCanhBao
            // 
            this.lblCanhBao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCanhBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblCanhBao.Location = new System.Drawing.Point(15, 145);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Size = new System.Drawing.Size(335, 70);
            this.lblCanhBao.TabIndex = 6;
            this.lblCanhBao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Controls.Add(this.lblTimKiem);
            this.grpTimKiem.Controls.Add(this.txtTimKiem);
            this.grpTimKiem.Controls.Add(this.chkChuaHoanThanh);
            this.grpTimKiem.Controls.Add(this.lblTuNgay);
            this.grpTimKiem.Controls.Add(this.dtpTuNgay);
            this.grpTimKiem.Controls.Add(this.lblDenNgay);
            this.grpTimKiem.Controls.Add(this.dtpDenNgay);
            this.grpTimKiem.Controls.Add(this.btnLoc);
            this.grpTimKiem.Controls.Add(this.btnBoLoc);
            this.grpTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpTimKiem.Location = new System.Drawing.Point(12, 299);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(1056, 62);
            this.grpTimKiem.TabIndex = 3;
            this.grpTimKiem.TabStop = false;
            this.grpTimKiem.Text = "Tìm kiếm & Lọc chuyến đi";
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(12, 27);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(91, 17);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tên/Địa điểm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(105, 24);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm tên hoặc địa điểm...";
            this.txtTimKiem.Size = new System.Drawing.Size(180, 24);
            this.txtTimKiem.TabIndex = 1;
            // 
            // chkChuaHoanThanh
            // 
            this.chkChuaHoanThanh.AutoSize = true;
            this.chkChuaHoanThanh.Location = new System.Drawing.Point(300, 26);
            this.chkChuaHoanThanh.Name = "chkChuaHoanThanh";
            this.chkChuaHoanThanh.Size = new System.Drawing.Size(202, 21);
            this.chkChuaHoanThanh.TabIndex = 2;
            this.chkChuaHoanThanh.Text = "Chỉ hiện chuyến chưa hoàn thành";
            this.chkChuaHoanThanh.UseVisualStyleBackColor = true;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(520, 27);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(58, 17);
            this.lblTuNgay.TabIndex = 3;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(582, 24);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(110, 24);
            this.dtpTuNgay.TabIndex = 4;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(705, 27);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(66, 17);
            this.lblDenNgay.TabIndex = 5;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(775, 24);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(110, 24);
            this.dtpDenNgay.TabIndex = 6;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(900, 22);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(65, 28);
            this.btnLoc.TabIndex = 7;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            // 
            // btnBoLoc
            // 
            this.btnBoLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnBoLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBoLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBoLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBoLoc.ForeColor = System.Drawing.Color.White;
            this.btnBoLoc.Location = new System.Drawing.Point(975, 22);
            this.btnBoLoc.Name = "btnBoLoc";
            this.btnBoLoc.Size = new System.Drawing.Size(65, 28);
            this.btnBoLoc.TabIndex = 8;
            this.btnBoLoc.Text = "Bỏ lọc";
            this.btnBoLoc.UseVisualStyleBackColor = false;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgvChuyenDi);
            this.pnlGrid.Location = new System.Drawing.Point(12, 367);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1056, 270);
            this.pnlGrid.TabIndex = 4;
            // 
            // dgvChuyenDi
            // 
            this.dgvChuyenDi.AllowUserToAddRows = false;
            this.dgvChuyenDi.AllowUserToDeleteRows = false;
            this.dgvChuyenDi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChuyenDi.BackgroundColor = System.Drawing.Color.White;
            this.dgvChuyenDi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChuyenDi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChuyenDi.Location = new System.Drawing.Point(0, 0);
            this.dgvChuyenDi.MultiSelect = false;
            this.dgvChuyenDi.Name = "dgvChuyenDi";
            this.dgvChuyenDi.ReadOnly = true;
            this.dgvChuyenDi.RowHeadersWidth = 51;
            this.dgvChuyenDi.RowTemplate.Height = 29;
            this.dgvChuyenDi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChuyenDi.Size = new System.Drawing.Size(1056, 270);
            this.dgvChuyenDi.TabIndex = 0;
            // 
            // FormChuyenDi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1080, 649);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.pnlThoiTiet);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormChuyenDi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kế hoạch dã ngoại & Dự báo thời tiết";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpLang.ResumeLayout(false);
            this.grpLang.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNganSach)).EndInit();
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuyenDi)).EndInit();
            this.pnlThoiTiet.ResumeLayout(false);
            this.pnlThoiTiet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpLang;
        private System.Windows.Forms.RadioButton radVi;
        private System.Windows.Forms.RadioButton radEn;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaChuyen;
        private System.Windows.Forms.TextBox txtMaChuyen;
        private System.Windows.Forms.Label lblTenChuyen;
        private System.Windows.Forms.TextBox txtTenChuyen;
        private System.Windows.Forms.Label lblDiaDiem;
        private System.Windows.Forms.TextBox txtDiaDiem;
        private System.Windows.Forms.Label lblNgayDi;
        private System.Windows.Forms.DateTimePicker dtpNgayDi;
        private System.Windows.Forms.Label lblSoNguoi;
        private System.Windows.Forms.NumericUpDown nudSoNguoi;
        private System.Windows.Forms.Label lblNganSach;
        private System.Windows.Forms.NumericUpDown nudNganSach;
        private System.Windows.Forms.Label lblPhuongTien;
        private System.Windows.Forms.ComboBox cboPhuongTien;
        private System.Windows.Forms.CheckBox chkHoanThanh;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlThoiTiet;
        private System.Windows.Forms.Button btnXemThoiTiet;
        private System.Windows.Forms.ProgressBar prgLoading;
        private System.Windows.Forms.Label lblThoiTietIcon;
        private System.Windows.Forms.Label lblThoiTietMoTa;
        private System.Windows.Forms.Label lblNhietDo;
        private System.Windows.Forms.Label lblXacSuatMua;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.CheckBox chkChuaHoanThanh;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnBoLoc;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvChuyenDi;
    }
}
