using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau1_SoChiTieu.Data;
using Cau1_SoChiTieu.Models;
using Cau1_SoChiTieu.Services;

namespace Cau1_SoChiTieu
{
    public partial class FormChiTieu : Form
    {
        private List<ChiTieu> _allChiTieu = new();

        public FormChiTieu()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            this.Load += FormChiTieu_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnLoc.Click += (s, e) => ApplyFilter();
            btnBoLoc.Click += BtnBoLoc_Click;
            txtTimNoiDung.TextChanged += (s, e) => ApplyFilter();
            dgvChiTieu.CellClick += DgvChiTieu_CellClick;
        }

        private void FormChiTieu_Load(object? sender, EventArgs e)
        {
            cboLoaiChi.SelectedIndex = 0;
            cboLocLoaiChi.SelectedIndex = 0;

            DateTime now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;
            dtpNgayChi.Value = now;

            ConfigureGridColumns();
            LoadDataFromDatabase();
        }

        private void ConfigureGridColumns()
        {
            dgvChiTieu.AutoGenerateColumns = false;
            dgvChiTieu.Columns.Clear();

            dgvChiTieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaCT",
                HeaderText = "Mã CT",
                Width = 70
            });

            dgvChiTieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayChi",
                HeaderText = "Ngày chi",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                Width = 110
            });

            dgvChiTieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NoiDung",
                HeaderText = "Nội dung chi tiêu",
                Width = 230
            });

            dgvChiTieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoTien",
                HeaderText = "Số tiền (VNĐ)",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 130
            });

            dgvChiTieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LoaiChi",
                HeaderText = "Loại chi",
                Width = 120
            });

            dgvChiTieu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GhiChu",
                HeaderText = "Ghi chú",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                using var context = new QLChiTieuContext();
                _allChiTieu = context.ChiTieus
                    .OrderByDescending(c => c.NgayChi)
                    .ThenByDescending(c => c.MaCT)
                    .ToList();

                ApplyFilter();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi nạp dữ liệu chi tiêu từ CSDL.");
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra lại cấu hình kết nối trong file appsettings.json!",
                    "Lỗi kết nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            var query = _allChiTieu.AsEnumerable();

            string keyword = txtTimNoiDung.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.NoiDung.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            string loaiChi = cboLocLoaiChi.SelectedItem?.ToString() ?? "Tất cả";
            if (loaiChi != "Tất cả")
            {
                query = query.Where(c => c.LoaiChi == loaiChi);
            }

            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;
            if (tuNgay <= denNgay)
            {
                query = query.Where(c => c.NgayChi >= tuNgay && c.NgayChi <= denNgay);
            }

            var displayList = query.ToList();
            dgvChiTieu.DataSource = displayList;
            UpdateTongChi(displayList);
        }

        private void UpdateTongChi(List<ChiTieu> list)
        {
            decimal tong = list.Sum(c => c.SoTien);
            lblTongChi.Text = $"Tổng chi: {tong:N0} đ";
        }

        private void DgvChiTieu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTieu.Rows.Count) return;

            var row = dgvChiTieu.Rows[e.RowIndex];
            if (row.DataBoundItem is ChiTieu ct)
            {
                txtMaCT.Text = ct.MaCT.ToString();
                dtpNgayChi.Value = ct.NgayChi;
                txtNoiDung.Text = ct.NoiDung;
                cboLoaiChi.SelectedItem = ct.LoaiChi;
                nudSoTien.Value = ct.SoTien;
                txtGhiChu.Text = ct.GhiChu ?? string.Empty;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung chi tiêu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return false;
            }

            if (nudSoTien.Value <= 0)
            {
                MessageBox.Show("Số tiền chi tiêu phải lớn hơn 0 đ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoTien.Focus();
                return false;
            }

            if (dtpNgayChi.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Ngày chi không được lớn hơn ngày hôm nay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayChi.Focus();
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using var context = new QLChiTieuContext();
                var item = new ChiTieu
                {
                    NgayChi = dtpNgayChi.Value.Date,
                    NoiDung = txtNoiDung.Text.Trim(),
                    SoTien = nudSoTien.Value,
                    LoaiChi = cboLoaiChi.SelectedItem?.ToString() ?? "Khác",
                    GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
                };

                context.ChiTieus.Add(item);
                context.SaveChanges();

                LoggerService.LogInfo($"Thêm chi tiêu mới thành công. MaCT: {item.MaCT}, Nội dung: {item.NoiDung}, Số tiền: {item.SoTien}");
                MessageBox.Show("Thêm khoản chi tiêu mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataFromDatabase();
                ClearInputs();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, "Lỗi khi thêm mới chi tiêu vào CSDL.");
                MessageBox.Show("Đã xảy ra lỗi khi thêm chi tiêu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCT.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng chi tiêu trên bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            int maCT = int.Parse(txtMaCT.Text);

            try
            {
                using var context = new QLChiTieuContext();
                var item = context.ChiTieus.FirstOrDefault(c => c.MaCT == maCT);
                if (item == null)
                {
                    MessageBox.Show("Không tìm thấy khoản chi tiêu cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                item.NgayChi = dtpNgayChi.Value.Date;
                item.NoiDung = txtNoiDung.Text.Trim();
                item.SoTien = nudSoTien.Value;
                item.LoaiChi = cboLoaiChi.SelectedItem?.ToString() ?? "Khác";
                item.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

                context.SaveChanges();

                LoggerService.LogInfo($"Cập nhật chi tiêu thành công. MaCT: {item.MaCT}, Số tiền: {item.SoTien}");
                MessageBox.Show("Cập nhật khoản chi tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi sửa chi tiêu MaCT={maCT}.");
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCT.Text))
            {
                MessageBox.Show("Vui lòng chọn một khoản chi tiêu trên bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maCT = int.Parse(txtMaCT.Text);

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khoản chi tiêu [Mã {maCT}]: \"{txtNoiDung.Text}\" không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var context = new QLChiTieuContext();
                var item = context.ChiTieus.FirstOrDefault(c => c.MaCT == maCT);
                if (item != null)
                {
                    context.ChiTieus.Remove(item);
                    context.SaveChanges();

                    LoggerService.LogInfo($"Xóa chi tiêu thành công. MaCT: {maCT}");
                    MessageBox.Show("Đã xóa khoản chi tiêu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataFromDatabase();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Lỗi khi xóa chi tiêu MaCT={maCT}.");
                MessageBox.Show("Đã xảy ra lỗi khi xóa chi tiêu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnBoLoc_Click(object? sender, EventArgs e)
        {
            txtTimNoiDung.Clear();
            cboLocLoaiChi.SelectedIndex = 0;
            DateTime now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;
            ApplyFilter();
        }

        private void ClearInputs()
        {
            txtMaCT.Clear();
            txtNoiDung.Clear();
            nudSoTien.Value = 0;
            txtGhiChu.Clear();
            dtpNgayChi.Value = DateTime.Today;
            cboLoaiChi.SelectedIndex = 0;
            dgvChiTieu.ClearSelection();
        }
    }
}
