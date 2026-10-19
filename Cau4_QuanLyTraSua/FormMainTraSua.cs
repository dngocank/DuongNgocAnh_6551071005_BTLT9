using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau4_QuanLyTraSua.Config;
using Cau4_QuanLyTraSua.Data;
using Cau4_QuanLyTraSua.Models;
using Cau4_QuanLyTraSua.Services;

namespace Cau4_QuanLyTraSua
{
    public class GioHangItem
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }

    public class MonUongDisplayDTO
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public string TenDM { get; set; } = string.Empty;
        public decimal GiaBan { get; set; }
        public bool DangBan { get; set; }
        public int MaDM { get; set; }
    }

    public partial class FormMainTraSua : Form
    {
        private List<GioHangItem> _gioHang = new();
        private List<DanhMucMon> _danhMucList = new();
        private List<MonUong> _allMonUong = new();
        private List<MonUongDisplayDTO> _thucDonDisplay = new();

        public FormMainTraSua()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            this.Load += FormMainTraSua_Load;

            // Bán hàng
            cboDMBanHang.SelectedIndexChanged += CboDMBanHang_SelectedIndexChanged;
            btnThemVaoDon.Click += BtnThemVaoDon_Click;
            btnXoaKhoiGio.Click += BtnXoaKhoiGio_Click;
            btnThanhToanQR.Click += async (s, e) => await TaoMaVietQRAsync();
            btnDaNhanTien.Click += (s, e) => LuuHoaDon("Chuyển khoản");
            btnTienMat.Click += (s, e) => LuuHoaDon("Tiền mặt");

            // Thực đơn
            btnThemMon.Click += BtnThemMon_Click;
            btnSuaMon.Click += BtnSuaMon_Click;
            btnXoaMon.Click += BtnXoaMon_Click;
            btnLamMoiMon.Click += (s, e) => ClearInputsMon();
            txtTimTenMon.TextChanged += (s, e) => ApplyFilterThucDon();
            cboLocDMMon.SelectedIndexChanged += (s, e) => ApplyFilterThucDon();
            btnBoLocMon.Click += BtnBoLocMon_Click;
            dgvThucDon.CellClick += DgvThucDon_CellClick;

            // Hóa đơn
            btnLocHD.Click += (s, e) => LoadHoaDonTheoNgay();
            dgvHoaDon.CellClick += DgvHoaDon_CellClick;
        }

        private void FormMainTraSua_Load(object? sender, EventArgs e)
        {
            ConfigureGridColumns();
            LoadDanhMuc();
            LoadThucDon();
            LoadHoaDonTheoNgay();
        }

        private void ConfigureGridColumns()
        {
            // Grid Giỏ hàng
            dgvGioHang.AutoGenerateColumns = false;
            dgvGioHang.Columns.Clear();
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenMon", HeaderText = "Tên món", Width = 180 });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "SL", Width = 50 });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 90 });
            dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành tiền", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 100 });

            // Grid Thực đơn
            dgvThucDon.AutoGenerateColumns = false;
            dgvThucDon.Columns.Clear();
            dgvThucDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaMon", HeaderText = "Mã", Width = 60 });
            dgvThucDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenMon", HeaderText = "Tên món đồ uống", Width = 280 });
            dgvThucDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDM", HeaderText = "Danh mục", Width = 200 });
            dgvThucDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GiaBan", HeaderText = "Giá bán (đ)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 130 });
            dgvThucDon.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DangBan", HeaderText = "Đang kinh doanh", Width = 140 });

            // Grid Hóa đơn
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.Columns.Clear();
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaHD", HeaderText = "Mã HD", Width = 70 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgayLap", HeaderText = "Giờ lập", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, Width = 140 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HinhThucTT", HeaderText = "Hình thức", Width = 110 });
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TongTien", HeaderText = "Tổng tiền (đ)", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 120 });
            dgvHoaDon.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DaThanhToan", HeaderText = "Đã thanh toán", Width = 100 });

            // Grid Chi tiết hóa đơn
            dgvChiTietHoaDon.AutoGenerateColumns = false;
            dgvChiTietHoaDon.Columns.Clear();
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenMon", HeaderText = "Tên món", Width = 200 });
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLy", HeaderText = "Số ly", Width = 60 });
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 110 });
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành tiền", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }, Width = 120 });
        }

        #region BÁN HÀNG & VIETQR
        private void LoadDanhMuc()
        {
            try
            {
                using var context = new QLTraSuaContext();
                _danhMucList = context.DanhMucMons.OrderBy(d => d.MaDM).ToList();

                cboDMBanHang.DataSource = null;
                cboDMBanHang.DisplayMember = "TenDM";
                cboDMBanHang.ValueMember = "MaDM";
                cboDMBanHang.DataSource = _danhMucList.ToList();

                cboDanhMucMon.DataSource = null;
                cboDanhMucMon.DisplayMember = "TenDM";
                cboDanhMucMon.ValueMember = "MaDM";
                cboDanhMucMon.DataSource = _danhMucList.ToList();

                var locDM = new List<string> { "Tất cả danh mục" };
                locDM.AddRange(_danhMucList.Select(d => d.TenDM));
                cboLocDMMon.DataSource = locDM;
                cboLocDMMon.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi nạp danh mục.");
            }
        }

        private void CboDMBanHang_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboDMBanHang.SelectedValue is int maDM)
            {
                try
                {
                    using var context = new QLTraSuaContext();
                    var monTheoDM = context.MonUongs
                        .Where(m => m.MaDM == maDM && m.DangBan)
                        .OrderBy(m => m.TenMon)
                        .ToList();

                    lstMonUong.DataSource = null;
                    lstMonUong.DisplayMember = "TenMon";
                    lstMonUong.ValueMember = "MaMon";
                    lstMonUong.DataSource = monTheoDM;
                }
                catch (Exception ex)
                {
                    LoggerService.LogError(ex, "Lỗi khi nạp món theo danh mục.");
                }
            }
        }

        private async void BtnThemVaoDon_Click(object? sender, EventArgs e)
        {
            if (lstMonUong.SelectedItem is not MonUong mon)
            {
                MessageBox.Show("Vui lòng chọn một món uống từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLy = (int)nudSoLy.Value;
            var item = _gioHang.FirstOrDefault(g => g.MaMon == mon.MaMon);
            if (item != null)
            {
                // Tự động cộng dồn số lượng nếu đã có món
                item.SoLuong += soLy;
            }
            else
            {
                _gioHang.Add(new GioHangItem
                {
                    MaMon = mon.MaMon,
                    TenMon = mon.TenMon,
                    SoLuong = soLy,
                    DonGia = mon.GiaBan
                });
            }

            UpdateGioHangView();
            nudSoLy.Value = 1;

            // Tự động tạo và hiển thị mã VietQR ngay khi thêm món vào đơn
            await TaoMaVietQRAsync(showEmptyWarning: false);
        }

        private async void BtnXoaKhoiGio_Click(object? sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow?.DataBoundItem is GioHangItem item)
            {
                _gioHang.Remove(item);
                UpdateGioHangView();
                // Tự động cập nhật lại mã VietQR theo tổng tiền mới
                await TaoMaVietQRAsync(showEmptyWarning: false);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng món trong đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateGioHangView()
        {
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = _gioHang.ToList();
            decimal tong = _gioHang.Sum(g => g.ThanhTien);
            lblTongTienDon.Text = $"Tổng tiền: {tong:N0} VNĐ";
        }

        private async Task TaoMaVietQRAsync(bool showEmptyWarning = true)
        {
            if (!_gioHang.Any())
            {
                picVietQR.Image = null;
                lblThongTinQR.Text = "Chưa có món trong đơn hàng.";
                if (showEmptyWarning)
                {
                    MessageBox.Show("Đơn hàng hiện đang trống! Hãy thêm món trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            decimal tongTien = _gioHang.Sum(g => g.ThanhTien);
            int nextId = GetNextHoaDonId();
            string noiDung = $"TRASUA HD{nextId}";

            var payment = AppConfig.Instance.Payment;
            lblThongTinQR.Text = $"Đang tạo mã VietQR ({tongTien:N0} đ)...";

            btnThanhToanQR.Enabled = false;

            string qrUrl = VietQrService.BuildVietQrUrl(tongTien, noiDung);
            var image = await VietQrService.FetchQrImageAsync(qrUrl, noiDung, tongTien);

            btnThanhToanQR.Enabled = true;

            if (image != null)
            {
                picVietQR.Image = image;
                lblThongTinQR.Text = $"✔️ Quét mã để thanh toán:\n• Ngân hàng: {payment.BankId}\n• Số TK: {payment.AccountNo}\n• Chủ TK: {payment.AccountName}\n• Số tiền: {tongTien:N0} đ\n• Cú pháp CK: {noiDung}";
            }
            else
            {
                // Khi mất mạng: vẫn hiển thị số tài khoản và cú pháp chuyển khoản bằng chữ để khách nhập tay
                picVietQR.Image = null;
                lblThongTinQR.Text = $"⚠️ Không tải được ảnh QR (mất mạng).\nKhách vui lòng chuyển khoản thủ công:\n• Ngân hàng: {payment.BankId}\n• Số TK: {payment.AccountNo}\n• Chủ TK: {payment.AccountName}\n• Số tiền: {tongTien:N0} đ\n• Nội dung: {noiDung}\n(Hoặc chọn nút 'Tiền mặt' để đổi hình thức)";
                MessageBox.Show("Không kết nối được dịch vụ tạo ảnh VietQR. Bạn có thể cho khách nhập tay số tài khoản hoặc đổi sang thanh toán Tiền mặt!",
                    "Chế độ ngoại tuyến", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int GetNextHoaDonId()
        {
            try
            {
                using var context = new QLTraSuaContext();
                int maxId = context.HoaDons.Max(h => (int?)h.MaHD) ?? 0;
                return maxId + 1;
            }
            catch
            {
                return 1;
            }
        }

        private void LuuHoaDon(string hinhThuc)
        {
            if (!_gioHang.Any())
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tong = _gioHang.Sum(g => g.ThanhTien);

            try
            {
                using var context = new QLTraSuaContext();
                var hd = new HoaDon
                {
                    NgayLap = DateTime.Now,
                    TongTien = tong,
                    HinhThucTT = hinhThuc,
                    DaThanhToan = true
                };

                context.HoaDons.Add(hd);

                // Thêm các dòng ChiTietHoaDon
                foreach (var item in _gioHang)
                {
                    hd.ChiTietHoaDons.Add(new ChiTietHoaDon
                    {
                        MaMon = item.MaMon,
                        SoLy = item.SoLuong,
                        DonGia = item.DonGia
                    });
                }

                // Lưu trong cùng 1 SaveChanges()
                context.SaveChanges();

                LoggerService.LogInfo($"Lập hóa đơn MaHD={hd.MaHD} thành công: {hinhThuc}, Tổng tiền={tong:N0} đ");
                MessageBox.Show($"Thanh toán và lưu hóa đơn #{hd.MaHD} thành công! ({hinhThuc})",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _gioHang.Clear();
                UpdateGioHangView();
                picVietQR.Image = null;
                lblThongTinQR.Text = "Hóa đơn đã được thanh toán thành công!";

                LoadHoaDonTheoNgay();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi lưu hóa đơn.");
                MessageBox.Show("Đã xảy ra lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region QUẢN LÝ THỰC ĐƠN
        private void LoadThucDon()
        {
            try
            {
                using var context = new QLTraSuaContext();
                _allMonUong = context.MonUongs.Include(m => m.DanhMucMon).ToList();

                _thucDonDisplay = _allMonUong.Select(m => new MonUongDisplayDTO
                {
                    MaMon = m.MaMon,
                    TenMon = m.TenMon,
                    TenDM = m.DanhMucMon?.TenDM ?? string.Empty,
                    GiaBan = m.GiaBan,
                    DangBan = m.DangBan,
                    MaDM = m.MaDM
                }).ToList();

                ApplyFilterThucDon();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi nạp thực đơn.");
            }
        }

        private void ApplyFilterThucDon()
        {
            var query = _thucDonDisplay.AsEnumerable();

            string keyword = txtTimTenMon.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(m => m.TenMon.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            string dm = cboLocDMMon.SelectedItem?.ToString() ?? "Tất cả danh mục";
            if (dm != "Tất cả danh mục")
            {
                query = query.Where(m => m.TenDM == dm);
            }

            dgvThucDon.DataSource = query.ToList();
        }

        private void DgvThucDon_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvThucDon.Rows.Count) return;

            if (dgvThucDon.Rows[e.RowIndex].DataBoundItem is MonUongDisplayDTO dto)
            {
                txtMaMon.Text = dto.MaMon.ToString();
                txtTenMon.Text = dto.TenMon;
                cboDanhMucMon.SelectedValue = dto.MaDM;
                nudGiaBan.Value = dto.GiaBan;
                chkDangBan.Checked = dto.DangBan;
            }
        }

        private void BtnThemMon_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món uống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            if (cboDanhMucMon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var context = new QLTraSuaContext();
                var mon = new MonUong
                {
                    TenMon = txtTenMon.Text.Trim(),
                    MaDM = (int)cboDanhMucMon.SelectedValue,
                    GiaBan = nudGiaBan.Value,
                    DangBan = chkDangBan.Checked
                };

                context.MonUongs.Add(mon);
                context.SaveChanges();

                LoggerService.LogInfo($"Thêm món mới thành công: MaMon={mon.MaMon}, Ten={mon.TenMon}");
                MessageBox.Show("Thêm món uống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadThucDon();
                CboDMBanHang_SelectedIndexChanged(null, EventArgs.Empty);
                ClearInputsMon();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi thêm món.");
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaMon_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn món cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMon = int.Parse(txtMaMon.Text);

            try
            {
                using var context = new QLTraSuaContext();
                var mon = context.MonUongs.FirstOrDefault(m => m.MaMon == maMon);
                if (mon == null) return;

                mon.TenMon = txtTenMon.Text.Trim();
                mon.MaDM = (int)cboDanhMucMon.SelectedValue!;
                mon.GiaBan = nudGiaBan.Value;
                mon.DangBan = chkDangBan.Checked;

                context.SaveChanges();

                LoggerService.LogInfo($"Cập nhật món MaMon={maMon} thành công.");
                MessageBox.Show("Cập nhật món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadThucDon();
                CboDMBanHang_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi sửa món MaMon={maMon}.");
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaMon_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMon = int.Parse(txtMaMon.Text);
            if (MessageBox.Show($"Bạn có chắc muốn xóa món [{txtTenMon.Text}] không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var context = new QLTraSuaContext();
                var mon = context.MonUongs.FirstOrDefault(m => m.MaMon == maMon);
                if (mon != null)
                {
                    context.MonUongs.Remove(mon);
                    context.SaveChanges();

                    LoggerService.LogInfo($"Xóa món MaMon={maMon} thành công.");
                    MessageBox.Show("Đã xóa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadThucDon();
                    CboDMBanHang_SelectedIndexChanged(null, EventArgs.Empty);
                    ClearInputsMon();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi xóa món MaMon={maMon}.");
                MessageBox.Show("Không thể xóa món này vì đã có trong hóa đơn cũ. Bạn có thể đổi trạng thái thành ngừng bán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBoLocMon_Click(object? sender, EventArgs e)
        {
            txtTimTenMon.Clear();
            cboLocDMMon.SelectedIndex = 0;
            ApplyFilterThucDon();
        }

        private void ClearInputsMon()
        {
            txtMaMon.Clear();
            txtTenMon.Clear();
            nudGiaBan.Value = 0;
            chkDangBan.Checked = true;
            dgvThucDon.ClearSelection();
        }
        #endregion

        #region DANH SÁCH & CHI TIẾT HÓA ĐƠN
        private void LoadHoaDonTheoNgay()
        {
            DateTime ngay = dtpNgayHoaDon.Value.Date;
            try
            {
                using var context = new QLTraSuaContext();
                var list = context.HoaDons
                    .Where(h => h.NgayLap.Date == ngay)
                    .OrderByDescending(h => h.NgayLap)
                    .ToList();

                dgvHoaDon.DataSource = list;

                decimal doanhThu = list.Sum(h => h.TongTien);
                lblDoanhThuNgay.Text = $"Doanh thu ngày {ngay:dd/MM/yyyy}: {doanhThu:N0} VNĐ";

                dgvChiTietHoaDon.DataSource = null;
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi nạp danh sách hóa đơn theo ngày.");
            }
        }

        private void DgvHoaDon_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHoaDon.Rows.Count) return;

            if (dgvHoaDon.Rows[e.RowIndex].DataBoundItem is HoaDon hd)
            {
                try
                {
                    using var context = new QLTraSuaContext();
                    var details = context.ChiTietHoaDons
                        .Where(c => c.MaHD == hd.MaHD)
                        .Include(c => c.MonUong)
                        .Select(c => new
                        {
                            TenMon = c.MonUong != null ? c.MonUong.TenMon : "Món #" + c.MaMon,
                            SoLy = c.SoLy,
                            DonGia = c.DonGia,
                            ThanhTien = c.SoLy * c.DonGia
                        })
                        .ToList();

                    dgvChiTietHoaDon.DataSource = details;
                }
                catch (Exception ex)
                {
                    LoggerService.LogError(ex, $"Lỗi khi nạp chi tiết hóa đơn MaHD={hd.MaHD}.");
                }
            }
        }
        #endregion
    }
}
