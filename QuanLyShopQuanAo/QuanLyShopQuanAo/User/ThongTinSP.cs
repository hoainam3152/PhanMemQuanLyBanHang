using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyShopQuanAo.ObjectClass;
using QuanLyShopQuanAo.User.Class;

namespace QuanLyShopQuanAo.User
{
    public partial class frmThongTinSP : Form
    {
        private string maSP;
        SanPham nsp = new SanPham();
        public frmThongTinSP()
        {
            InitializeComponent();
        }

        public frmThongTinSP(string selectedMaSP)
        {
            InitializeComponent();
            this.maSP = selectedMaSP;
        }

        private void frmThongTinSP_Load(object sender, EventArgs e)
        {
            nsp.layThongTinSanPham(maSP);
            //Tạo 1 PictureBox để chứa hình
            string imagePath = nsp.HinhSP;
            pBHinhSP.Image = Image.FromFile(imagePath);
            lbTenSP.Text = nsp.TenSP;
            lbTenSP.AutoEllipsis = true;
            // Chuyển đổi item["GiaBan"] thành kiểu double
            double giaBanDouble = Convert.ToDouble(nsp.GiaBan.ToString());
            lbGiaSP.Text = giaBanDouble.ToString("#,##0") + "đ";

            //Set giá trị đối đa mà nud_SL mà có thể thêm được
            nud_SL.Maximum = nsp.SoLuongTon;
            lb_SP_SC.Text += (" " + nsp.SoLuongTon.ToString());

            lbTTSP.Text = nsp.ThongTinSP;
            lbTTSP.Text += "\n- Chất liệu: " + nsp.ChatLieu + "\n- Form:" + nsp.Form;
            lbTTSP.Text += "\n- Xuất xứ: Việt Nam";
        }

        private void btnThemVaoGioHang_Click(object sender, EventArgs e)
        {
            var button = gb_KichCo.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            if(button == null)
                MessageBox.Show("Bạn chưa chọn kích cỡ");
            else if (nud_SL.Value == 0)
                MessageBox.Show("Bạn chưa chọn số lượng");
            else
            {
                string size = button.Text;
                int soLuong = (int)nud_SL.Value;
                GioHang sp = new GioHang(nsp, size, soLuong);
                QuanLyGioHang.Instance.ThemVaoGioHang(sp);
                MessageBox.Show("Thêm thành công");
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
