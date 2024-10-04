using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuanLyKho.Models;
using System.Data;
using System.IO;

namespace QuanLyKho
{
    public partial class ViewReport : System.Web.UI.Page
    {
        private string s_tenreport;
        public string S_tenreport
        {
            get { return s_tenreport; }
            set { s_tenreport = value; }
        }
        private string s_table;
        public string S_table
        {
            get { return s_table; }
            set { s_table = value; }
        }

        private string s_thuoctinh1;
        public string S_thuoctinh1
        {
            get { return s_thuoctinh1; }
            set { s_thuoctinh1 = value; }
        }
        private int i_dieukien1;
        public int I_dieukien1
        {
            get { return i_dieukien1; }
            set { i_dieukien1 = value; }
        }

        private string s_thuoctinh2;
        public string S_thuoctinh2
        {
            get { return s_thuoctinh2; }
            set { s_thuoctinh2 = value; }
        }
        private int i_dieukien2;
        public int I_dieukien2
        {
            get { return i_dieukien2; }
            set { i_dieukien2 = value; }
        }

        private string s_thuoctinh3;
        public string S_thuoctinh3
        {
            get { return s_thuoctinh3; }
            set { s_thuoctinh3 = value; }
        }
        private DateTime? i_dieukien3;
        public DateTime? I_dieukien3
        {
            get { return i_dieukien3; }
            set { i_dieukien3 = value; }
        }

        private string s_thuoctinh4;
        public string S_thuoctinh4
        {
            get { return s_thuoctinh4; }
            set { s_thuoctinh4 = value; }
        }
        private DateTime? i_dieukien4;
        public DateTime? I_dieukien4
        {
            get { return i_dieukien4; }
            set { i_dieukien4 = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ViewReportInfo"] != null)
                {
                    ViewReport viewReport = (ViewReport)Session["ViewReportInfo"];
                    ReportDocument rptDoc = new ReportDocument();
                    //DataSet ds = hsmf();
                    ds_intemdan ds = new ds_intemdan();
                    // tập tin .xsd
                    DataTable dt = new DataTable();
                    // Đặt tên cho datatable
                    dt.TableName = "Crystal Report";
                    //Kết nối SQL
                    KetNoi KetNoi = new KetNoi();
                    //, , v, 
                    dt = KetNoi.ketnoi(viewReport.S_table, viewReport.S_thuoctinh1, viewReport.S_thuoctinh2, viewReport.S_thuoctinh3, viewReport.S_thuoctinh4, viewReport.I_dieukien1, viewReport.I_dieukien2, viewReport.I_dieukien3, viewReport.I_dieukien4);
                    //Gọi hàm getAllGenres
                    ds.Tables[0].Merge(dt);
                    string directoryPath = "~/XML/";
                    // Tạo tên file cho ảnh QR code (ví dụ: mã hàng + timestamp)
                    string fileName = viewReport.S_tenreport + ".xml";
                    // Đường dẫn hoàn chỉnh tới thư mục chứa tệp ảnh QR code
                    string filePath = Path.Combine(Server.MapPath(directoryPath), fileName);
                    ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
                    rptDoc.Load(Server.MapPath("~/Report/" + viewReport.S_tenreport + ".rpt"));
                    //gán dataset đến report view
                    rptDoc.SetDataSource(ds);
                    CrystalReportViewer.ReportSource = rptDoc;
                    CrystalReportViewer.DisplayToolbar = true;
                }
            }
        }
    }
}