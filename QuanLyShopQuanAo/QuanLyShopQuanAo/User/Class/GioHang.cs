using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyShopQuanAo.ObjectClass;

namespace QuanLyShopQuanAo.User.Class
{
    public class GioHang : SanPham
    {
        //public string MaSP { get; set; }
        //public string HinhSP { get; set; }
        //public string TenSP { get; set; }
        //public double GiaBan { get; set; }
        public string KichCo { get; set; }
        public int SoLuong { get; set; }
        //public int SoLuongTon { get; set; }
        public double ThanhTien { get; set; }

        public GioHang()
        {
        }
        public GioHang(SanPham sp, string kichCo, int soLuong)
        {
            MaSP = sp.MaSP;
            TenSP = sp.TenSP;
            KichCo = kichCo;
            SoLuong = soLuong;
            GiaBan = sp.GiaBan;
            SoLuongTon = sp.SoLuongTon;
            ThanhTien = SoLuong * GiaBan;
        }

    }
}
