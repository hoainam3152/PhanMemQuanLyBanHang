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
using QuanLyShopQuanAo.User;
using QuanLyShopQuanAo.User.Class;

namespace QuanLyShopQuanAo.User
{
    public partial class frmBanHang : Form
    {
        Connect cn = new Connect();
        SqlConnection connsql;
        DataSet listDMSP;
        public frmBanHang()
        {
            InitializeComponent();
            connsql = cn.connect;
        }

        private void load_FLP_DMSO(string selectStr)
        {
            //Xoá dữ liệu hiện tại
            fLP_DMSO.Controls.Clear();

            // Hiển thị dữ liệu sau khi lọc trong một DataGridView hoặc điều chỉnh dữ liệu hiển thị theo yêu cầu của bạn
            listDMSP = new DataSet();
            SqlDataAdapter data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(listDMSP, "DanhMuc_SP");

            foreach (DataRow item in listDMSP.Tables["DanhMuc_SP"].Rows)
            {
                //Tạo 1 panel để chứa hình và các thông tin
                Panel pn = new Panel();
                pn.Size = new Size(142, 200);
                pn.BorderStyle = BorderStyle.FixedSingle;

                //Tạo 1 PictureBox để chứa hình
                PictureBox pb = new PictureBox();
                pb.Size = new Size(pn.Width, 140);
                string imagePath =  item["HinhSP"].ToString();
                pb.Image = Image.FromFile(imagePath);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Click += Panel_Click;

                //Tạo tên sản phẩm và giá tiền
                Label lbTenSP = new Label();
                lbTenSP.Width = pn.Width;
                lbTenSP.Text = item["TenSP"].ToString();
                lbTenSP.Font = new Font("Constantia", 10, FontStyle.Bold);
                lbTenSP.AutoEllipsis = true;
                lbTenSP.Location = new Point(8, 150);
                lbTenSP.Click += Panel_Click;

                Label lbGiaTien = new Label();
                // Chuyển đổi item["GiaBan"] thành kiểu double
                double giaBanDouble = Convert.ToDouble(item["GiaBan"].ToString());
                lbGiaTien.Text = giaBanDouble.ToString("#,##0") + "đ";
                lbGiaTien.Font = new Font("Palatino Linotype", 8);
                lbGiaTien.AutoSize = true;
                lbGiaTien.Location = new Point(8, 176);
                lbGiaTien.Click += Panel_Click;

                Label lbSanCo = new Label();
                lbSanCo.Text = "Sẵn có: " + item["SoLuongTon"].ToString();
                lbSanCo.Font = new Font("Palatino Linotype", 8);
                lbSanCo.Location = new Point(75, 177);
                lbSanCo.Click += Panel_Click;

                //Thêm PictureBox vào panel
                pn.Controls.Add(pb);
                pn.Controls.Add(lbTenSP);
                pn.Controls.Add(lbGiaTien);
                pn.Controls.Add(lbSanCo);

                //item tương ứng cho select
                pn.Tag = item["MaSP"].ToString();

                //Thêm panel vào flowLayoutPanel1
                fLP_DMSO.Controls.Add(pn);
            }
        }

        private void load_cbo_DM()
        {
            listDMSP = new DataSet();
            string selectStr = "Select * from LOAISP";
            SqlDataAdapter data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(listDMSP, "LOAISP");
            cbo_DM.DataSource = listDMSP.Tables["LOAISP"];
            cbo_DM.DisplayMember = "TenL";
            cbo_DM.ValueMember = "MaL";
        }

        private void load_cbo_GiaTien()
        {
            string[] listStr = { "--Giá--", "Từ thấp đến cao", "Từ cao đến thấp" };
            cbo_GiaTien.DataSource = listStr;
        }

        private void load_cbo_NhanVien()
        {
            listDMSP = new DataSet();
            string selectStr = "Select MaNV, TenNV from NHANVIEN";
            SqlDataAdapter data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(listDMSP, "NHANVIEN");
            cbo_NhanVien.DataSource = listDMSP.Tables["NHANVIEN"];
            cbo_NhanVien.DisplayMember = "TenNV";
            cbo_NhanVien.ValueMember = "MaNV";
        }

        private void load_cbo_HTGiamGia()
        {
            string[] listStr = { "--Hình Thức Giảm Giá--", "Giảm giá theo %", "GIảm giá trực tiếp" };
            cbo_HTGiamGia.DataSource = listStr;
        }

        private void load_cbo_HTThanhToan()
        {
            string[] listStr = { "--Hình Thức Thanh Toán--", "Tiền mặt", "Chuyển khoản" };
            cbo_HTThanhToan.DataSource = listStr;
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            load_cbo_DM();
            load_cbo_GiaTien();
            load_cbo_NhanVien();
            load_cbo_HTGiamGia();
            load_cbo_HTThanhToan();
            string selectStr = "Select MaSP, TenSP, HinhSP, GiaBan, SoLuongTon from SANPHAM";
            load_FLP_DMSO(selectStr);
            dtGV_GioHang.AllowUserToAddRows = false;
        }

        // Xử lý sự kiện click trên Panel
        private void Panel_Click(object sender, EventArgs e)
        {

            //Biến sender đại diện cho đối tượng gửi sự kiện, ở đây là panel được click.
            Panel panel = (Panel)((Control)sender).Parent;

            //Gọi frmThongTinSP để lấy thông tin 
            string selectedMaSP = (string)panel.Tag;
            frmThongTinSP form = new frmThongTinSP(selectedMaSP);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            //Thêm dữ liệu vào dtGV_GioHang
            DataTable listGH = QuanLyGioHang.Instance.LayDuLieuGioHang();

            dtGV_GioHang.Columns[0].DataPropertyName = "MaSP";
            dtGV_GioHang.Columns[1].DataPropertyName = "TenSP";
            dtGV_GioHang.Columns[2].DataPropertyName = "KichCo";
            dtGV_GioHang.Columns[3].DataPropertyName = "SoLuong";
            dtGV_GioHang.Columns[4].DataPropertyName = "GiaBan";
            dtGV_GioHang.Columns[5].DataPropertyName = "ThanhTien";

            dtGV_GioHang.DataSource = listGH;

            cbo_NhanVien.Enabled = true;
            txtTenKH.Enabled = true;
            mtxt_SDTKH.Enabled = true;

            capNhapThanhToan(QuanLyGioHang.Instance.TongThanhToan);
        }

        private void capNhapThanhToan(double tongTien)
        {
            lbTongTien.Text = tongTien.ToString("#,##0") + "đ";
            double giamGia;
            if (txtGiamGia.Text.Length == 0)
                giamGia = 0;
            else giamGia = double.Parse(txtGiamGia.Text);

            double tongThanhToan = 0;

            if (cbo_HTGiamGia.SelectedIndex == 1)
            {
                double phanTram = giamGia / 100;
                tongThanhToan = tongTien -= tongTien * phanTram;
            }
            else tongThanhToan = tongTien -= giamGia;
            lbTongThanhToan.Text = tongThanhToan.ToString("#,##0") + "đ";

            if (cbo_HTThanhToan.SelectedIndex == 1)
            {
                if (txtTienNhan.Text.Length > 0)
                {
                    double tienNhan = double.Parse(txtTienNhan.Text);
                    lbTienThua.Text = (tienNhan - tongThanhToan).ToString("#,##0") + "đ";
                }
            }
        }

        private void cbo_DM_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbo_GiaTien.SelectedItem = 0;
            string selectStr;
            if (cbo_DM.SelectedIndex != 0)
                selectStr = "Select MaSP, TenSP, HinhSP, GiaBan, SoLuongTon, SP.MaL from SANPHAM SP, LOAISP L where SP.MaL = L.MaL AND SP.MaL = '" + cbo_DM.SelectedValue.ToString() + "'";
            else selectStr = "Select MaSP, TenSP, HinhSP, GiaBan, SoLuongTon, SP.MaL from SANPHAM SP, LOAISP L where SP.MaL = L.MaL";
            load_FLP_DMSO(selectStr);
        }

        private void cbo_GiaTien_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbo_DM.SelectedItem = 0;
            string selectStr;
            if (cbo_GiaTien.SelectedIndex == 0)
                selectStr = "Select MaSP, TenSP, HinhSP, GiaBan, SoLuongTon, SP.MaL from SANPHAM SP, LOAISP L where SP.MaL = L.MaL";
            else if (cbo_GiaTien.SelectedIndex == 1)
                selectStr = "Select MaSP, TenSP, HinhSP, GiaBan, SoLuongTon, SP.MaL from SANPHAM SP, LOAISP L  where SP.MaL = L.MaL Order By SP.GiaBan ASC;";
            else selectStr = "Select MaSP, TenSP, HinhSP, GiaBan, SoLuongTon, SP.MaL from SANPHAM SP, LOAISP L  where SP.MaL = L.MaL Order By SP.GiaBan DESC;";
            load_FLP_DMSO(selectStr);
        }

        private void cbo_NhanVien_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbo_NhanVien.SelectedIndex == 0)
            {
                cbo_HTThanhToan.Enabled = false;
                cbo_HTGiamGia.Enabled = false;
                txtGiamGia.Enabled = false;
                txtTienNhan.Enabled = false;
                chk_ChuaThanhToan.Enabled = false;
                chk_DatCoc.Enabled = false;
                txtGhiChu.Enabled = false;
                btnThanhToan.Enabled = false;
            }
            else
            {
                cbo_HTThanhToan.Enabled = true;
                cbo_HTGiamGia.Enabled = true;
                btnThanhToan.Enabled = true;
                chk_ChuaThanhToan.Enabled = true;
                chk_DatCoc.Enabled = true;
                txtGhiChu.Enabled = true;
            }
        }

        private void cbo_HTThanhToan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtTienNhan.Text = "";
            capNhapThanhToan(double.Parse(lbTongTien.Text.Substring(0, lbTongTien.Text.Length - 1)));
            lbTienThua.Text = "0đ";

            if (cbo_HTThanhToan.SelectedIndex == 1)
                txtTienNhan.Enabled = true;
            else txtTienNhan.Enabled = false;
        }

        private void cbo_HTGiamGia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtGiamGia.Text = "";
            capNhapThanhToan(double.Parse(lbTongTien.Text.Substring(0, lbTongTien.Text.Length - 1)));
            if (cbo_HTGiamGia.SelectedIndex == 0)
            {
                txtGiamGia.Enabled = false;
                return;
            }
            txtGiamGia.Enabled = true;

            if (cbo_HTGiamGia.SelectedIndex == 1)
                lb_TTGG.Text = "%";
            else lb_TTGG.Text = "đ";

        }

        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra TextBox đang trống và ký tự đầu tiên là 0, ký tự không phải là số, TextBox đã có giá trị và ký tự đầu tiên là 0 thì không cho phép
            if ((txtGiamGia.Text.Length == 0 && e.KeyChar == '0') || (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || (txtGiamGia.Text.Length > 0 && txtGiamGia.Text[0] == '0'))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
                return;
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            double giamGia;
            if (txtGiamGia.Text.Length == 0)
                giamGia = 0;
            else giamGia = double.Parse(txtGiamGia.Text);

            double tongTien = double.Parse(lbTongTien.Text.Substring(0, lbTongTien.Text.Length - 1));
            if (cbo_HTGiamGia.SelectedIndex == 1)
            {
                if (giamGia > 100)
                {
                    giamGia = 100;
                    txtGiamGia.Text = "100";
                }
                double phanTram = giamGia / 100;
                lbTongThanhToan.Text = (tongTien - tongTien * phanTram).ToString("#,##0") + "đ";
            }
            else
                lbTongThanhToan.Text = (tongTien - giamGia).ToString("#,##0") + "đ";

            //đưa trỏ chuột nằm bên phải
            txtGiamGia.SelectionStart = txtGiamGia.TextLength;
            txtGiamGia.SelectionLength = 0;
        }

        private void txtTienNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Nếu ký tự đầu là 0 hoặc ký tự không phải là số thì huỷ
            if ((txtTienNhan.Text.Length == 0 && e.KeyChar == '0') || (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || (txtTienNhan.Text.Length > 0 && txtTienNhan.Text[0] == '0'))
            {
                e.Handled = true; // Hủy bỏ ký tự không hợp lệ
                return;
            }
        }

        private void txtTienNhan_TextChanged(object sender, EventArgs e)
        {
            if (txtTienNhan.Text.Length > 0)
            {
                double tienNhan = double.Parse(txtTienNhan.Text);
                double tongThanhToan = double.Parse(lbTongThanhToan.Text.Substring(0, lbTongThanhToan.Text.Length - 1));
                lbTienThua.Text = (tienNhan - tongThanhToan).ToString("#,##0") + "đ";
            }
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            double tienThua = double.Parse(lbTienThua.Text.Substring(0, lbTienThua.Text.Length - 1));
            if (tienThua < 0)
            {
                MessageBox.Show("TIỀN NHẬN KHÔNG ĐỦ!!!");
                return;
            }

            if (chk_DatCoc.Checked == true)
            {
                if (string.IsNullOrEmpty(txtGhiChu.Text))
                {
                    MessageBox.Show("Vui lòng nhập số tiền đặt cọc vào ghi chú!!!");
                    return;
                }
            }

            if (connsql.State == ConnectionState.Closed) 
                connsql.Open();
            int newMaKH = 0;
            //Nếu có thông tin khách hàng thì tạo mới 1 khách hàng
            if (!string.IsNullOrEmpty(txtTenKH.Text.Trim()) || !string.IsNullOrEmpty(mtxt_SDTKH.Text.Trim()))
            {
                string insertStrKH = "INSERT INTO KHACHHANG (TenKH, SDTKH)" + "OUTPUT INSERTED.MaKH VALUES (@TenKH, @SDTKH)";
                SqlCommand cmdKH = new SqlCommand(insertStrKH, connsql);
                cmdKH.Parameters.AddWithValue("@TenKH", txtTenKH.Text);
                string sdtKH = mtxt_SDTKH.Text.Replace(" ", "");
                if (sdtKH.Length < 10)
                {
                    MessageBox.Show("Số điện thoại phải đủ 10 số");
                    return;
                }
                cmdKH.Parameters.AddWithValue("@SDTKH", sdtKH);
                newMaKH = Convert.ToInt32(cmdKH.ExecuteScalar());
            }

            //Tạo mới 1 hoá đơn
            int maHD;
            string insertStrHD = "INSERT INTO HOADON (MaKH, MaNV, NgayBan, TongThanhToan, TrangThai, HinhThucThanhToan, GhiChu)"
                + "OUTPUT INSERTED.MaHD VALUES (@MaKH, @MaNV, @NgayBan, @TongThanhToan, @TrangThai, @HinhThucThanhToan, @GhiChu)";
            SqlCommand cmdHD = new SqlCommand(insertStrHD, connsql);
            //Nếu không nhập thông tin khách hàng thì không cần phải gán giá trị cho MaKH
            if (newMaKH != 0)
                cmdHD.Parameters.AddWithValue("@MaKH", newMaKH);
            else
                cmdHD.Parameters.AddWithValue("@MaKH", DBNull.Value);
            cmdHD.Parameters.AddWithValue("@MaNV", cbo_NhanVien.SelectedValue);
            cmdHD.Parameters.AddWithValue("@NgayBan", DateTime.Now);
            double tongThanhToan = double.Parse(lbTongThanhToan.Text.Substring(0, lbTongThanhToan.Text.Length - 1));
            cmdHD.Parameters.AddWithValue("@TongThanhToan", tongThanhToan);
            if (cbo_HTThanhToan.SelectedIndex == 0)
                cmdHD.Parameters.AddWithValue("@HinhThucThanhToan", "");
            else cmdHD.Parameters.AddWithValue("@HinhThucThanhToan", cbo_HTThanhToan.SelectedItem.ToString());
            
            if (chk_ChuaThanhToan.Checked == true)
                cmdHD.Parameters.AddWithValue("@TrangThai", "Chưa thanh toán");
            else if (chk_DatCoc.Checked == true)
                cmdHD.Parameters.AddWithValue("@TrangThai", "Đặt cọc");
            else cmdHD.Parameters.AddWithValue("@TrangThai", "Đã thanh toán");

            if (cbo_HTGiamGia.SelectedIndex > 0)
            {
                string ghiChu = "Tổng thanh toán: " + double.Parse(lbTongTien.Text.Substring(0, lbTongThanhToan.Text.Length - 1)) + " - Giảm: " + txtGiamGia.Text + ". " + txtGhiChu.Text;
                cmdHD.Parameters.AddWithValue("@GhiChu", ghiChu);
            }
            else cmdHD.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

            maHD = Convert.ToInt32(cmdHD.ExecuteScalar());
            
            //Tạo mới CTHD
            foreach (DataGridViewRow item in dtGV_GioHang.Rows)
            {
                    string insertStrCTHD = "INSERT INTO CTHD (MaHD, MaSP, KichCo, SoLuong, GiaBan, ThanhTien) VALUES (@MaHD, @MaSP, @KichCo, @SoLuong, @GiaBan, @ThanhTien)";
                    SqlCommand cmdCTHD = new SqlCommand(insertStrCTHD, connsql);
                    cmdCTHD.Parameters.AddWithValue("@MaHD", maHD);
                    cmdCTHD.Parameters.AddWithValue("@MaSP", item.Cells["MaSP"].Value.ToString());
                    cmdCTHD.Parameters.AddWithValue("@KichCo", item.Cells["KichCo"].Value.ToString());
                    cmdCTHD.Parameters.AddWithValue("@SoLuong", int.Parse(item.Cells["SoLuong"].Value.ToString()));
                    cmdCTHD.Parameters.AddWithValue("@GiaBan", double.Parse(item.Cells["GiaBan"].Value.ToString()));
                    cmdCTHD.Parameters.AddWithValue("@ThanhTien", double.Parse(item.Cells["ThanhTien"].Value.ToString()));
                    cmdCTHD.ExecuteScalar();
            }
            connsql.Close();
            MessageBox.Show("Thành công");
            xoaThongTinBanHang(sender, e);
        }

        private void xoaThongTinBanHang(object sender, EventArgs e)
        {
            ////btnThanhToan
            if (sender == btnThanhToan)
            {
                txtTenKH.Text = "";
                txtTenKH.Enabled = false;

                mtxt_SDTKH.Text = "";
                mtxt_SDTKH.Enabled = false;

                DataTable listGH = QuanLyGioHang.Instance.LayDuLieuGioHang();
                listGH.Clear();

                txtTimKiem.Text = "";
                cbo_DM.SelectedIndex = 0;
                cbo_GiaTien.SelectedIndex = 0;
            }

            cbo_NhanVien.SelectedIndex = 0;
            cbo_NhanVien.Enabled = false;

            lbTongTien.Text = "0đ";

            cbo_HTGiamGia.SelectedIndex = 0;
            cbo_HTGiamGia.Enabled = false;

            txtGiamGia.Text = "";
            txtGiamGia.Enabled = false;

            lbTongThanhToan.Text = "0đ";

            cbo_HTThanhToan.SelectedIndex = 0;
            cbo_HTThanhToan.Enabled = false;

            txtTienNhan.Text = "";
            txtTienNhan.Enabled = false;

            lbTienThua.Text = "0đ";

            chk_ChuaThanhToan.Checked = false;
            chk_ChuaThanhToan.Enabled = false;

            chk_DatCoc.Checked = false;
            chk_DatCoc.Enabled = false;

            txtGhiChu.Text = "";
            txtGhiChu.Enabled = false;

            btnThanhToan.Enabled = false;
        }

        private void dtGV_GioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dtGV_GioHang.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                DataGridViewRow dongHienTai = dtGV_GioHang.Rows[e.RowIndex];
                double donGia = double.Parse(dongHienTai.Cells["GiaBan"].Value.ToString());
                int soLuong = int.Parse(dtGV_GioHang.CurrentCell.Value.ToString());
                dongHienTai.Cells["ThanhTien"].Value = (soLuong * donGia).ToString("N0");

                DataGridViewColumn cotCuoiCung = dtGV_GioHang.Columns[dtGV_GioHang.Columns.Count - 1];
                double tongTien = 0;
                foreach (DataGridViewRow row in dtGV_GioHang.Rows)
                {
                    tongTien += double.Parse(row.Cells["ThanhTien"].Value.ToString());
                }

                capNhapThanhToan(tongTien);
            }
        }

        private void dtGV_GioHang_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Kiểm tra có dữ liệu trong ô dữ liệu hay không
                if (dtGV_GioHang.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    e.ContextMenuStrip = cms_dtGVGioHang;
                    cms_dtGVGioHang.Tag = e.RowIndex;
                }
                else
                {
                    e.ContextMenuStrip = null; // Không hiển thị ContextMenuStrip
                }
            }
        }

        private void tsmi_Xoa_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)cms_dtGVGioHang.Tag;
            double thanhTienDongHienTai = double.Parse(dtGV_GioHang.Rows[rowIndex].Cells["ThanhTien"].Value.ToString());
            dtGV_GioHang.Rows.RemoveAt(rowIndex);
            double tongTien = 0;
            foreach (DataGridViewRow row in dtGV_GioHang.Rows)
            {
                tongTien += double.Parse(row.Cells["ThanhTien"].Value.ToString());
            }
            if (dtGV_GioHang.RowCount == 0)
            {
                xoaThongTinBanHang(sender, e);
            }
            capNhapThanhToan(tongTien);
        }

        private void chk_ChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ChuaThanhToan.Checked == true)
            {
                chk_DatCoc.Enabled = false;
                cbo_HTThanhToan.SelectedIndex = 0;
            }
            else chk_DatCoc.Enabled = true;
        }

        private void chk_DatCoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_DatCoc.Checked == true)
                chk_ChuaThanhToan.Enabled = false;
            else chk_ChuaThanhToan.Enabled = true;
        }
    }
}
