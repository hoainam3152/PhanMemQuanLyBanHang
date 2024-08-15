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
using QuanLyShopQuanAo.ObjectClass;

namespace QuanLyShopQuanAo.Admin
{
    public partial class frmQuanLyNhanVien : Form
    {
        Connect cn = new Connect();
        SqlConnection connsql;
        SqlDataAdapter data;
        DataSet listShopQuanAo;
        DataSet dtNhanVien = new DataSet();
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
            connsql = cn.connect;
        }

        private void loadCBO_GioiTinh()
        {
            string[] gioiTinh = { "Nam", "Nữ" };
            cbo_GioiTinh.DataSource = gioiTinh;
        }

        private void loadDGV_NhanVien()
        {
            string selectStr = "Select * from NHANVIEN";
            data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(dtNhanVien, "NHANVIEN");

            DataColumn[] key = new DataColumn[1];
            key[0] = dtNhanVien.Tables["NHANVIEN"].Columns["MaNV"];
            dtNhanVien.Tables["NHANVIEN"].PrimaryKey = key;

            dtGV_NhanVien.Columns["MaNV"].DataPropertyName = "MaNV";
            dtGV_NhanVien.Columns["TenNV"].DataPropertyName = "TenNV";
            dtGV_NhanVien.Columns["SDTNV"].DataPropertyName = "SDTNV";
            dtGV_NhanVien.Columns["NgaySinhNV"].DataPropertyName = "NgaySinhNV";
            dtGV_NhanVien.Columns["DiaChiNV"].DataPropertyName = "DiaChiNV";
            dtGV_NhanVien.Columns["GioiTinhNV"].DataPropertyName = "GioiTinhNV";
            dtGV_NhanVien.Columns["CCCD"].DataPropertyName = "CCCD";
            dtGV_NhanVien.Columns["NVL"].DataPropertyName = "NVL";
            dtGV_NhanVien.Columns["Luong"].DataPropertyName = "Luong";
            dtGV_NhanVien.Columns["MATK"].DataPropertyName = "MATK";

            dtGV_NhanVien.DataSource = dtNhanVien.Tables["NHANVIEN"];
        }

        private void loadDGV_TaiKhoan()
        {
            listShopQuanAo = new DataSet();

            string selectStr = "Select * from DANGNHAP";
            SqlDataAdapter data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(listShopQuanAo, "DANGNHAP");

            dtGV_TaiKhoan.Columns["MaTKDN"].DataPropertyName = "MaTK";
            dtGV_TaiKhoan.Columns["TAIKHOAN"].DataPropertyName = "TAIKHOAN";
            dtGV_TaiKhoan.Columns["MATKHAU"].DataPropertyName = "MATKHAU";
            dtGV_TaiKhoan.Columns["LOAITK"].DataPropertyName = "LOAITK";

            string[] loaiTK = { "admin", "user" };

            var cboLoaiTK = (DataGridViewComboBoxColumn)dtGV_TaiKhoan.Columns["LOAITK"];

            cboLoaiTK.DataSource = loaiTK;

            dtGV_TaiKhoan.DataSource = listShopQuanAo.Tables["DANGNHAP"];
        }

        private void load_CBOMaTK()
        {
            listShopQuanAo = new DataSet();
            string selectStr = "Select * from DANGNHAP";
            SqlDataAdapter data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(listShopQuanAo, "CBOMaTK");
            cboMaTK.DataSource = listShopQuanAo.Tables["CBOMaTK"];
            cboMaTK.DisplayMember = "MaTK";

            cboMaTK.SelectedIndex = -1;
        }

        void dataBingDing(DataTable pDT)
        {
            txtMaNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            cbo_GioiTinh.DataBindings.Clear();
            txtSDTNV.DataBindings.Clear();
            txtNgaySinh.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            txtLuong.DataBindings.Clear();
            txtNamVaoLam.DataBindings.Clear();
            cboMaTK.DataBindings.Clear();

            txtMaNV.DataBindings.Add("Text", pDT, "MaNV");
            txtTenNV.DataBindings.Add("Text", pDT, "TenNV");
            cbo_GioiTinh.DataBindings.Add("Text", pDT, "GioiTinhNV");
            txtSDTNV.DataBindings.Add("Text", pDT, "SDTNV");
            txtNgaySinh.DataBindings.Add("Text", pDT, "NgaySinhNV");
            txtDiaChi.DataBindings.Add("Text", pDT, "DiaChiNV");
            txtCCCD.DataBindings.Add("Text", pDT, "CCCD");
            txtLuong.DataBindings.Add("Text", pDT, "Luong");
            txtNamVaoLam.DataBindings.Add("Text", pDT, "NVL");
            cboMaTK.DataBindings.Add("Text", pDT, "MATK");
        }

        private void ngatDataBingDing()
        {
            txtMaNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            cbo_GioiTinh.DataBindings.Clear();
            txtSDTNV.DataBindings.Clear();
            txtNgaySinh.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            txtLuong.DataBindings.Clear();
            txtNamVaoLam.DataBindings.Clear();
            cboMaTK.DataBindings.Clear();
        }

        private void khoaTextBox()
        {
            foreach (Control item in gb_TTNV.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))
                    item.Enabled = false;
            }
        }

        private void moTextBox()
        {
            foreach (Control item in gb_TTNV.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))
                    item.Enabled = true;
            }
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadCBO_GioiTinh();
            load_CBOMaTK();
            loadDGV_NhanVien();
            dataBingDing(dtNhanVien.Tables["NHANVIEN"]);
            loadDGV_TaiKhoan();

            dtGV_NhanVien.ReadOnly = true;
            dtGV_TaiKhoan.ReadOnly = true;

            dtGV_NhanVien.AllowUserToAddRows = false;
            dtGV_TaiKhoan.AllowUserToAddRows = false;

            khoaTextBox();
        }

        private void xoaThongTinNV()
        {
            foreach (Control item in gb_TTNV.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox))
                    item.Text = "";
            }
            cboMaTK.SelectedIndex = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xoaThongTinNV();

            ngatDataBingDing();

            btnLuu.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

            btnTaoTK.Enabled = false;
            btnXoaTK.Enabled = false;
            btnSuaTK.Enabled = false;
            btnLuuTK.Enabled = false;

            moTextBox();

            dtGV_NhanVien.FirstDisplayedScrollingRowIndex = dtGV_NhanVien.Rows.Count - 1; 
        }

        private void loadDGV_TaiKhoan(string maTK)
        {
            if (maTK != string.Empty)
            {
                DataTable dtTable = listShopQuanAo.Tables["DANGNHAP"];
                DataRow[] tableTheoMa = dtTable.Select("MATK = '" + maTK + "'");
                if (tableTheoMa.Length == 0)
                {
                    dtGV_TaiKhoan.DataSource = listShopQuanAo.Tables["DANGNHAP"];
                    return;
                }
                DataTable TableCopy = tableTheoMa.CopyToDataTable();
                dtGV_TaiKhoan.DataSource = TableCopy;
            }
            else loadDGV_TaiKhoan();
        }

        private void dtGV_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dtGV_NhanVien.Rows[e.RowIndex];

                string maTK = selectedRow.Cells["MaTK"].Value.ToString();
                loadDGV_TaiKhoan(maTK.Trim());
            }
        }

        private void resetDuLieu()
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;

            btnTaoTK.Enabled = true;
            btnXoaTK.Enabled = true;
            btnSuaTK.Enabled = true;
            btnLuuTK.Enabled = false;

            dtGV_NhanVien.Enabled = true;

            dtGV_TaiKhoan.ReadOnly = true;
            dtGV_TaiKhoan.AllowUserToAddRows = false;

            khoaTextBox();

            dataBingDing(dtNhanVien.Tables["NHANVIEN"]);
        }

        private void xoaDuLieuTrong_TrongDTGV_TaiKhoan()
        {
            for (int i = dtGV_TaiKhoan.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dtGV_TaiKhoan.Rows[i];

                bool isRowEmpty = true;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        isRowEmpty = false;
                        break;
                    }
                }

                if (isRowEmpty)
                {
                    dtGV_TaiKhoan.Rows.Remove(row);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetDuLieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim() == "NV000")
            {
                resetDuLieu();
                MessageBox.Show("Không thể xoá!!!");
                return;
            }
            DialogResult r = MessageBox.Show("Bạn có muốn xoá nhân viên này chứ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                try
                {
                    DataRow deleteRow = dtNhanVien.Tables["NHANVIEN"].Rows.Find(txtMaNV.Text);
                    deleteRow.Delete();
                    SqlCommandBuilder cB = new SqlCommandBuilder(data);
                    data.Update(dtNhanVien, "NHANVIEN");
                    MessageBox.Show("Thành công");
                    loadDGV_TaiKhoan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim() == "NV000")
            {
                resetDuLieu();
                MessageBox.Show("Không thể sửa!!!");
                return;
            }

            ngatDataBingDing();

            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            btnTaoTK.Enabled = false;
            btnXoaTK.Enabled = false;
            btnSuaTK.Enabled = false;
            btnLuuTK.Enabled = false;

            foreach (Control item in gb_TTNV.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))
                    item.Enabled = true;
            }

            txtMaNV.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Lưu thay đổi nhân viên?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                return;
            else
            {
                try
                {
                    if (txtMaNV.Enabled == true) //Them
                    {
                        if (txtMaNV.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập Mã nhân viên!!!");
                            return;
                        }
                        if (txtTenNV.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập Tên nhân viên!!!");
                            return;
                        }
                        DataRow dr = dtNhanVien.Tables["NHANVIEN"].Rows.Find(txtMaNV.Text);
                        if (dr == null)
                        {
                            DataRow insertRow = dtNhanVien.Tables["NHANVIEN"].NewRow();
                            insertRow["MaNV"] = txtMaNV.Text;
                            insertRow["TenNV"] = txtTenNV.Text;
                            insertRow["GioiTinhNV"] = cbo_GioiTinh.Text;
                            insertRow["SDTNV"] = txtSDTNV.Text;
                            if (txtNgaySinh.Text != string.Empty)
                            {
                                DateTime NgaySinhNV = DateTime.Parse(txtNgaySinh.Text);
                                insertRow["NgaySinhNV"] = NgaySinhNV.ToString("yyyy-MM-dd");
                            }
                            else insertRow["NgaySinhNV"] = DBNull.Value;
                            insertRow["DiaChiNV"] = txtDiaChi.Text;
                            insertRow["CCCD"] = txtCCCD.Text;
                            if (txtLuong.Text == string.Empty)
                                insertRow["Luong"] = 0;
                            else insertRow["Luong"] = txtLuong.Text;
                            if (txtNamVaoLam.Text == string.Empty)
                                insertRow["NVL"] = DateTime.Now.Year;
                            else insertRow["NVL"] = txtNamVaoLam.Text;
                            if (cboMaTK.SelectedIndex == -1)
                                insertRow["MATK"] = DBNull.Value;
                            else insertRow["MATK"] = cboMaTK.Text;
                            dtNhanVien.Tables["NHANVIEN"].Rows.Add(insertRow);
                        }
                        else
                        {
                            MessageBox.Show("Trung Mã nhân viên!!!");
                            return;
                        }
                    }
                    else    //Sua
                    {

                        DataRow updateRow = dtNhanVien.Tables["NHANVIEN"].Rows.Find(txtMaNV.Text);
                        if (updateRow != null)
                        {
                            updateRow["TenNV"] = txtTenNV.Text;
                            updateRow["GioiTinhNV"] = cbo_GioiTinh.Text;
                            updateRow["SDTNV"] = txtSDTNV.Text;

                            if (txtNgaySinh.Text != string.Empty)
                            {
                                DateTime NgaySinhNV = DateTime.Parse(txtNgaySinh.Text);
                                updateRow["NgaySinhNV"] = NgaySinhNV.ToString("yyyy-MM-dd");
                            }
                            else updateRow["NgaySinhNV"] = DBNull.Value;

                            updateRow["DiaChiNV"] = txtDiaChi.Text;
                            updateRow["CCCD"] = txtCCCD.Text;
                            if (txtLuong.Text == string.Empty)
                                updateRow["Luong"] = 0;
                            else updateRow["Luong"] = txtLuong.Text;
                            if (txtNamVaoLam.Text == string.Empty)
                                updateRow["NVL"] = DateTime.Now.Year;
                            else updateRow["NVL"] = txtNamVaoLam.Text;
                            if (cboMaTK.SelectedIndex == -1)
                                updateRow["MATK"] = DBNull.Value;
                            else updateRow["MATK"] = cboMaTK.Text;
                        }
                    }
                    SqlCommandBuilder cB = new SqlCommandBuilder(data);
                    data.Update(dtNhanVien, "NHANVIEN");
                    MessageBox.Show("Thành công");
                    xoaThongTinNV();
                    resetDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtLuong.Text.Length == 0 && e.KeyChar == '0') || (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || (txtLuong.Text.Length > 0 && txtLuong.Text[0] == '0'))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
                return;
            }
        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
            }
        }

        private void txtNamVaoLam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtNamVaoLam.Text.Length == 0 && e.KeyChar == '0') || (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || (txtNamVaoLam.Text.Length > 0 && txtNamVaoLam.Text[0] == '0'))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
                return;
            }
        }

        private void txtSDTNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
            }
        }

        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            btnLuuTK.Enabled = true;
            btnXoaTK.Enabled = false;
            btnSuaTK.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;

            dtGV_TaiKhoan.ReadOnly = false;
            dtGV_TaiKhoan.AllowUserToAddRows = true;

            for (int i = 0; i < dtGV_TaiKhoan.Rows.Count - 1; i++)
            {
                dtGV_TaiKhoan.Rows[i].ReadOnly = true;
            }

            dtGV_TaiKhoan.FirstDisplayedScrollingRowIndex = dtGV_TaiKhoan.Rows.Count - 1;
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            btnLuuTK.Enabled = true;
            btnXoaTK.Enabled = false;
            btnTaoTK.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;

            dtGV_TaiKhoan.ReadOnly = false;
            for (int i = 0; i < dtGV_TaiKhoan.Rows.Count - 1; i++)
            {
                dtGV_TaiKhoan.Rows[i].ReadOnly = false;
            }
            dtGV_TaiKhoan.Columns[0].ReadOnly = true;

            dtGV_TaiKhoan.AllowUserToAddRows = false;
        }

        private void btnLuuTK_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Lưu thay đổi tài khoản?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                if (dtGV_TaiKhoan.Columns[0].ReadOnly != true)
                {
                    string selectStr = "select * from DANGNHAP";
                    SqlDataAdapter dataTK = new SqlDataAdapter(selectStr, connsql);
                    try
                    {
                        SqlCommandBuilder cmb = new SqlCommandBuilder(dataTK);
                        dataTK.Update(listShopQuanAo, "DANGNHAP");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Trùng Mã tài khoản");
                        return;
                    }
                }
                else
                {
                    DataTable tempTable = ((DataTable)dtGV_TaiKhoan.DataSource).Copy();
                    if (tempTable.PrimaryKey.Length > 0)
                    {
                        // Xoá ràng buộc khóa chính
                        tempTable.Constraints.Remove("PrimaryKey");
                        tempTable.PrimaryKey = null;
                    }
                    string selectStr = "SELECT * FROM DANGNHAP";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(selectStr, connsql);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    dataAdapter.Update(tempTable);
                }
                    
                    dataBingDing(dtNhanVien.Tables["NHANVIEN"]);
                    MessageBox.Show("Thành công");
                    //loadDGV_NhanVien();
                    load_CBOMaTK();
                    resetDuLieu();
            }
        }

        private void dtGV_TaiKhoan_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dtGV_TaiKhoan.Rows[e.RowIndex];
    
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    MessageBox.Show("Nhập đầy đủ thông tin tài khoản!!!");
                    break;
                }
            }
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn xoá tài khoản này chứ!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                try
                {
                    if (dtGV_TaiKhoan.SelectedCells.Count > 0)
                    {
                        // Lấy đối tượng DataGridViewCell của ô đang được chọn
                        DataGridViewCell selectedCell = dtGV_TaiKhoan.SelectedCells[0];

                        // Truy cập giá trị của ô đang được chọn
                        object cellValue = selectedCell.Value;

                        // Kiểm tra và sử dụng giá trị của ô đang được chọn
                        if (cellValue != null)
                        {
                            // Sử dụng giá trị của ô đang được chọn
                            string maTK = cellValue.ToString();
                            connsql.Open();

                            string selectString = string.Format("select count (*) from NHANVIEN where MATK='{0}'", maTK);
                            SqlCommand cmd = new SqlCommand(selectString, connsql);
                            int count = (int)cmd.ExecuteScalar();
                
                            if (count >= 1)
                            {
                                connsql.Close();
                                MessageBox.Show("Không thể xoá do đang có nhân viên xài tài khoản này!!!");
                                return;
                            }
                            else
                            {
                                string deleteStr = "delete DANGNHAP where MATK = '" + maTK + "'";
                                cmd = new SqlCommand(deleteStr, connsql);
                                cmd.ExecuteNonQuery();
                                connsql.Close();
                                MessageBox.Show("Thành công");
                                //dtGV_TaiKhoan.Refresh();
                                loadDGV_TaiKhoan();
                                load_CBOMaTK();
                                resetDuLieu();
                            } 
                        }
                        else
                        {
                            Console.WriteLine("Ô đang được chọn không có giá trị.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không có ô nào được chọn trong bảng Nhân Viên.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Thất bại");
                }
            }
        }
    }
}
