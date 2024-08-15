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

namespace QuanLyShopQuanAo
{
    public partial class frmDangNhap : Form
    {
        Connect cn = new Connect();
        SqlConnection connsql;
        public frmDangNhap()
        {
            InitializeComponent();
            connsql = cn.connect;
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (txtTK.Text.Trim().Length == 0)
                MessageBox.Show("Ban chua nhap tai khoan!!!");
            else if (txtMK.Text.Trim().Length == 0)
                MessageBox.Show("Ban chua nhap mat khau!!!");
            else
            {
                DangNhap tk = new DangNhap(txtTK.Text, txtMK.Text);
                int check = tk.kiemTraTaiKhoan();
                if (check == -1)
                {
                    MessageBox.Show("Tài khoản rỗng!!!");
                }
                else if (check == 0)
                {
                    MessageBox.Show("Tài khoản không tồn tại!!!");
                }
                else if (check == 1)
                {
                    txtTK.Clear();
                    txtTK.Focus();
                    txtMK.Clear();
                    MessageBox.Show("Đăng nhập thành công!!!");
                    this.Hide();
                    //TrangChu form = new TrangChu();
                    SharedForm form = new SharedForm(check);
                    form.ShowDialog();
                    this.Show();

                }
                else if (check == 2)
                {
                    txtTK.Clear();
                    txtTK.Focus();
                    txtMK.Clear();
                    MessageBox.Show("Đăng nhập thành công!!!");
                    this.Hide();
                    //frmUser form = new frmUser(tk);
                    SharedForm form = new SharedForm(check);
                    form.ShowDialog();
                    this.Show();

                    //Cach 2, nếu sử dụng cách này thì trong file program.cs phải chạy file frmUser
                    // Đóng form đăng nhập
                    //this.Close();

                    //// Hiển thị form frmUser
                    //frmUser form = new frmUser(tk);
                    //Application.Run(form);
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu!!!");
                }
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void chk_HienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_HienMK.Checked == true)
                txtMK.PasswordChar = '\0';
            else txtMK.PasswordChar = '*';
        }


    }
}
