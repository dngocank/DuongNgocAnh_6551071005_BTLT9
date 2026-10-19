using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau3_KeHoachDaNgoai.Data;
using Cau3_KeHoachDaNgoai.Models;
using Cau3_KeHoachDaNgoai.Services;

namespace Cau3_KeHoachDaNgoai
{
    public partial class FormChuyenDi : Form
    {
        private List<ChuyenDi> _allChuyenDi = new();
        private string _currentLang = "vi";
        private WeatherForecastResult? _lastWeatherResult;

        public FormChuyenDi()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            this.Load += FormChuyenDi_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (s, e) => ClearInputs();
            btnLoc.Click += (s, e) => ApplyFilter();
            btnBoLoc.Click += BtnBoLoc_Click;
            txtTimKiem.TextChanged += (s, e) => ApplyFilter();
            chkChuaHoanThanh.CheckedChanged += (s, e) => ApplyFilter();
            btnXemThoiTiet.Click += async (s, e) => await XemThoiTietAsync();
            dgvChuyenDi.CellClick += async (s, e) => await DgvChuyenDi_CellClickAsync(s, e);

            radVi.CheckedChanged += (s, e) => { if (radVi.Checked) SwitchLanguage("vi"); };
            radEn.CheckedChanged += (s, e) => { if (radEn.Checked) SwitchLanguage("en"); };
        }

        private void FormChuyenDi_Load(object? sender, EventArgs e)
        {
            // Tải cấu hình ngôn ngữ người dùng
            var settings = UserSettingsService.Load();
            _currentLang = settings.Language;
            if (_currentLang == "en") radEn.Checked = true;
            else radVi.Checked = true;

            cboPhuongTien.SelectedIndex = 0;
            DateTime now = DateTime.Today;
            dtpTuNgay.Value = now.AddDays(-7);
            dtpDenNgay.Value = now.AddDays(30);
            dtpNgayDi.Value = now.AddDays(2);

            ConfigureGridColumns();
            ApplyLanguageTexts();
            LoadDataFromDatabase();
        }

        private void ConfigureGridColumns()
        {
            dgvChuyenDi.AutoGenerateColumns = false;
            dgvChuyenDi.Columns.Clear();

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaChuyen",
                DataPropertyName = "MaChuyen",
                HeaderText = _currentLang == "vi" ? "Mã" : "ID",
                Width = 55
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenChuyen",
                DataPropertyName = "TenChuyen",
                HeaderText = _currentLang == "vi" ? "Tên chuyến đi" : "Trip Name",
                Width = 190
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDiaDiem",
                DataPropertyName = "DiaDiem",
                HeaderText = _currentLang == "vi" ? "Điểm đến" : "Destination",
                Width = 110
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayDi",
                DataPropertyName = "NgayDi",
                HeaderText = _currentLang == "vi" ? "Ngày đi" : "Date",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                Width = 100
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoNguoi",
                DataPropertyName = "SoNguoi",
                HeaderText = _currentLang == "vi" ? "Số người" : "People",
                Width = 75
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNganSach",
                DataPropertyName = "NganSach",
                HeaderText = _currentLang == "vi" ? "Ngân sách (đ)" : "Budget (VND)",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 125
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhuongTien",
                DataPropertyName = "PhuongTien",
                HeaderText = _currentLang == "vi" ? "Phương tiện" : "Transport",
                Width = 105
            });

            dgvChuyenDi.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colDaHoanThanh",
                DataPropertyName = "DaHoanThanh",
                HeaderText = _currentLang == "vi" ? "Hoàn thành" : "Completed",
                Width = 85
            });

            dgvChuyenDi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGhiChu",
                DataPropertyName = "GhiChu",
                HeaderText = _currentLang == "vi" ? "Ghi chú" : "Notes",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void SwitchLanguage(string lang)
        {
            if (_currentLang == lang) return;
            _currentLang = lang;

            // Lưu cài đặt
            UserSettingsService.Save(new UserSettings { Language = lang });

            ApplyLanguageTexts();
            ConfigureGridColumns();
            ApplyFilter();

            // Cập nhật lại mô tả thời tiết nếu đã có kết quả
            if (_lastWeatherResult != null && _lastWeatherResult.Success)
            {
                lblThoiTietMoTa.Text = _currentLang == "vi" ? _lastWeatherResult.WeatherDescriptionVi : _lastWeatherResult.WeatherDescriptionEn;
                lblNhietDo.Text = (_currentLang == "vi" ? "Nhiệt độ: " : "Temperature: ") + $"{_lastWeatherResult.TempMin:N0} - {_lastWeatherResult.TempMax:N0} °C";
                lblXacSuatMua.Text = (_currentLang == "vi" ? "Xác suất mưa: " : "Precipitation: ") + $"{_lastWeatherResult.RainProbability} %";
                if (_lastWeatherResult.IsRainWarning)
                {
                    lblCanhBao.Text = _currentLang == "vi"
                        ? "⚠️ NÊN MANG ÁO MƯA HOẶC CÂN NHẮC DỜI LỊCH!"
                        : "⚠️ HIGH CHANCE OF RAIN! BRING A RAINCOAT OR RESCHEDULE!";
                }
            }
        }

        private void ApplyLanguageTexts()
        {
            bool isVi = _currentLang == "vi";

            lblTitle.Text = isVi ? "KẾ HOẠCH DÃ NGOẠI & DỰ BÁO THỜI TIẾT" : "OUTING PLAN & WEATHER FORECAST";
            grpThongTin.Text = isVi ? "Thông tin chuyến đi" : "Trip Information";
            lblMaChuyen.Text = isVi ? "Mã chuyến:" : "Trip ID:";
            lblTenChuyen.Text = isVi ? "Tên chuyến:" : "Trip Name:";
            lblDiaDiem.Text = isVi ? "Địa điểm:" : "Destination:";
            lblNgayDi.Text = isVi ? "Ngày đi:" : "Date:";
            lblSoNguoi.Text = isVi ? "Số người:" : "People:";
            lblNganSach.Text = isVi ? "Ngân sách:" : "Budget:";
            lblPhuongTien.Text = isVi ? "Phương tiện:" : "Transport:";
            chkHoanThanh.Text = isVi ? "Đã hoàn thành" : "Completed";
            lblGhiChu.Text = isVi ? "Ghi chú:" : "Notes:";

            btnThem.Text = isVi ? "Thêm" : "Add";
            btnSua.Text = isVi ? "Sửa" : "Edit";
            btnXoa.Text = isVi ? "Xóa" : "Delete";
            btnLamMoi.Text = isVi ? "Làm mới" : "Clear";

            grpTimKiem.Text = isVi ? "Tìm kiếm & Lọc chuyến đi" : "Search & Filter Trips";
            lblTimKiem.Text = isVi ? "Tên/Địa điểm:" : "Name/Dest:";
            txtTimKiem.PlaceholderText = isVi ? "Tìm tên hoặc địa điểm..." : "Search name or destination...";
            chkChuaHoanThanh.Text = isVi ? "Chỉ hiện chuyến chưa hoàn thành" : "Only uncompleted trips";
            lblTuNgay.Text = isVi ? "Từ ngày:" : "From:";
            lblDenNgay.Text = isVi ? "Đến ngày:" : "To:";
            btnLoc.Text = isVi ? "Lọc" : "Filter";
            btnBoLoc.Text = isVi ? "Bỏ lọc" : "Reset";

            btnXemThoiTiet.Text = isVi ? "Xem thời tiết điểm đến" : "View Weather Forecast";
            this.Text = isVi ? "Kế hoạch dã ngoại & Dự báo thời tiết" : "Outing Plan & Weather Forecast";
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using var context = new QLDaNgoaiContext();
                _allChuyenDi = context.ChuyenDis.OrderBy(c => c.NgayDi).ToList();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi nạp dữ liệu chuyến đi.");
                string msg = _currentLang == "vi" ? "Không thể kết nối CSDL: " : "Cannot connect to database: ";
                MessageBox.Show(msg + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            var query = _allChuyenDi.AsEnumerable();

            string keyword = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.TenChuyen.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         c.DiaDiem.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (chkChuaHoanThanh.Checked)
            {
                query = query.Where(c => !c.DaHoanThanh);
            }

            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;
            if (tuNgay <= denNgay)
            {
                query = query.Where(c => c.NgayDi >= tuNgay && c.NgayDi <= denNgay);
            }

            dgvChuyenDi.DataSource = query.ToList();
        }

        private async Task DgvChuyenDi_CellClickAsync(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChuyenDi.Rows.Count) return;

            if (dgvChuyenDi.Rows[e.RowIndex].DataBoundItem is ChuyenDi cd)
            {
                txtMaChuyen.Text = cd.MaChuyen.ToString();
                txtTenChuyen.Text = cd.TenChuyen;
                txtDiaDiem.Text = cd.DiaDiem;
                dtpNgayDi.Value = cd.NgayDi;
                nudSoNguoi.Value = cd.SoNguoi > 0 ? cd.SoNguoi : 1;
                nudNganSach.Value = cd.NganSach;
                cboPhuongTien.SelectedItem = cd.PhuongTien;
                chkHoanThanh.Checked = cd.DaHoanThanh;
                txtGhiChu.Text = cd.GhiChu ?? string.Empty;

                // Tự động kích hoạt xem thời tiết khi chọn một dòng trên lưới theo yêu cầu
                await XemThoiTietAsync();
            }
        }

        private async Task XemThoiTietAsync()
        {
            string diaDiem = txtDiaDiem.Text.Trim();
            if (string.IsNullOrEmpty(diaDiem))
            {
                string warn = _currentLang == "vi" ? "Vui lòng nhập hoặc chọn địa điểm đến!" : "Please enter or select a destination!";
                MessageBox.Show(warn, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime ngayDi = dtpNgayDi.Value.Date;

            try
            {
                btnXemThoiTiet.Enabled = false;
                prgLoading.Visible = true;
                lblThoiTietMoTa.Text = _currentLang == "vi" ? "Đang tải dự báo..." : "Loading forecast...";
                pnlThoiTiet.BackColor = Color.FromArgb(245, 247, 250);
                lblCanhBao.Text = string.Empty;

                var result = await WeatherApiService.GetForecastAsync(diaDiem, ngayDi);
                _lastWeatherResult = result;

                if (result.Success)
                {
                    lblThoiTietIcon.Text = result.WeatherIcon;
                    lblThoiTietMoTa.Text = _currentLang == "vi" ? result.WeatherDescriptionVi : result.WeatherDescriptionEn;
                    lblNhietDo.Text = (_currentLang == "vi" ? "Nhiệt độ: " : "Temperature: ") + $"{result.TempMin:N0} - {result.TempMax:N0} °C";
                    lblXacSuatMua.Text = (_currentLang == "vi" ? "Xác suất mưa: " : "Precipitation: ") + $"{result.RainProbability} %";

                    if (result.IsRainWarning)
                    {
                        // Đổi màu nền sang cam cảnh báo
                        pnlThoiTiet.BackColor = Color.FromArgb(255, 230, 200);
                        lblCanhBao.ForeColor = Color.FromArgb(211, 84, 0);
                        lblCanhBao.Text = _currentLang == "vi"
                            ? "⚠️ NÊN MANG ÁO MƯA HOẶC CÂN NHẮC DỜI LỊCH!"
                            : "⚠️ HIGH CHANCE OF RAIN! BRING A RAINCOAT OR RESCHEDULE!";
                    }
                    else
                    {
                        pnlThoiTiet.BackColor = Color.FromArgb(235, 247, 238);
                        lblCanhBao.ForeColor = Color.FromArgb(39, 174, 96);
                        lblCanhBao.Text = _currentLang == "vi" ? "Thời tiết thuận lợi cho chuyến đi! ✨" : "Good weather for traveling! ✨";
                    }

                    LoggerService.LogInfo($"Lấy thời tiết thành công cho {diaDiem} ({ngayDi:yyyy-MM-dd}): {result.WeatherDescriptionVi}, Mưa {result.RainProbability}%");
                }
                else
                {
                    lblThoiTietIcon.Text = "❓";
                    lblThoiTietMoTa.Text = result.Exceeds16Days
                        ? (_currentLang == "vi" ? "Chưa có dữ liệu dự báo" : "No forecast data yet (>16 days)")
                        : (_currentLang == "vi" ? "Không tìm thấy dữ liệu" : "Data not found");

                    lblNhietDo.Text = "Nhiệt độ: -- / -- °C";
                    lblXacSuatMua.Text = "Xác suất mưa: -- %";
                    pnlThoiTiet.BackColor = Color.FromArgb(254, 237, 236);
                    lblCanhBao.ForeColor = Color.FromArgb(192, 57, 43);
                    lblCanhBao.Text = result.Message;
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogWarning($"Lỗi kết nối thời tiết: {ex.Message}");
                lblThoiTietMoTa.Text = _currentLang == "vi" ? "Lỗi kết nối mạng" : "Network error";
                pnlThoiTiet.BackColor = Color.FromArgb(254, 237, 236);
                lblCanhBao.Text = _currentLang == "vi"
                    ? "Không thể kết nối đến Open-Meteo. Vui lòng kiểm tra Internet!"
                    : "Cannot reach Open-Meteo. Please check your internet connection!";
            }
            finally
            {
                prgLoading.Visible = false;
                btnXemThoiTiet.Enabled = true;
            }
        }

        private bool ValidateInput(bool isNew = false)
        {
            if (string.IsNullOrWhiteSpace(txtTenChuyen.Text))
            {
                MessageBox.Show(_currentLang == "vi" ? "Vui lòng nhập tên chuyến đi!" : "Please enter trip name!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChuyen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaDiem.Text))
            {
                MessageBox.Show(_currentLang == "vi" ? "Vui lòng nhập địa điểm đến!" : "Please enter destination!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaDiem.Focus();
                return false;
            }

            if (isNew && dtpNgayDi.Value.Date < DateTime.Today)
            {
                MessageBox.Show(_currentLang == "vi" ? "Ngày đi không được nhỏ hơn ngày hôm nay!" : "Departure date cannot be earlier than today!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayDi.Focus();
                return false;
            }

            if (nudSoNguoi.Value < 1)
            {
                MessageBox.Show(_currentLang == "vi" ? "Số người phải từ 1 trở lên!" : "Number of people must be at least 1!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoNguoi.Focus();
                return false;
            }

            if (nudNganSach.Value < 0)
            {
                MessageBox.Show(_currentLang == "vi" ? "Ngân sách không được nhỏ hơn 0!" : "Budget cannot be negative!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudNganSach.Focus();
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(isNew: true)) return;

            try
            {
                using var context = new QLDaNgoaiContext();
                var cd = new ChuyenDi
                {
                    TenChuyen = txtTenChuyen.Text.Trim(),
                    DiaDiem = txtDiaDiem.Text.Trim(),
                    NgayDi = dtpNgayDi.Value.Date,
                    SoNguoi = (int)nudSoNguoi.Value,
                    NganSach = nudNganSach.Value,
                    PhuongTien = cboPhuongTien.SelectedItem?.ToString() ?? "Xe máy",
                    DaHoanThanh = chkHoanThanh.Checked,
                    GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
                };

                context.ChuyenDis.Add(cd);
                context.SaveChanges();

                LoggerService.LogInfo($"Thêm chuyến đi thành công: MaChuyen={cd.MaChuyen}, Ten={cd.TenChuyen}");
                string success = _currentLang == "vi" ? "Thêm chuyến đi mới thành công!" : "Added new trip successfully!";
                MessageBox.Show(success, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataFromDatabase();
                ClearInputs();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi thêm chuyến đi.");
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaChuyen.Text))
            {
                string warn = _currentLang == "vi" ? "Vui lòng chọn chuyến đi cần sửa từ danh sách!" : "Please select a trip to edit!";
                MessageBox.Show(warn, "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput(isNew: false)) return;

            int maChuyen = int.Parse(txtMaChuyen.Text);

            try
            {
                using var context = new QLDaNgoaiContext();
                var cd = context.ChuyenDis.FirstOrDefault(c => c.MaChuyen == maChuyen);
                if (cd == null)
                {
                    MessageBox.Show("Not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cd.TenChuyen = txtTenChuyen.Text.Trim();
                cd.DiaDiem = txtDiaDiem.Text.Trim();
                cd.NgayDi = dtpNgayDi.Value.Date;
                cd.SoNguoi = (int)nudSoNguoi.Value;
                cd.NganSach = nudNganSach.Value;
                cd.PhuongTien = cboPhuongTien.SelectedItem?.ToString() ?? "Xe máy";
                cd.DaHoanThanh = chkHoanThanh.Checked;
                cd.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

                context.SaveChanges();

                LoggerService.LogInfo($"Cập nhật chuyến đi MaChuyen={maChuyen} thành công.");
                string success = _currentLang == "vi" ? "Cập nhật chuyến đi thành công!" : "Updated trip successfully!";
                MessageBox.Show(success, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi sửa chuyến đi MaChuyen={maChuyen}.");
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaChuyen.Text))
            {
                string warn = _currentLang == "vi" ? "Vui lòng chọn chuyến đi cần xóa!" : "Please select a trip to delete!";
                MessageBox.Show(warn, "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maChuyen = int.Parse(txtMaChuyen.Text);
            string q = _currentLang == "vi"
                ? $"Bạn có chắc chắn muốn xóa chuyến đi [{txtTenChuyen.Text}] không?"
                : $"Are you sure you want to delete trip [{txtTenChuyen.Text}]?";

            if (MessageBox.Show(q, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var context = new QLDaNgoaiContext();
                var cd = context.ChuyenDis.FirstOrDefault(c => c.MaChuyen == maChuyen);
                if (cd != null)
                {
                    context.ChuyenDis.Remove(cd);
                    context.SaveChanges();

                    LoggerService.LogInfo($"Xóa chuyến đi MaChuyen={maChuyen} thành công.");
                    string success = _currentLang == "vi" ? "Xóa chuyến đi thành công!" : "Deleted trip successfully!";
                    MessageBox.Show(success, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataFromDatabase();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi xóa chuyến đi MaChuyen={maChuyen}.");
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBoLoc_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            chkChuaHoanThanh.Checked = false;
            DateTime now = DateTime.Today;
            dtpTuNgay.Value = now.AddDays(-7);
            dtpDenNgay.Value = now.AddDays(30);
            ApplyFilter();
        }

        private void ClearInputs()
        {
            txtMaChuyen.Clear();
            txtTenChuyen.Clear();
            txtDiaDiem.Clear();
            dtpNgayDi.Value = DateTime.Today.AddDays(1);
            nudSoNguoi.Value = 1;
            nudNganSach.Value = 0;
            cboPhuongTien.SelectedIndex = 0;
            chkHoanThanh.Checked = false;
            txtGhiChu.Clear();
            dgvChuyenDi.ClearSelection();
        }
    }
}
