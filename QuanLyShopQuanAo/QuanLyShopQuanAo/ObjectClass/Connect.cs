using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyShopQuanAo.ObjectClass
{
    class Connect
    {
        public SqlConnection connect;
        public Connect()
        {
            connect = new SqlConnection("Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=QL_ShopQuanAo;Integrated Security=True");
        }

        public Connect(string strCon)
        {
            connect = new SqlConnection(strCon);
        }
    }
}
