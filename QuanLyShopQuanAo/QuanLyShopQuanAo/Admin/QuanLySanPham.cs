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
using System.IO;

namespace QuanLyShopQuanAo.Admin
{
    public partial class frmQuanLySanPham : Form
    {
        Connect cn = new Connect();
        SqlConnection connsql;
        SqlDataAdapter data;
        SqlDataAdapter dataLoaiSP;
        DataSet listShopQuanAo;
        DataSet dtSanPham = new DataSet();
        DataSet dtLoaiSP = new DataSet();
        public frmQuanLySanPham()
        {
            InitializeComponent();
            connsql = cn.connect;
        }

        private void loadCBO_GioiTinh()
        {
            string[] gioiTinh = {"Nam", "Nữ" };
            cbo_GioiTinh.DataSource = gioiTinh;
        }

        private void loadDGV_SanPham()
        {
            string selectStr = "Select * from SANPHAM";
            data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(dtSanPham, "SANPHAM");

            DataColumn[] key = new DataColumn[1];
            key[0] = dtSanPham.Tables["SANPHAM"].Columns["MaSP"];
            dtSanPham.Tables["SANPHAM"].PrimaryKey = key;

            dtGV_SanPham.Columns["MaSP"].DataPropertyName = "MaSP";
            dtGV_SanPham.Columns["TenSP"].DataPropertyName = "TenSP";
            dtGV_SanPham.Columns["HinhSP"].DataPropertyName = "HinhSP";
            dtGV_SanPham.Columns["GiaBan"].DataPropertyName = "GiaBan";
            dtGV_SanPham.Columns["GioiTinh"].DataPropertyName = "GioiTinh";
            dtGV_SanPham.Columns["ThongTinSP"].DataPropertyName = "ThongTinSP";
            dtGV_SanPham.Columns["ChatLieu"].DataPropertyName = "ChatLieu";
            dtGV_SanPham.Columns["Form"].DataPropertyName = "Form";
            dtGV_SanPham.Columns["SoLuongTon"].DataPropertyName = "SoLuongTon";
            dtGV_SanPham.Columns["DaBan"].DataPropertyName = "DaBan";
            dtGV_SanPham.Columns["TinhTrang"].DataPropertyName = "TinhTrang";
            dtGV_SanPham.Columns["MaL"].DataPropertyName = "MaL";

            dtGV_SanPham.DataSource = dtSanPham.Tables["SANPHAM"];
        }

        private void load_CBODanhMuc()
        {
            listShopQuanAo = new DataSet();
            string selectStr = "Select * from LOAISP";
            SqlDataAdapter dataLoaiSP = new SqlDataAdapter(selectStr, connsql);
            dataLoaiSP.Fill(listShopQuanAo, "DANHMUC");
            cboDanhMuc.DataSource = listShopQuanAo.Tables["DANHMUC"];
            cboDanhMuc.DisplayMember = "TenL";
            cboDanhMuc.ValueMember = "MaL";
        }

        void dataBingDing(DataTable pDT)
        {
            txtMaSP.DataBindings.Clear();
            txtTenSP.DataBindings.Clear();
            cbo_GioiTinh.DataBindings.Clear();
            ptB_Hinh.DataBindings.Clear();
            txtGiaBan.DataBindings.Clear();
            txtThongTin.DataBindings.Clear();
            txtChatLieu.DataBindings.Clear();
            txtKieuDang.DataBindings.Clear();
            txtSLT.DataBindings.Clear();
            txtDaBan.DataBindings.Clear();
            txtTinhTrang.DataBindings.Clear();
            cboDanhMuc.DataBindings.Clear();

            txtMaSP.DataBindings.Add("Text", pDT, "MaSP");
            txtTenSP.DataBindings.Add("Text", pDT, "TenSP");
            cbo_GioiTinh.DataBindings.Add("Text", pDT, "GioiTinh");

            ptB_Hinh.DataBindings.Add("ImageLocation", pDT, "HinhSP", true, DataSourceUpdateMode.Never, null);
            ptB_Hinh.SizeMode = PictureBoxSizeMode.StretchImage;

            txtGiaBan.DataBindings.Add("Text", pDT, "GiaBan");
            txtThongTin.DataBindings.Add("Text", pDT, "ThongTinSP");
            txtChatLieu.DataBindings.Add("Text", pDT, "ChatLieu");
            txtKieuDang.DataBindings.Add("Text", pDT, "Form");
            txtSLT.DataBindings.Add("Text", pDT, "SoLuongTon");
            txtDaBan.DataBindings.Add("Text", pDT, "DaBan");
            txtTinhTrang.DataBindings.Add("Text", pDT, "TinhTrang");
            cboDanhMuc.DataBindings.Add("Text", pDT, "MaL");

            string selectedMaL = pDT.Rows[0]["MaL"].ToString().Trim(); // Lấy giá trị "MaL" từ DataTable

            int index = cboDanhMuc.FindStringExact(selectedMaL); // Tìm chỉ mục của giá trị trong ComboBox

            if (index >= 0)
            {
                cboDanhMuc.SelectedIndex = index;
            }

        }

        private void loadDGV_DanhMuc()
        {
            string selectStr = "Select * from LOAISP";
            dataLoaiSP = new SqlDataAdapter(selectStr, connsql);
            dataLoaiSP.Fill(dtLoaiSP, "LOAISP");

            DataColumn[] key = new DataColumn[1];
            key[0] = dtLoaiSP.Tables["LOAISP"].Columns["MaL"];
            dtLoaiSP.Tables["LOAISP"].PrimaryKey = key;

            dtGV_DanhMuc.Columns["MaLSP"].DataPropertyName = "MaL";
            dtGV_DanhMuc.Columns["TenL"].DataPropertyName = "TenL";

            dtGV_DanhMuc.DataSource = dtLoaiSP.Tables["LOAISP"];
        }

        void dataBingDingDanhMuc(DataTable pDT)
        {
            txtMaDM.DataBindings.Clear();
            txtTenDM.DataBindings.Clear();

            txtMaDM.DataBindings.Add("Text", pDT, "MaL");
            txtTenDM.DataBindings.Add("Text", pDT, "TenL");
        }

        private void frmQuanLySanPham_Load(object sender, EventArgs e)
        {
            load_CBODanhMuc();
            loadDGV_SanPham();
            dataBingDing(dtSanPham.Tables["SANPHAM"]);
            loadCBO_GioiTinh();
            loadDGV_DanhMuc();
            dataBingDingDanhMuc(dtLoaiSP.Tables["LOAISP"]);

            dtGV_SanPham.ReadOnly = true;
            dtGV_DanhMuc.ReadOnly = true;

            dtGV_SanPham.AllowUserToAddRows = false;
            dtGV_DanhMuc.AllowUserToAddRows = false;

            khoaTextBox();
            khoaTextBoxDanhMUC();
        }

        private void khoaTextBox()
        {
            foreach (Control item in gb_TTSP.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox))
                    item.Enabled = false;
            }
            btnUpHinh.Enabled = false;
        }

        private void khoaTextBoxDanhMUC()
        {
            txtMaDM.Enabled = false;
            txtTenDM.Enabled = false;
        }

        private void resetDuLieu()
        {
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;

            btnTaoDM.Enabled = true;
            btnXoaDM.Enabled = true;
            btnSuaDM.Enabled = true;
            btnLuuDM.Enabled = false;

            dtGV_SanPham.Enabled = true;
            dtGV_DanhMuc.Enabled = true;

            khoaTextBox();
            khoaTextBoxDanhMUC();

            dataBingDing(dtSanPham.Tables["SANPHAM"]);
            dataBingDingDanhMuc(dtLoaiSP.Tables["LOAISP"]);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetDuLieu();
        }

        private void xoaThongTinSP()
        {
            foreach (Control item in gb_TTSP.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox))
                    item.Text = "";
            }
            ptB_Hinh.ImageLocation = null;
        }

        private void ngatDataBingDing()
        {
            txtMaSP.DataBindings.Clear();
            txtTenSP.DataBindings.Clear();
            cbo_GioiTinh.DataBindings.Clear();
            ptB_Hinh.DataBindings.Clear();
            txtGiaBan.DataBindings.Clear();
            txtThongTin.DataBindings.Clear();
            txtChatLieu.DataBindings.Clear();
            txtKieuDang.DataBindings.Clear();
            txtSLT.DataBindings.Clear();
            txtDaBan.DataBindings.Clear();
            txtTinhTrang.DataBindings.Clear();
            cboDanhMuc.DataBindings.Clear();
        }

        private void ngatDataBingDingDanhMuc()
        {
            txtMaDM.DataBindings.Clear();
            txtTenDM.DataBindings.Clear();
        }

        private void moTextBox()
        {
            foreach (Control item in gb_TTSP.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() == typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))
                    item.Enabled = true;
            }
            btnUpHinh.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xoaThongTinSP();

            ngatDataBingDing();

            btnLuu.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

            btnTaoDM.Enabled = false;
            btnXoaDM.Enabled = false;
            btnSuaDM.Enabled = false;
            btnLuuDM.Enabled = false;

            moTextBox();

            dtGV_SanPham.FirstDisplayedScrollingRowIndex = dtGV_SanPham.Rows.Count - 1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Lưu thay đổi sản phẩm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                return;
            else
            {
                try
                {
                    if (txtMaSP.Enabled == true) //Them
                    {
                        if (txtMaSP.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập Mã nhân viên!!!");
                            return;
                        }
                        if (txtTenSP.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập Tên nhân viên!!!");
                            return;
                        }

                        DataRow dr = dtSanPham.Tables["SANPHAM"].Rows.Find(txtMaSP.Text);
                        if (dr == null)
                        {
                            DataRow insertRow = dtSanPham.Tables["SANPHAM"].NewRow();
                            insertRow["MaSP"] = txtMaSP.Text;
                            insertRow["TenSP"] = txtTenSP.Text;
                            insertRow["HinhSP"] = ptB_Hinh.ImageLocation;

                            if (txtGiaBan.Text == string.Empty)
                                insertRow["GiaBan"] = 0;
                            else insertRow["GiaBan"] = txtGiaBan.Text;

                            insertRow["GioiTinh"] = cbo_GioiTinh.Text;

                            insertRow["ThongTinSP"] = txtThongTin.Text;
                            insertRow["ChatLieu"] = txtChatLieu.Text;
                            insertRow["Form"] = txtKieuDang.Text;

                            if (txtSLT.Text == string.Empty)
                                insertRow["SoLuongTon"] = 0;
                            else insertRow["SoLuongTon"] = txtSLT.Text;

                            if (txtDaBan.Text == string.Empty)
                                insertRow["DaBan"] = 0;
                            else insertRow["DaBan"] = txtDaBan.Text;

                            insertRow["TinhTrang"] = txtTinhTrang.Text;
                            if (cboDanhMuc.SelectedIndex == 0 || cboDanhMuc.SelectedIndex == -1)
                                insertRow["MaL"] = DBNull.Value;
                            else insertRow["MaL"] = cboDanhMuc.SelectedValue.ToString();
                            dtSanPham.Tables["SANPHAM"].Rows.Add(insertRow);
                        }
                        else
                        {
                            MessageBox.Show("Trung Mã sản phẩm!!!");
                            return;
                        }
                    }
                    else    //Sua
                    {

                        DataRow updateRow = dtSanPham.Tables["SANPHAM"].Rows.Find(txtMaSP.Text);
                        if (updateRow != null)
                        {
                            updateRow["MaSP"] = txtMaSP.Text;
                            updateRow["TenSP"] = txtTenSP.Text;

                            //if (ptB_Hinh.ImageLocation == string.Empty)

                            updateRow["HinhSP"] = ptB_Hinh.ImageLocation;

                            if (txtGiaBan.Text == string.Empty)
                                updateRow["GiaBan"] = 0;
                            else updateRow["GiaBan"] = txtGiaBan.Text;

                            updateRow["GioiTinh"] = cbo_GioiTinh.Text;

                            updateRow["ThongTinSP"] = txtThongTin.Text;
                            updateRow["ChatLieu"] = txtChatLieu.Text;
                            updateRow["Form"] = txtKieuDang.Text;

                            if (txtSLT.Text == string.Empty)
                                updateRow["SoLuongTon"] = 0;
                            else updateRow["SoLuongTon"] = txtSLT.Text;

                            if (txtDaBan.Text == string.Empty)
                                updateRow["DaBan"] = 0;
                            else updateRow["DaBan"] = txtDaBan.Text;

                            updateRow["TinhTrang"] = txtTinhTrang.Text;
                            if (cboDanhMuc.SelectedIndex == 0)
                                updateRow["MaL"] = DBNull.Value;
                            else updateRow["MaL"] = cboDanhMuc.SelectedValue.ToString();
                        }
                    }
                    SqlCommandBuilder cB = new SqlCommandBuilder(data);
                    data.Update(dtSanPham, "SANPHAM");
                    MessageBox.Show("Thành công");
                    xoaThongTinSP();
                    resetDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ngatDataBingDing();

            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            btnTaoDM.Enabled = false;
            btnXoaDM.Enabled = false;
            btnSuaDM.Enabled = false;
            btnLuuDM.Enabled = false;

            moTextBox();

            foreach (Control item in gb_TTSP.Controls)
            {
                if (item.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)item;
                    textBox.SelectionStart = textBox.Text.Length;
                    textBox.SelectionLength = 0;
                }
            }

            txtMaSP.Enabled = false;
        }

        private void btnUpHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Chỉ cho phép chọn tệp hình ảnh
            openFileDialog.Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string duongDanAnh = openFileDialog.FileName;
                ptB_Hinh.ImageLocation = duongDanAnh;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn xoá sản phẩm này chứ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                try
                {
                    DataRow deleteRow = dtSanPham.Tables["SANPHAM"].Rows.Find(txtMaSP.Text);
                    deleteRow.Delete();
                    SqlCommandBuilder cB = new SqlCommandBuilder(data);
                    data.Update(dtSanPham, "SANPHAM");
                    MessageBox.Show("Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void xoaThongTinDM()
        {
            txtMaDM.Text = "";
            txtTenDM.Text = "";
        }

        private void moTextBoxDanhMuc()
        {
            txtMaDM.Enabled = true;
            txtTenDM.Enabled = true;
        }

        private void btnTaoDM_Click(object sender, EventArgs e)
        {
            xoaThongTinDM();

            ngatDataBingDingDanhMuc();

            btnLuu.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

            btnXoaDM.Enabled = false;
            btnSuaDM.Enabled = false;
            btnLuuDM.Enabled = true;

            moTextBoxDanhMuc();

            dtGV_DanhMuc.FirstDisplayedScrollingRowIndex = dtGV_DanhMuc.Rows.Count - 1;
        }

        private void btnSuaDM_Click(object sender, EventArgs e)
        {
            if (txtMaDM.Text.Trim() == "L000")
            {
                resetDuLieu();
                MessageBox.Show("Không thể sửa!!!");
                return;
            }

            ngatDataBingDingDanhMuc();

            btnLuu.Enabled = false;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

            btnTaoDM.Enabled = false;
            btnXoaDM.Enabled = false;
            btnLuuDM.Enabled = true;

            txtTenDM.Enabled = true;

            txtTenDM.SelectionStart = txtTenDM.Text.Length;
            txtTenDM.SelectionLength = 0;
        }

        private void btnLuuDM_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Lưu thay đổi danh mục?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.No)
                return;
            else
            {
                try
                {
                    if (txtMaDM.Enabled == true) //Them
                    {
                        if (txtMaDM.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập Mã danh mục!!!");
                            return;
                        }
                        if (txtTenDM.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập Tên danh mục!!!");
                            return;
                        }

                        DataRow dr = dtLoaiSP.Tables["LOAISP"].Rows.Find(txtMaSP.Text);
                        if (dr == null)
                        {
                            DataRow insertRow = dtLoaiSP.Tables["LOAISP"].NewRow();
                            insertRow["MaL"] = txtMaDM.Text;
                            insertRow["TenL"] = txtTenDM.Text;

                            dtLoaiSP.Tables["LOAISP"].Rows.Add(insertRow);
                        }
                        else
                        {
                            MessageBox.Show("Trung Mã danh mục!!!");
                            return;
                        }
                    }
                    else    //Sua
                    {

                        DataRow updateRow = dtLoaiSP.Tables["LOAISP"].Rows.Find(txtMaDM.Text);
                        if (updateRow != null)
                        {
                            updateRow["TenL"] = txtTenDM.Text;
                        }
                    }
                    SqlCommandBuilder cB = new SqlCommandBuilder(dataLoaiSP);
                    dataLoaiSP.Update(dtLoaiSP, "LOAISP");
                    MessageBox.Show("Thành công");
                    xoaThongTinDM();
                    load_CBODanhMuc();
                    resetDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            if (txtMaDM.Text.Trim() == "L000")
            {
                resetDuLieu();
                MessageBox.Show("Không thể xoá!!!");
                return;
            }

            DialogResult r = MessageBox.Show("Bạn có muốn xoá danh mục này chứ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                try
                {
                    connsql.Open();
                    string selectString = string.Format("select count (*) from SANPHAM where MaL='{0}'", txtMaDM.Text);
                    SqlCommand cmd = new SqlCommand(selectString, connsql);
                    int count = (int)cmd.ExecuteScalar();
                    if (count >= 1)
                    {
                        connsql.Close();
                        MessageBox.Show("Không thể xoá do đang có sản phẩm liên dùng danh mục này!!!");
                        return;
                    }
                    connsql.Close();
                    DataRow deleteRow = dtLoaiSP.Tables["LOAISP"].Rows.Find(txtMaDM.Text);
                    deleteRow.Delete();
                    SqlCommandBuilder cB = new SqlCommandBuilder(data);
                    dataLoaiSP.Update(dtLoaiSP, "LOAISP");
                    MessageBox.Show("Thành công");
                    load_CBODanhMuc();
                    resetDuLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((txtGiaBan.Text.Length == 0 && e.KeyChar == '0') || (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || (txtGiaBan.Text.Length > 0 && txtGiaBan.Text[0] == '0'))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
                return;
            }
        }

        private void txtSLT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
            }
        }

        private void txtDaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
            }
        }
    }
}
