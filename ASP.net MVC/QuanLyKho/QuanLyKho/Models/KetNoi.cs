using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyKho.Models
{
    public class KetNoi
    {
        private string connectionString = @"Data Source=POLIETTA\SQLEXPRESS;Initial Catalog=QuanLyKho_HongLam; Connection Timeout=80";

        public DataTable ketnoi(string bang, string thuoctinh, int dieukien1)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + bang + " WHERE " + thuoctinh + " = @DieuKien1", connect);
                    cmd.Parameters.AddWithValue("@DieuKien1", dieukien1);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(resultTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi trong quá trình kết nối và truy vấn cơ sở dữ liệu: " + ex.Message);
                }
            }

            return resultTable;
        }

        public DataTable ketnoi(string bang, string thuoctinh1, string thuoctinh2, string thuoctinh3, string thuoctinh4, int dieukien1, int dieukien2, DateTime? dieukien3, DateTime? dieukien4)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connect;

                    if (dieukien3 == null)
                    {
                        if (dieukien4 == null)
                        {
                            cmd.CommandText = "SELECT * FROM " + bang + " WHERE " + thuoctinh1 + " = @DieuKien1 and " + thuoctinh2 + " = @DieuKien2 and nsx IS NULL and hsd IS NULL";
                            cmd.Parameters.AddWithValue("@DieuKien1", dieukien1);
                            cmd.Parameters.AddWithValue("@DieuKien2", dieukien2);
                        }
                        else
                        {
                            string hsdFormatted = ((DateTime)dieukien4).ToString("yyyy-MM-dd");
                            cmd.CommandText = "SELECT * FROM " + bang + " WHERE " + thuoctinh1 + " = @DieuKien1 and " + thuoctinh2 + " = @DieuKien2 and nsx IS NULL and " + thuoctinh4 + " = @DieuKien4";
                            cmd.Parameters.AddWithValue("@DieuKien1", dieukien1);
                            cmd.Parameters.AddWithValue("@DieuKien2", dieukien2);
                            cmd.Parameters.AddWithValue("@DieuKien4", hsdFormatted);
                        }
                    }
                    else
                    {
                        string nsxFormatted = ((DateTime)dieukien3).ToString("yyyy-MM-dd");
                        if (dieukien4 == null)
                        {
                            cmd.CommandText = "SELECT * FROM " + bang + " WHERE " + thuoctinh1 + " = @DieuKien1 and " + thuoctinh2 + " = @DieuKien2 and " + thuoctinh3 + " = @DieuKien3 and hsd IS NULL";
                            cmd.Parameters.AddWithValue("@DieuKien1", dieukien1);
                            cmd.Parameters.AddWithValue("@DieuKien2", dieukien2);
                            cmd.Parameters.AddWithValue("@DieuKien3", nsxFormatted);
                        }
                        else
                        {
                            string hsdFormatted = ((DateTime)dieukien4).ToString("yyyy-MM-dd");
                            cmd.CommandText = "SELECT * FROM " + bang + " WHERE " + thuoctinh1 + " = @DieuKien1 and " + thuoctinh2 + " = @DieuKien2 and " + thuoctinh3 + " = @DieuKien3 and " + thuoctinh4 + " = @DieuKien4";
                            cmd.Parameters.AddWithValue("@DieuKien1", dieukien1);
                            cmd.Parameters.AddWithValue("@DieuKien2", dieukien2);
                            cmd.Parameters.AddWithValue("@DieuKien3", nsxFormatted);
                            cmd.Parameters.AddWithValue("@DieuKien4", hsdFormatted);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(resultTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi trong quá trình kết nối và truy vấn cơ sở dữ liệu: " + ex.Message);
                }
            }

            return resultTable;
        }
    }
}