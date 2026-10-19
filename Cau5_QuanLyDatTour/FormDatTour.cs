using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau5_QuanLyDatTour.Data;
using Cau5_QuanLyDatTour.Models;
using Cau5_QuanLyDatTour.Services;

namespace Cau5_QuanLyDatTour
{
    public class DatTourDisplayDTO
    {
        public int MaDat { get; set; }
        public string HoTenKH { get; set; } = string.Empty;
        public string TenTour { get; set; } = string.Empty;
        public DateTime NgayKhoiHanh { get; set; }
        public int SoNguoi { get; set; }
        public decimal TyGia { get; set; }
        public decimal TongTienVND { get; set; }
        public DateTime NgayDat { get; set; }
    }

    public partial class FormDatTour : Form
    {
        private List<Tour> _allTours = new();
        private List<KhachHang> _allKhachHangs = new();
        private List<DatTourDisplayDTO> _allDatTours = new();
        private decimal _currentTyGia = 1;

        public FormDatTour()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            this.Load += async (s, e) => await FormDatTour_LoadAsync();

            // Nghiệp vụ Đặt tour
            cboDatTour.SelectedIndexChanged += async (s, e) => await CboDatTour_SelectedIndexChangedAsync();
            nudSoNguoiDat.ValueChanged += (s, e) => TinhTongTienDatTour();
            btnDatTour.Click += async (s, e) => await BtnDatTour_ClickAsync();
            txtLocKH.TextChanged += (s, e) => ApplyFilterDatTour();
            txtLocTour.TextChanged += (s, e) => ApplyFilterDatTour();
            cboLocThang.SelectedIndexChanged += (s, e) => ApplyFilterDatTour();
            btnBoLocDat.Click += BtnBoLocDat_Click;

            // Tab Tour
            btnThemTour.Click += BtnThemTour_Click;
            btnSuaTour.Click += BtnSuaTour_Click;
            btnXoaTour.Click += BtnXoaTour_Click;
            btnLamMoiTour.Click += (s, e) => ClearTourInputs();
            txtTimTour.TextChanged += (s, e) => ApplyFilterTour();
            btnBoLocTour.Click += (s, e) => { txtTimTour.Clear(); ApplyFilterTour(); };
            dgvTour.CellClick += DgvTour_CellClick;

            // Tab Khách hàng
            btnThemKH.Click += BtnThemKH_Click;
            btnSuaKH.Click += BtnSuaKH_Click;
            btnXoaKH.Click += BtnXoaKH_Click;
            btnLamMoiKH.Click += (s, e) => ClearKHInputs();
            txtTimKH.TextChanged += (s, e) => ApplyFilterKH();
            btnBoLocKH.Click += (s, e) => { txtTimKH.Clear(); ApplyFilterKH(); };
            dgvKhachHang.CellClick += DgvKhachHang_CellClick;
        }

        private async Task FormDatTour_LoadAsync()
        {
            this.Text = "Quản lý Đặt Tour Du Lịch v1.0.0 (API Tỷ giá)";
            cboNgoaiTe.SelectedIndex = 0;
            cboLocThang.SelectedIndex = 0;
            dtpNgayKhoiHanh.Value = DateTime.Today.AddDays(15);

            ConfigureGridColumns();
            LoadKhachHang();
            LoadTour();
            LoadDatTour();

            if (cboDatTour.SelectedItem != null)
            {
                await CboDatTour_SelectedIndexChangedAsync();
            }
        }

        private void ConfigureGridColumns()
        {
            // Grid Đặt tour
            dgvDatTour.AutoGenerateColumns = false;
            dgvDatTour.Columns.Clear();
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaDat", HeaderText = "Mã Đơn", Width = 70 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTenKH", HeaderText = "Khách hàng", Width = 170 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenTour", HeaderText = "Tên chương trình Tour", Width = 250 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayKhoiHanh", HeaderText = "Ngày đi", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, Width = 100 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoNguoi", HeaderText = "Số người", Width = 75 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TyGia", HeaderText = "Tỷ giá áp dụng", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 110 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TongTienVND", HeaderText = "Tổng tiền (VNĐ)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 140 });
            dgvDatTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayDat", HeaderText = "Ngày đặt", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, Width = 130 });

            // Grid Tour
            dgvTour.AutoGenerateColumns = false;
            dgvTour.Columns.Clear();
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaTour", HeaderText = "Mã", Width = 50 });
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenTour", HeaderText = "Tên Tour", Width = 280 });
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiemDen", HeaderText = "Điểm đến", Width = 140 });
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgoaiTe", HeaderText = "Tiền tệ", Width = 70 });
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiaNgoaiTe", HeaderText = "Giá gốc", DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 110 });
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayKhoiHanh", HeaderText = "Khởi hành", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }, Width = 110 });
            dgvTour.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoChoToiDa", HeaderText = "Chỗ tối đa", Width = 90 });

            // Grid Khách hàng
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.Columns.Clear();
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaKH", HeaderText = "Mã KH", Width = 70 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ và tên", Width = 250 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoDienThoai", HeaderText = "Số điện thoại", Width = 160 });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        #region TAB ĐẶT TOUR
        private async Task CboDatTour_SelectedIndexChangedAsync()
        {
            if (cboDatTour.SelectedItem is not Tour tour) return;

            string ngoaiTe = tour.NgoaiTe.Trim();
            lblGiaNgoaiTeVal.Text = $"Giá/người (ngoại tệ): {tour.GiaNgoaiTe:N2} {ngoaiTe}";
            lblTyGiaVal.Text = $"Đang lấy tỷ giá {ngoaiTe} từ API...";

            try
            {
                _currentTyGia = await TyGiaService.LayTyGiaAsync(ngoaiTe);
                lblTyGiaVal.Text = $"Tỷ giá hôm nay: 1 {ngoaiTe} = {_currentTyGia:N2} VND";

                decimal giaVND = tour.GiaNgoaiTe * _currentTyGia;
                lblGiaVNDVal.Text = $"Giá/người (VND): {giaVND:N0} VND";

                // Tính số chỗ còn lại
                int soChoConLai = TinhSoChoConLai(tour.MaTour, tour.SoChoToiDa);
                lblSoChoConLai.Text = $"Số chỗ còn lại: {soChoConLai} / {tour.SoChoToiDa} chỗ";

                if (soChoConLai > 0)
                {
                    nudSoNguoiDat.Maximum = soChoConLai;
                    lblSoChoConLai.ForeColor = Color.FromArgb(41, 128, 185);
                    btnDatTour.Enabled = true;
                }
                else
                {
                    nudSoNguoiDat.Maximum = 0;
                    lblSoChoConLai.ForeColor = Color.Red;
                    lblSoChoConLai.Text += " (ĐÃ HẾT CHỖ)";
                    btnDatTour.Enabled = false;
                }

                TinhTongTienDatTour();
            }
            catch (Exception ex)
            {
                lblTyGiaVal.Text = "Không lấy được tỷ giá: " + ex.Message;
                lblGiaVNDVal.Text = "Giá/người (VND): -- VND";
                lblTongTienVNDVal.Text = "TỔNG TIỀN: -- VNĐ";
            }
        }

        private int TinhSoChoConLai(int maTour, int soChoToiDa)
        {
            try
            {
                using var context = new QLDatTourContext();
                int daDat = context.DatTours
                    .Where(d => d.MaTour == maTour)
                    .Sum(d => (int?)d.SoNguoi) ?? 0;

                int conLai = soChoToiDa - daDat;
                return Math.Max(0, conLai);
            }
            catch
            {
                return soChoToiDa;
            }
        }

        private void TinhTongTienDatTour()
        {
            if (cboDatTour.SelectedItem is not Tour tour) return;

            int soNguoi = (int)nudSoNguoiDat.Value;
            decimal tong = tour.GiaNgoaiTe * soNguoi * _currentTyGia;
            lblTongTienVNDVal.Text = $"TỔNG TIỀN: {tong:N0} VNĐ";
        }

        private async Task BtnDatTour_ClickAsync()
        {
            if (cboDatKH.SelectedItem is not KhachHang kh)
            {
                MessageBox.Show("Vui lòng chọn khách hàng đặt tour!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboDatTour.SelectedItem is not Tour tour)
            {
                MessageBox.Show("Vui lòng chọn chương trình tour!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soNguoi = (int)nudSoNguoiDat.Value;
            int soChoConLai = TinhSoChoConLai(tour.MaTour, tour.SoChoToiDa);

            // Ràng buộc không vượt quá số chỗ còn lại
            if (soNguoi > soChoConLai)
            {
                MessageBox.Show($"Số lượng vé đặt ({soNguoi} người) vượt quá số chỗ còn lại của tour ({soChoConLai} chỗ)!",
                    "Cảnh báo hết chỗ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongTien = tour.GiaNgoaiTe * soNguoi * _currentTyGia;

            try
            {
                using var context = new QLDatTourContext();
                var datTour = new DatTour
                {
                    MaKH = kh.MaKH,
                    MaTour = tour.MaTour,
                    SoNguoi = soNguoi,
                    NgayDat = DateTime.Now,
                    TyGia = _currentTyGia, // Ghi nhận tỷ giá cố định tại thời điểm chốt đơn
                    TongTienVND = Math.Round(tongTien, 0)
                };

                context.DatTours.Add(datTour);
                context.SaveChanges();

                LoggerService.LogInfo($"Đặt tour thành công: MaDat={datTour.MaDat}, Khach={kh.HoTen}, Tour={tour.TenTour}, TyGia={_currentTyGia}, TongTien={tongTien:N0} VND");
                MessageBox.Show($"Đặt tour thành công!\n• Khách hàng: {kh.HoTen}\n• Tour: {tour.TenTour}\n• Số vé: {soNguoi} người\n• Tỷ giá áp dụng: {_currentTyGia:N2} VND/{tour.NgoaiTe.Trim()}\n• Thành tiền: {tongTien:N0} VNĐ",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDatTour();
                await CboDatTour_SelectedIndexChangedAsync();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi lưu đơn đặt tour.");
                MessageBox.Show("Đã xảy ra lỗi khi đặt tour: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDatTour()
        {
            try
            {
                using var context = new QLDatTourContext();
                _allDatTours = context.DatTours
                    .Include(d => d.KhachHang)
                    .Include(d => d.Tour)
                    .OrderByDescending(d => d.NgayDat)
                    .Select(d => new DatTourDisplayDTO
                    {
                        MaDat = d.MaDat,
                        HoTenKH = d.KhachHang != null ? d.KhachHang.HoTen : string.Empty,
                        TenTour = d.Tour != null ? d.Tour.TenTour : string.Empty,
                        NgayKhoiHanh = d.Tour != null ? d.Tour.NgayKhoiHanh : DateTime.MinValue,
                        SoNguoi = d.SoNguoi,
                        TyGia = d.TyGia,
                        TongTienVND = d.TongTienVND,
                        NgayDat = d.NgayDat
                    })
                    .ToList();

                ApplyFilterDatTour();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi nạp danh sách đặt tour.");
            }
        }

        private void ApplyFilterDatTour()
        {
            var query = _allDatTours.AsEnumerable();

            string kh = txtLocKH.Text.Trim();
            if (!string.IsNullOrEmpty(kh))
            {
                query = query.Where(d => d.HoTenKH.Contains(kh, StringComparison.OrdinalIgnoreCase));
            }

            string tour = txtLocTour.Text.Trim();
            if (!string.IsNullOrEmpty(tour))
            {
                query = query.Where(d => d.TenTour.Contains(tour, StringComparison.OrdinalIgnoreCase));
            }

            string thang = cboLocThang.SelectedItem?.ToString() ?? "Tất cả";
            if (thang != "Tất cả")
            {
                int monthNum = thang switch
                {
                    "Tháng 10" => 10,
                    "Tháng 11" => 11,
                    "Tháng 12" => 12,
                    _ => 0
                };
                if (monthNum > 0)
                {
                    query = query.Where(d => d.NgayKhoiHanh.Month == monthNum);
                }
            }

            var list = query.ToList();
            dgvDatTour.DataSource = list;

            decimal totalRevenue = list.Sum(d => d.TongTienVND);
            lblTongDoanhThu.Text = $"Tổng doanh thu: {totalRevenue:N0} VNĐ";
        }

        private void BtnBoLocDat_Click(object? sender, EventArgs e)
        {
            txtLocKH.Clear();
            txtLocTour.Clear();
            cboLocThang.SelectedIndex = 0;
            ApplyFilterDatTour();
        }
        #endregion

        #region TAB TOUR
        private void LoadTour()
        {
            try
            {
                using var context = new QLDatTourContext();
                _allTours = context.Tours.OrderBy(t => t.NgayKhoiHanh).ToList();

                cboDatTour.DataSource = null;
                cboDatTour.DisplayMember = "TenTour";
                cboDatTour.ValueMember = "MaTour";
                cboDatTour.DataSource = _allTours.ToList();

                ApplyFilterTour();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi nạp tour.");
            }
        }

        private void ApplyFilterTour()
        {
            var query = _allTours.AsEnumerable();
            string keyword = txtTimTour.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(t => t.TenTour.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         t.DiemDen.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }
            dgvTour.DataSource = query.ToList();
        }

        private void DgvTour_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTour.Rows.Count) return;

            if (dgvTour.Rows[e.RowIndex].DataBoundItem is Tour t)
            {
                txtMaTour.Text = t.MaTour.ToString();
                txtTenTour.Text = t.TenTour;
                txtDiemDen.Text = t.DiemDen;
                cboNgoaiTe.SelectedItem = t.NgoaiTe.Trim();
                nudGiaNgoaiTe.Value = t.GiaNgoaiTe;
                dtpNgayKhoiHanh.Value = t.NgayKhoiHanh;
                nudSoChoToiDa.Value = t.SoChoToiDa;
            }
        }

        private void BtnThemTour_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTour.Text) || string.IsNullOrWhiteSpace(txtDiemDen.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ tên tour và điểm đến!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var context = new QLDatTourContext();
                var tour = new Tour
                {
                    TenTour = txtTenTour.Text.Trim(),
                    DiemDen = txtDiemDen.Text.Trim(),
                    NgoaiTe = cboNgoaiTe.SelectedItem?.ToString() ?? "USD",
                    GiaNgoaiTe = nudGiaNgoaiTe.Value,
                    NgayKhoiHanh = dtpNgayKhoiHanh.Value.Date,
                    SoChoToiDa = (int)nudSoChoToiDa.Value
                };

                context.Tours.Add(tour);
                context.SaveChanges();

                LoggerService.LogInfo($"Thêm tour thành công: MaTour={tour.MaTour}, Ten={tour.TenTour}");
                MessageBox.Show("Thêm Tour mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTour();
                ClearTourInputs();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi thêm tour.");
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaTour_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTour.Text))
            {
                MessageBox.Show("Vui lòng chọn tour cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTour = int.Parse(txtMaTour.Text);

            try
            {
                using var context = new QLDatTourContext();
                var tour = context.Tours.FirstOrDefault(t => t.MaTour == maTour);
                if (tour == null) return;

                tour.TenTour = txtTenTour.Text.Trim();
                tour.DiemDen = txtDiemDen.Text.Trim();
                tour.NgoaiTe = cboNgoaiTe.SelectedItem?.ToString() ?? "USD";
                tour.GiaNgoaiTe = nudGiaNgoaiTe.Value;
                tour.NgayKhoiHanh = dtpNgayKhoiHanh.Value.Date;
                tour.SoChoToiDa = (int)nudSoChoToiDa.Value;

                context.SaveChanges();

                LoggerService.LogInfo($"Cập nhật tour MaTour={maTour} thành công.");
                MessageBox.Show("Cập nhật tour thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTour();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi sửa tour MaTour={maTour}.");
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaTour_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTour.Text))
            {
                MessageBox.Show("Vui lòng chọn tour cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTour = int.Parse(txtMaTour.Text);
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tour [{txtTenTour.Text}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var context = new QLDatTourContext();
                var tour = context.Tours.FirstOrDefault(t => t.MaTour == maTour);
                if (tour != null)
                {
                    context.Tours.Remove(tour);
                    context.SaveChanges();

                    LoggerService.LogInfo($"Xóa tour MaTour={maTour} thành công.");
                    MessageBox.Show("Xóa tour thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTour();
                    ClearTourInputs();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi xóa tour MaTour={maTour}.");
                MessageBox.Show("Không thể xóa tour này vì đã có đơn đặt chỗ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearTourInputs()
        {
            txtMaTour.Clear();
            txtTenTour.Clear();
            txtDiemDen.Clear();
            cboNgoaiTe.SelectedIndex = 0;
            nudGiaNgoaiTe.Value = 0;
            nudSoChoToiDa.Value = 20;
            dtpNgayKhoiHanh.Value = DateTime.Today.AddDays(15);
            dgvTour.ClearSelection();
        }
        #endregion

        #region TAB KHÁCH HÀNG
        private void LoadKhachHang()
        {
            try
            {
                using var context = new QLDatTourContext();
                _allKhachHangs = context.KhachHangs.OrderBy(k => k.HoTen).ToList();

                cboDatKH.DataSource = null;
                cboDatKH.DisplayMember = "HoTen";
                cboDatKH.ValueMember = "MaKH";
                cboDatKH.DataSource = _allKhachHangs.ToList();

                ApplyFilterKH();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi nạp khách hàng.");
            }
        }

        private void ApplyFilterKH()
        {
            var query = _allKhachHangs.AsEnumerable();
            string keyword = txtTimKH.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         k.SoDienThoai.Contains(keyword));
            }
            dgvKhachHang.DataSource = query.ToList();
        }

        private void DgvKhachHang_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKhachHang.Rows.Count) return;

            if (dgvKhachHang.Rows[e.RowIndex].DataBoundItem is KhachHang kh)
            {
                txtMaKH.Text = kh.MaKH.ToString();
                txtHoTenKH.Text = kh.HoTen;
                txtSDTKH.Text = kh.SoDienThoai;
                txtEmailKH.Text = kh.Email;
            }
        }

        private void BtnThemKH_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTenKH.Text) || string.IsNullOrWhiteSpace(txtSDTKH.Text))
            {
                MessageBox.Show("Vui lòng điền họ tên và số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var context = new QLDatTourContext();
                var kh = new KhachHang
                {
                    HoTen = txtHoTenKH.Text.Trim(),
                    SoDienThoai = txtSDTKH.Text.Trim(),
                    Email = txtEmailKH.Text.Trim()
                };

                context.KhachHangs.Add(kh);
                context.SaveChanges();

                LoggerService.LogInfo($"Thêm khách hàng thành công: MaKH={kh.MaKH}, HoTen={kh.HoTen}");
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadKhachHang();
                ClearKHInputs();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi thêm khách hàng.");
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaKH_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKH = int.Parse(txtMaKH.Text);

            try
            {
                using var context = new QLDatTourContext();
                var kh = context.KhachHangs.FirstOrDefault(k => k.MaKH == maKH);
                if (kh == null) return;

                kh.HoTen = txtHoTenKH.Text.Trim();
                kh.SoDienThoai = txtSDTKH.Text.Trim();
                kh.Email = txtEmailKH.Text.Trim();

                context.SaveChanges();

                LoggerService.LogInfo($"Cập nhật khách hàng MaKH={maKH} thành công.");
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadKhachHang();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi sửa khách hàng MaKH={maKH}.");
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaKH_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKH = int.Parse(txtMaKH.Text);
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng [{txtHoTenKH.Text}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var context = new QLDatTourContext();
                var kh = context.KhachHangs.FirstOrDefault(k => k.MaKH == maKH);
                if (kh != null)
                {
                    context.KhachHangs.Remove(kh);
                    context.SaveChanges();

                    LoggerService.LogInfo($"Xóa khách hàng MaKH={maKH} thành công.");
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadKhachHang();
                    ClearKHInputs();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi xóa khách hàng MaKH={maKH}.");
                MessageBox.Show("Không thể xóa khách hàng này vì đã có đơn đặt tour!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearKHInputs()
        {
            txtMaKH.Clear();
            txtHoTenKH.Clear();
            txtSDTKH.Clear();
            txtEmailKH.Clear();
            dgvKhachHang.ClearSelection();
        }
        #endregion
    }
}
