using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Nhom8
{
    class DS_TaiKhoan
    {
        public DS_TaiKhoan()
        {
        }
        SqlCommand sqlcommand;
        SqlDataReader datareader;
        public List<TaiKhoan> TAIKHOAN(string query)
        {
            List<TaiKhoan> taikhoans = new List<TaiKhoan>();
            var conn = new KetNoi();

            using (SqlConnection sqlcon =  conn.connect)
            {
                sqlcon.Open();
                sqlcommand = new SqlCommand(query, sqlcon);
                datareader = sqlcommand.ExecuteReader();
                while(datareader.Read())
                {
                    taikhoans.Add(new TaiKhoan(datareader.GetString(2), datareader.GetString(3)));
                }
                sqlcon.Close();
            }
            return taikhoans;
        }
    }
}
