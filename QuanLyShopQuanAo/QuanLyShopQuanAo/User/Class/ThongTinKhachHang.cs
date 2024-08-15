using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QuanLyShopQuanAo.ObjectClass;
using System.Data;

namespace QuanLyShopQuanAo.User.Class
{
    class ThongTinKhachHang
    {
        private static ThongTinKhachHang instance;
        private DangNhap TK { get; set; }
        public KhachHang kh;

        private ThongTinKhachHang()
        {
            kh = new KhachHang();
        }

        public static ThongTinKhachHang Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThongTinKhachHang();
                }
                return instance;
            }
        }

        Connect cn = new Connect();
        SqlConnection connsql;

        public void SetDangNhap(DangNhap dangNhap)
        {
            TK = dangNhap;
            connsql = cn.connect;
            //Lấy mã tài khoản
            string selectStr = string.Format("select MaTK from DANGNHAP where TAIKHOAN = '{0}' and MATKHAU = '{1}'", TK.TaiKhoan, TK.MatKhau);
            connsql.Open();
            SqlCommand cmd = new SqlCommand(selectStr, connsql);
            DataTable dtTable = new DataTable();
            dtTable.Load(cmd.ExecuteReader());
            DataRow row = dtTable.Rows[0];
            string maTK = row["MaTK"].ToString();

            //Lấy thông tin khách hàng từ mã tài khoản
            selectStr = string.Format("select * from KHACHHANG where MaTk = '{0}'", maTK);
            cmd = new SqlCommand(selectStr, connsql);
            dtTable = new DataTable();
            dtTable.Load(cmd.ExecuteReader());
            row = dtTable.Rows[0];
            kh.MaKH = row["MaKH"].ToString();
            kh.TenKH = row["TenKH"].ToString();
            //kh.DiaChiKH = row["DiaChiKH"].ToString();
            //kh.GioiTinhKH = row["GioiTinhKH"].ToString();
            kh.SDTKH = row["SDTKH"].ToString();
            //kh.NgaySinhKH = DateTime.Parse(row["NgaySinhKH"].ToString());
            //kh.MaTK = row["MaTK"].ToString();

            connsql.Close();
        }
    }
}
