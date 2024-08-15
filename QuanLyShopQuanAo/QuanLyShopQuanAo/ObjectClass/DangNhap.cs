using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyShopQuanAo.ObjectClass
{
    public class DangNhap
    {
        Connect cn = new Connect();
        SqlConnection connsql;
        //public string MaTK { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        //public string LoaiTK { get; set; }

        public DangNhap()
        {
            connsql = cn.connect;
        }

        public DangNhap(string tk, string mk)
        {
            connsql = cn.connect;
            TaiKhoan = tk;
            MatKhau = mk;
            //LoaiTK = "user";
        }

        //-1: tài khoản rỗng
        //0: Tài khoản không tồn tại
        //1: tài khoản của admin
        //2: tài khoản của user
        //3: tài khoản đúng nhưng sai mật khẩu
        public int kiemTraTaiKhoan()
        {
            if (this.TaiKhoan != null && this.MatKhau != null)
            {
                    string selectStr = string.Format("select * from DANGNHAP where TAIKHOAN = '{0}' and MATKHAU = '{1}'", this.TaiKhoan, this.MatKhau);
                    connsql.Open();
                    SqlCommand cmd = new SqlCommand(selectStr, connsql);
                    DataTable dtTable = new DataTable();
                    dtTable.Load(cmd.ExecuteReader());
                    if (dtTable.Rows.Count > 0)
                    {
                        DataRow row = dtTable.Rows[0];
                        string strLoai = row["LOAITK"].ToString();
                        connsql.Close();
                        if (strLoai == "admin")
                            return 1;
                        else return 2;
                    }
                    selectStr = string.Format("select * from DANGNHAP where TAIKHOAN = '{0}'", this.TaiKhoan);
                    cmd = new SqlCommand(selectStr, connsql);
                    dtTable.Load(cmd.ExecuteReader());
                    if (dtTable.Rows.Count > 0)
                    {
                        connsql.Close();
                        return 3;
                    }
                    else
                    {
                        connsql.Close();
                        return 0;
                    }
            }
            else return -1;
        }
    }
}
