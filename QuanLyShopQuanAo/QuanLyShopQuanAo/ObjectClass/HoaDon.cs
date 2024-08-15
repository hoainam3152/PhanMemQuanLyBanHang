using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QuanLyShopQuanAo.ObjectClass;

namespace QuanLyShopQuanAo.ObjectClass
{
    class HoaDon
    {
        public int MaHD { get; set; }
        public string MaNV { get; set; }
        public int MaKH { get; set; }
        public DateTime NgayBan { get; set; }
        //public string HoTenNguoiNhan { get; set; }
        //public string SDTNguoiNhan { get; set; }
        //public string DiaChi { get; set; }
        public double TongThanhToan { get; set; }
        public string TrangThai { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string GhiChu { get; set; }

        //////////////////////////////////////////
        Connect cn = new Connect();
        SqlConnection connsql;
        public void themDuLieuVaoSQL(HoaDon hd)
        {
            connsql = cn.connect;

        }
    }
}
