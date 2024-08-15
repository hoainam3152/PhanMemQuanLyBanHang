using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyShopQuanAo.ObjectClass
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string HinhSP { get; set; }
        public double GiaBan { get; set; }
        public string GioiTinh { get; set; }
        public string ThongTinSP { get; set; }
        public string ChatLieu { get; set; }
        public string Form { get; set; }
        public int SoLuongTon { get; set; }
        public int DaBan { get; set; }
        public string TinhTrang { get; set; }
        public string MaL { get; set; }

        Connect cn = new Connect();
        SqlConnection connsql;
        //Cách 2: lấy dữ liệu từ form khác
        //public DataSet layDuLieu()
        //{
        //    DataSet dt = new DataSet();
        //    return dt;
        //}

        public SanPham()
        {
            connsql = cn.connect;
        }

        public void layThongTinSanPham(string maSP)
        {
            DataSet ttsp = new DataSet();
            string selectStr = string.Format("Select * from SANPHAM where MaSP = '{0}'", maSP.Trim());;
            SqlDataAdapter data = new SqlDataAdapter(selectStr, connsql);
            data.Fill(ttsp, "TTSP");
            //connsql.Open();
            //string selectString = string.Format("select * from SanPham where MaSP = '{0}'", MaSP.Trim());
            //SqlCommand cmd = new SqlCommand(selectString, connsql);
            //SqlDataReader rd = cmd.ExecuteReader();
            DataRow rd = ttsp.Tables["TTSP"].Rows[0];
            MaSP = rd["MaSP"].ToString();
            TenSP = rd["TenSP"].ToString();
            HinhSP = rd["HinhSP"].ToString();
            GiaBan = double.Parse(rd["GiaBan"].ToString());
            GioiTinh = rd["GioiTinh"].ToString();
            ThongTinSP = rd["ThongTinSP"].ToString();
            ChatLieu = rd["ChatLieu"].ToString();
            Form = rd["Form"].ToString();
            SoLuongTon = int.Parse(rd["SoLuongTon"].ToString());
            DaBan = int.Parse(rd["DaBan"].ToString());
            TinhTrang = rd["TinhTrang"].ToString();
            MaL = rd["MaL"].ToString();
            //connsql.Close();
        }
    }
}
