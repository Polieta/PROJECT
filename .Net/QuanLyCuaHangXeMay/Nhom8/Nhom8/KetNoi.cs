using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Nhom8
{
   public class KetNoi
    {
        public SqlConnection connect;
        public KetNoi()
        {
            connect = new SqlConnection(@"Data Source=POLIETTA\SQLEXPRESS;Initial Catalog=QL_XeMay;Integrated Security=True");
        }
        public KetNoi(string strcn)
        {
            connect = new SqlConnection(strcn);
        }
    }
}
