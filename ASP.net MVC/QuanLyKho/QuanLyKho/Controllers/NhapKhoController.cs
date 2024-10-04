using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;
using System.Data.Entity.Infrastructure;
using Lib_QRcode;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Net;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using OfficeOpenXml;

namespace QuanLyKho.Controllers
{
    public class NhapKhoController : Controller
    {
        QuanLyKho_HongLamEntities db = new QuanLyKho_HongLamEntities();
        private ActionResult TaoTem(tbltaotem nh)
        {
            var KiemTra = db.tbltaotem.FirstOrDefault(k => k.lot == nh.lot && k.ma == nh.ma && k.nsx == nh.nsx && k.hsd == nh.hsd);
            if (KiemTra == null)
            {
                db.tbltaotem.Add(nh);
            }
            nh.userid = Convert.ToInt32(Session["idUser"]);
            nh.hide = 0;
            nh.ngayud = DateTime.Now.Date;
            //đổi tên loại hàng, ncc
            var lh = db.tblloaihang.FirstOrDefault(t => t.id == nh.loaihang);
            nh.loaihang = lh.id;
            var ncc = db.tblnhacc.FirstOrDefault(t => t.id == nh.nhacc);//FIX later
            nh.nhacc = ncc.id;
            nh.idcongty = Convert.ToInt32(Session["Congty"]);
            string s_chuoi = "Mã hàng: " + nh.ma.ToString() + " || Lô SX: " + nh.lot.ToString() + " ||  NSX: " + nh.nsx.ToString() + " ||  HSD: " + nh.hsd.ToString() + " ||  Loại hàng: " + lh.ten.ToString() + " ||  Nhà CC: " + ncc.ten.ToString();
            MemoryStream qrCodeResult = renderQRCode(s_chuoi);
            byte[] qrCodeImageBytes = ImageToByteArray(qrCodeResult);
            string directoryPath = "~/Images/";
            // Tạo tên file cho ảnh QR code (ví dụ: mã hàng + timestamp)
            string fileName = "Mã_" + nh.ma.ToString() + "_QR_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".png";
            // Đường dẫn hoàn chỉnh tới thư mục chứa tệp ảnh QR code
            string filePath = Path.Combine(Server.MapPath(directoryPath), fileName);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(Server.MapPath(directoryPath)))
            {
                Directory.CreateDirectory(Server.MapPath(directoryPath));
            }
            // Tạo và lưu ảnh QR code vào đường dẫn tương đối
            using (MemoryStream ms = new MemoryStream(renderQRCode(s_chuoi).ToArray()))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    ms.CopyTo(fs);
                }
            }
            // Lưu đường dẫn tới ảnh QR code vào trường nh.Image trong cơ sở dữ liệu
            nh.Image = qrCodeImageBytes; // Lưu đường dẫn đầy đủ tới tệp ảnh QR code trên máy tính cá nhân vào trường nh.Image
            nh.ImageName = fileName; // Lưu tên file chứa mã QR
            return null;
        }
        private MemoryStream renderQRCode(string noidung)
        {
            //QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)cbLevel.SelectedIndex;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(noidung, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            int cellSize = 3; // Kích thước mỗi ô (cell) của mã QR code
            int margin = 0; // Khoảng cách lề
            // Tạo hình ảnh từ mã QR code
            Bitmap qrCodeImage = qrCode.GetGraphic(cellSize, Color.Black, Color.White, null, margin, 2);
            // Tạo một MemoryStream để lưu ảnh dưới dạng JPEG để giảm dung lượng
            MemoryStream stream = new MemoryStream();
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 70L); // Thiết lập chất lượng nén ảnh
            ImageCodecInfo jpgEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
            qrCodeImage.Save(stream, jpgEncoder, encoderParams);
            stream.Position = 0; // Đặt lại vị trí của stream về đầu để đọc từ đầu
            return stream;
        }
        private ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public byte[] ImageToByteArray(MemoryStream memoryStream)
        {
            return memoryStream.ToArray();
        }
        public ActionResult XuLyMaQR(string qrData)
        {
            string[] separatedInfo = qrData.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> extractedData = new Dictionary<string, string>();
            foreach (var info in separatedInfo)
            {
                string[] keyValue = info.Split(':');
                if (keyValue.Length == 2 || keyValue.Length == 4)
                {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();
                    if (key == "NSX" || key == "HSD")
                    {
                        for (int i = 2; i < keyValue.Length; i++)
                        {
                            value += ":" + keyValue[i].Trim();
                        }
                    }
                    extractedData.Add(key, value);
                }
            }
            // Lấy thông tin cụ thể từ Dictionary
            string maHang = extractedData.ContainsKey("Mã hàng") ? extractedData["Mã hàng"] : "";
            string loSX = extractedData.ContainsKey("Lô SX") ? extractedData["Lô SX"] : "";
            string nsx = extractedData.ContainsKey("NSX") ? extractedData["NSX"] : "";
            string hsd = extractedData.ContainsKey("HSD") ? extractedData["HSD"] : "";
            string loaiHang = extractedData.ContainsKey("Loại hàng") ? extractedData["Loại hàng"] : "";
            //string nhaCC = extractedData.ContainsKey("Nhà CC") ? extractedData["Nhà CC"] : "";
            // Xử lý dữ liệu nhận được
            tblkiemkeCTkho tem = new tblkiemkeCTkho();
            tem.mahang = Convert.ToInt32(maHang);
            tem.solo = loSX;
            tem.HSD = Convert.ToDateTime(hsd);
            tem.sl = 1;
            tem.nsx = Convert.ToDateTime(nsx);
            var lh = db.tblloaihang.FirstOrDefault(t => t.ten == loaiHang);
            tem.loaihang = lh.id;
            // var ncc = db.tblnhacc.FirstOrDefault(t => t.ten == nhaCC);
            //tem.n = ncc.id;
            tem.ngayud = DateTime.Now.Date;
            var existingItem = db.tblkiemkeCTkho.FirstOrDefault(item => item.mahang == tem.mahang);
            var existingItem2 = db.tblkiemkeCTkho.FirstOrDefault(item => item.solo == tem.solo);
            var existingItem3 = db.tblkiemkeCTkho.FirstOrDefault(item => item.loaihang == tem.loaihang);
            var existingItem4 = db.tblkiemkeCTkho.FirstOrDefault(item => item.HSD == tem.HSD);
            var existingItem5 = db.tblkiemkeCTkho.FirstOrDefault(item => item.nsx == tem.nsx);
            if (existingItem == null || existingItem2 == null || existingItem3 == null || existingItem4 == null || existingItem5 == null)
            {
                TempData["temData"] = tem;
                return RedirectToAction("ChiTietKiemKeKho", "NhapKho");
            }
            return RedirectToAction("ScanQR", "BaoCao");
        }
        public ActionResult ChiTietKiemKeKho()
        {
            tblkiemkeCTkho KiemKeKhoChitiet = (tblkiemkeCTkho)TempData["temData"];
            KiemKeKhoChitiet.id = Convert.ToDecimal(Session["idKiemKeKho"]);
            tblkiemkeCTkho tam = db.tblkiemkeCTkho.FirstOrDefault(item => item.id == KiemKeKhoChitiet.id);
            KiemKeKhoChitiet.ngayud = DateTime.Now.Date;
            var existingItem = db.tblkiemkeCTkho.FirstOrDefault(item => item.id == KiemKeKhoChitiet.id);
            var existingItem2 = db.tblkiemkeCTkho.FirstOrDefault(item => item.mahang == KiemKeKhoChitiet.mahang);
            var existingItem3 = db.tblkiemkeCTkho.FirstOrDefault(item => item.solo == KiemKeKhoChitiet.solo);
            var existingItem4 = db.tblkiemkeCTkho.FirstOrDefault(item => item.HSD == KiemKeKhoChitiet.HSD);
            var existingItem5 = db.tblkiemkeCTkho.FirstOrDefault(item => item.sl == KiemKeKhoChitiet.sl);
            var existingItem6 = db.tblkiemkeCTkho.FirstOrDefault(item => item.nsx == KiemKeKhoChitiet.nsx);
            var existingItem7 = db.tblkiemkeCTkho.FirstOrDefault(item => item.loaihang == KiemKeKhoChitiet.loaihang);
            var existingItem8 = db.tblkiemkeCTkho.FirstOrDefault(item => item.ngayud == KiemKeKhoChitiet.ngayud);
            int IDKHO2 = Convert.ToInt32(Session["idKho"]);
            var mahang2 = db.tbltonkhoth.FirstOrDefault(item => item.mahanghoa == KiemKeKhoChitiet.mahang && item.idkho == IDKHO2);
            int solo = Convert.ToInt32(KiemKeKhoChitiet.solo);
            if (existingItem != null && existingItem2 != null && existingItem3 != null && existingItem4 != null && existingItem5 != null && existingItem6 != null && existingItem7 != null && existingItem8 != null)
            {
                tam.sttton = db.tblkiemkeCTkho.Where(n => n.id == KiemKeKhoChitiet.id && n.mahang == KiemKeKhoChitiet.mahang && n.solo == KiemKeKhoChitiet.solo && n.nsx == KiemKeKhoChitiet.nsx && n.HSD == KiemKeKhoChitiet.HSD && n.loaihang == KiemKeKhoChitiet.loaihang).Max(n => n.sttton) + Convert.ToInt32(KiemKeKhoChitiet.sl);
            }

            else
            {
                int tondau = 0;
                KiemKeKhoChitiet.sttton = Convert.ToInt32(KiemKeKhoChitiet.sl);
                db.tblkiemkeCTkho.Add(KiemKeKhoChitiet);
                db.SaveChanges();
                var idkho = db.tbltonkhoth.FirstOrDefault(item => item.idkho == IDKHO2);
                var mahang = db.tbltonkhoth.FirstOrDefault(item => item.mahanghoa == KiemKeKhoChitiet.mahang);
                if (idkho == null)
                {
                    var IDkho = new tbltonkhoth();
                    IDkho.idkho = IDKHO2;
                    IDkho.mahanghoa = KiemKeKhoChitiet.mahang;
                    IDkho.tondau = KiemKeKhoChitiet.sttton;
                    tondau = Convert.ToInt32(IDkho.tondau);
                    IDkho.ngayud = DateTime.Now.Date;
                    IDkho.idcongty = Convert.ToInt32(Session["Congty"]);
                    db.tbltonkhoth.Add(IDkho);
                    db.SaveChanges();
                }
                else
                {
                    if (mahang == null)
                    {
                        var khoth = new tbltonkhoth();
                        khoth.idkho = Convert.ToInt32(Session["idKho"]);
                        khoth.mahanghoa = KiemKeKhoChitiet.mahang;
                        khoth.tondau = KiemKeKhoChitiet.sttton;
                        tondau = Convert.ToInt32(khoth.tondau);
                        khoth.ngayud = DateTime.Now.Date;
                        khoth.idcongty = Convert.ToInt32(Session["Congty"]);
                        db.tbltonkhoth.Add(khoth);
                        db.SaveChanges();
                    }
                    else
                    {
                        var tonkho = db.tbltonkhoth.FirstOrDefault(item => item.mahanghoa == KiemKeKhoChitiet.mahang);
                        tonkho.tondau = db.tbltonkhoth.Where(n => n.idkho == IDKHO2).Max(n => n.tondau) + KiemKeKhoChitiet.sl;
                        tondau = Convert.ToInt32(KiemKeKhoChitiet.sl);
                        db.SaveChanges();
                    }
                }
                CapNhatTonKho_Chi_Tiet(KiemKeKhoChitiet.id, KiemKeKhoChitiet.mahang, solo, tondau);
                TempData["NhValues"] = KiemKeKhoChitiet;
                return RedirectToAction("ThemKiemKeKho_DuLieu_Nhap_Tay", "BaoCao");
            }

            if (mahang2 == null)
            {
                var khoth = new tbltonkhoth();
                khoth.idkho = Convert.ToInt32(Session["idKho"]);
                khoth.mahanghoa = KiemKeKhoChitiet.mahang;
                khoth.tondau = KiemKeKhoChitiet.sttton;
                khoth.ngayud = DateTime.Now.Date;
                khoth.idcongty = Convert.ToInt32(Session["Congty"]);
                db.tbltonkhoth.Add(khoth);
                db.SaveChanges();
            }
            else
            {
                var idKho = (int)Session["idKho"];
                var tonkho = db.tbltonkhoth.FirstOrDefault(item => item.mahanghoa == KiemKeKhoChitiet.mahang);
                var maxTondau = db.tbltonkhoth
                    .Where(n => n.idkho == idKho && n.mahanghoa == KiemKeKhoChitiet.mahang)
                    .Max(n => (int?)n.tondau);
                tonkho.tondau = maxTondau.GetValueOrDefault() + KiemKeKhoChitiet.sl;
                db.SaveChanges();
            }
            int tondau2 = Convert.ToInt32(KiemKeKhoChitiet.sl);
            CapNhatTonKho_Chi_Tiet(KiemKeKhoChitiet.id, KiemKeKhoChitiet.mahang, solo, tondau2);
            TempData["NhValues"] = KiemKeKhoChitiet;
            return RedirectToAction("ThemKiemKeKho_DuLieu_Nhap_Tay", "BaoCao");
        }
        private void CapNhatTonKho_Chi_Tiet(decimal ID, int mahanghoa, int solo, int tonkho)
        {
            try
            {
                var ctkho = db.tblkiemkeCTkho.FirstOrDefault(item => item.id == ID && item.mahang == mahanghoa);
                var tkkhoct = new tbltonkhoct();
                if (db.tbltonkhoct.Count() == 0)
                {
                    tkkhoct.id = 0;
                }
                else
                {
                    tkkhoct.id = db.tbltonkhoct.Max(n => n.id) + 1;
                }
                tkkhoct.idnguoikk = ctkho.id;
                tkkhoct.idkho = Convert.ToInt32(Session["idKho"]);
                tkkhoct.mahanghoa = ctkho.mahang;
                tkkhoct.solo = solo;
                tkkhoct.tondau = tonkho;
                tkkhoct.hsd = ctkho.HSD;
                tkkhoct.ngayud = DateTime.Now.Date;
                tkkhoct.idcongty = Convert.ToInt32(Session["Congty"]);
                db.tbltonkhoct.Add(tkkhoct);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
            }

        }


        public ActionResult Index(DateTime? ngay)
        {
            DateTime ngayChon = ngay ?? DateTime.Today;
            var danhSachPhieu = db.tblphieunhapkho.Where(p => p.ngayud == ngayChon).ToList();
            ViewBag.hanghoa = new SelectList(db.tblhanghoa.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.kho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            if (danhSachPhieu != null)
            {
                var jsonData = new
                {
                    DanhSachPhieu = danhSachPhieu.Select(item => new
                    {
                        IDPhieu = item.id.ToString(),
                        MaPhieu = item.ma,
                        NgayLap = item.ngayud.ToString()
                    }),
                    TongSoPhieu = danhSachPhieu.Count
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { TongSoPhieu = 0, DanhSachPhieu = new List<object>() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult NhapKho()
        {
            ViewBag.hanghoa = new SelectList(db.tblhanghoa.ToList().OrderBy(n => n.id), "id", "ma");
            ViewBag.kho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        [HttpPost]
        public ActionResult LuuDuLieu(string maPhieu, string nguoiNhap, string nguoiGiao, string diaChi, string soThue, string dienGiai, string kho)
        {
            try
            {
                // Lưu thông tin phiếu nhập kho vào cơ sở dữ liệu
                tblphieunhapkho nhapkho = new tblphieunhapkho();
                nhapkho.id = GenerateDecimalId();
                nhapkho.idcongty = Convert.ToInt32(Session["Congty"]);
                nhapkho.userid = Convert.ToInt32(Session["idUser"]);
                nhapkho.ma = Convert.ToInt32(maPhieu);
                nhapkho.nguoinhapphieu = nguoiNhap;
                nhapkho.nguoigiaohang = nguoiGiao;
                nhapkho.diachi = diaChi;
                nhapkho.diengiai = dienGiai;
                nhapkho.masothue = soThue;
                nhapkho.ngayud = DateTime.Now.Date;
                nhapkho.idkho = Convert.ToInt32(kho);
                db.tblphieunhapkho.Add(nhapkho);
                // Lưu thông tin chi tiết phiếu nhập kho vào cơ sở dữ liệu
                int stt = 0;
                var dataTam = db.bangtamnhapkho.ToList();
                foreach (var item in dataTam)
                {
                    tblphieunhapkhoct chiTietPhieu = new tblphieunhapkhoct();
                    chiTietPhieu.id = nhapkho.id; // Lấy ID của phiếu nhập kho vừa tạo
                    chiTietPhieu.mahanghoa = item.mahanghoa;
                    chiTietPhieu.lot = Convert.ToString(item.solo);
                    chiTietPhieu.stt = stt + 1;
                    stt++;
                    chiTietPhieu.soluong = Convert.ToDecimal(item.soluong);
                    chiTietPhieu.hsd = item.hsd;
                    chiTietPhieu.nsx = item.nsx;
                    chiTietPhieu.ngayud = nhapkho.ngayud;
                    chiTietPhieu.idcongty = nhapkho.idcongty;
                    chiTietPhieu.maphieu = nhapkho.ma;
                    db.tblphieunhapkhoct.Add(chiTietPhieu);

                    tbltonkhoct tonkhoct = new tbltonkhoct();
                    tonkhoct.id = chiTietPhieu.id;
                    tonkhoct.idkho = nhapkho.idkho;
                    tonkhoct.idn = chiTietPhieu.id;
                    tonkhoct.idx = null;
                    tonkhoct.mahanghoa = chiTietPhieu.mahanghoa;
                    tonkhoct.solo = Convert.ToInt32(chiTietPhieu.lot);
                    tonkhoct.hsd = chiTietPhieu.hsd;
                    tonkhoct.tondau = 0;
                    tonkhoct.sln = chiTietPhieu.soluong;
                    tonkhoct.slx = 0;
                    tonkhoct.ngayud = nhapkho.ngayud;
                    tonkhoct.idcongty = nhapkho.idcongty;
                    tonkhoct.idnguoikk = Convert.ToInt32(Session["idUser"]);
                    db.tbltonkhoct.Add(tonkhoct);
                    db.SaveChanges();
                }
                db.SaveChanges();
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult LuuDuLieuTam(string maphieu, string mahang, string solo, string soluong, string nsx, string hsd)
        {
            try
            {
                int maPhieuInt = int.Parse(maphieu);
                Session["maphieu"] = maPhieuInt;
                int maHangInt = int.Parse(mahang);
                //var ktHH = db.tblhanghoa.FirstOrDefault(item => item.id == maHangInt);
                //if (ktHH != null) 
                //{
                //    maHangInt = ktHH.id;
                //}
                int soLoInt = int.Parse(solo);
                int soLuongInt = int.Parse(soluong);
                DateTime? nsxDateTime = string.IsNullOrEmpty(nsx) ? (DateTime?)null : Convert.ToDateTime(nsx);
                DateTime? hsdDateTime = string.IsNullOrEmpty(hsd) ? (DateTime?)null : Convert.ToDateTime(hsd);
                var existingItem = db.bangtamnhapkho.FirstOrDefault(item => item.maphieu == maPhieuInt && item.solo == soLoInt && item.mahanghoa == maHangInt && item.nsx == nsxDateTime && item.hsd == hsdDateTime);
                if (existingItem != null)
                {
                    existingItem.soluong += soLuongInt;
                    db.Entry(existingItem).State = EntityState.Modified;
                }
                else
                {
                    bangtamnhapkho CT_nhapkhotam = new bangtamnhapkho();
                    var maxStt = db.bangtamnhapkho.Any() ? db.bangtamnhapkho.Max(item => item.stt) : 0;
                    int sttValue = maxStt + 1;
                    CT_nhapkhotam.stt = sttValue;
                    CT_nhapkhotam.maphieu = maPhieuInt;
                    CT_nhapkhotam.mahanghoa = maHangInt;
                    CT_nhapkhotam.tenhanghoa = LayTen_TEN_HangHoa(maHangInt);
                    CT_nhapkhotam.solo = soLoInt;
                    CT_nhapkhotam.soluong = soLuongInt;
                    CT_nhapkhotam.nsx = nsxDateTime;
                    CT_nhapkhotam.hsd = hsdDateTime;
                    db.bangtamnhapkho.Add(CT_nhapkhotam);
                    //In ma QR cua hang hoa
                    tbltaotem a = new tbltaotem();
                    tblhanghoa b = new tblhanghoa();
                    a.ma = CT_nhapkhotam.mahanghoa;
                    a.lot = Convert.ToString(CT_nhapkhotam.solo);
                    a.loaihang = db.tblhanghoa.FirstOrDefault(t => t.id == a.ma).loaihang;
                    a.nhacc = 1;//Chuưa coó max nhaà cung caâấp
                    a.nsx = CT_nhapkhotam.nsx;
                    a.hsd = CT_nhapkhotam.hsd;
                    TaoTem(a);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private string LayTen_TEN_HangHoa(int idHangHoa)
        {
            var tenHangHoa = db.tblhanghoa.Where(h => h.id == idHangHoa).Select(h => h.ten).FirstOrDefault();
            return tenHangHoa; // Trả về tên hàng hóa
        }
        [HttpGet]
        public ActionResult LayDuLieuTam()
        {
            try
            {
                int a = Convert.ToInt32(Session["maphieu"]);
                if (a != 0)
                {
                    var data = db.bangtamnhapkho.Where(t => t.maphieu == a).ToList();
                    var jsonData = data.Select(item => new
                    {
                        maHangHoa = item.mahanghoa,
                        tenHangHoa = item.tenhanghoa,
                        soLo = item.solo,
                        soLuong = item.soluong,
                        ngaySanXuat = item.nsx != null ? item.nsx.ToString() : null,
                        hanSuDung = item.hsd != null ? item.hsd.ToString() : null
                    });
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }

                var data1 = db.bangtamnhapkho.ToList();
                if (data1 == null || data1.Count == 0)
                {
                    return Json(new List<object>(), JsonRequestBehavior.AllowGet);
                }
                var jsonData1 = data1.Select(item => new
                {
                    maHangHoa = item.mahanghoa,
                    tenHangHoa = item.tenhanghoa,
                    soLo = item.solo,
                    soLuong = item.soluong,
                    ngaySanXuat = item.nsx != null ? item.nsx.ToString() : null,
                    hanSuDung = item.hsd != null ? item.hsd.ToString() : null
                });
                return Json(jsonData1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        private int LayTenHangHoa(string tenhh)
        {
            var idhh = db.tblhanghoa.Where(h => h.ten == tenhh).Select(h => h.id).FirstOrDefault();
            return idhh; // Trả về id hàng hóa từ tên
        }
        public ActionResult XoaMatHang(string maHangHoa, string soLo)
        {
            int mahanghoa = LayTenHangHoa(maHangHoa);
            int solo = Convert.ToInt32(soLo);
            var hangHoa = db.bangtamnhapkho.FirstOrDefault(h => h.mahanghoa == mahanghoa && h.solo == solo);
            if (hangHoa != null)
            {
                db.bangtamnhapkho.Remove(hangHoa);
                db.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public ActionResult InTem(string maHangHoa, string soLo, string nSx, string hSd)
        {
            DateTime? nsx = null;
            DateTime? hsd = null;
            if (!string.IsNullOrEmpty(nSx))
            {
                nsx = Convert.ToDateTime(nSx);
            }
            if (!string.IsNullOrEmpty(hSd))
            {
                hsd = Convert.ToDateTime(hSd);
            }
            int ma = Convert.ToInt32(maHangHoa);
            int solo = Convert.ToInt32(soLo);

            ViewReport a = new ViewReport();
            a.S_table = "tbltaotem";
            a.S_tenreport = "rpt_intemdan";
            a.S_thuoctinh1 = "ma";
            a.I_dieukien1 = ma;
            a.S_thuoctinh2 = "lot";
            a.I_dieukien2 = solo;
            a.S_thuoctinh3 = "nsx";
            a.I_dieukien3 = nsx;
            a.S_thuoctinh4 = "hsd";
            a.I_dieukien4 = hsd;
            Session["ViewReportInfo"] = a;
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult LuuSuaMatHang(string maHangHoa, string soLo, string soLuong, string ngaySanXuat, string hanSuDung)
        {
            try
            {
                if (ngaySanXuat == "null")
                {
                    ngaySanXuat = "";
                }
                if (hanSuDung == "null")
                {
                    hanSuDung = "";
                }
                int mahanghoa = LayTenHangHoa(maHangHoa);
                int solo = Convert.ToInt32(soLo);
                int soluong = Convert.ToInt32(soLuong);
                var matHang = db.bangtamnhapkho.SingleOrDefault(m => m.mahanghoa == mahanghoa && m.solo == solo);
                matHang.nsx = string.IsNullOrEmpty(ngaySanXuat) ? (DateTime?)null : Convert.ToDateTime(ngaySanXuat);
                matHang.hsd = string.IsNullOrEmpty(hanSuDung) ? (DateTime?)null : Convert.ToDateTime(hanSuDung);
                matHang.mahanghoa = mahanghoa;
                matHang.solo = solo;
                matHang.soluong = soluong;

                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult XoaDuLieu()
        {
            try
            {
                var maPhieu = Convert.ToInt32(Session["maphieu"]);
                if (maPhieu != 0)
                {
                    // Xây dựng câu lệnh SQL với điều kiện WHERE
                    string sql = "DELETE FROM bangtamnhapkho WHERE maphieu = @maPhieu";
                    // Thực thi câu lệnh SQL với tham số
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter("@maPhieu", maPhieu));
                    // Lưu các thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }
                else
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM bangtamnhapkho");
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public ActionResult TimTenHangHoa(string maHangHoa)
        {
            int id = Convert.ToInt32(maHangHoa);
            var tenHangHoa = db.tblhanghoa.Where(hh => hh.id == id).Select(hh => hh.ten).FirstOrDefault();
            if (!string.IsNullOrEmpty(tenHangHoa))
            {
                return Json(new { success = true, tenHangHoa = tenHangHoa }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult XemPhieu(string IDPhieu)
        {
            decimal id = Convert.ToDecimal(IDPhieu);
            var phieuNhapKho = db.tblphieunhapkho.FirstOrDefault(a => a.id == id);
            if (phieuNhapKho != null)
            {
                tblphieunhapkho phieu = new tblphieunhapkho
                {
                    ma = phieuNhapKho.ma,
                    nguoinhapphieu = phieuNhapKho.nguoinhapphieu,
                    nguoigiaohang = phieuNhapKho.nguoigiaohang,
                    diachi = phieuNhapKho.diachi,
                    masothue = phieuNhapKho.masothue,
                    diengiai = phieuNhapKho.diengiai,
                    idkho = phieuNhapKho.idkho
                };
                return Json(new { success = true, phieuNhapKho = phieu }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult XemDuLieuBang(string IDPhieu)
        {
            decimal id = Convert.ToDecimal(IDPhieu);
            var data = db.tblphieunhapkhoct.Where(t => t.id == id).ToList();
            var jsonData = data.Select(item => new
            {
                maPhieu = item.maphieu,
                maHangHoa = item.mahanghoa,
                tenHangHoa = LayTen_TEN_HangHoa(item.mahanghoa),
                soLo = item.lot,
                soLuong = item.soluong,
                ngaySanXuat = item.nsx != null ? item.nsx.ToString() : null,
                hanSuDung = item.hsd != null ? item.hsd.ToString() : null
            });
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NhapKho_DienThoai()
        {
            ViewBag.kho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        //public ActionResult InExcel(string IDPhieu)
        //{
        //    decimal id = Convert.ToDecimal(IDPhieu);
        //    var danhSachHangHoa = db.tblphieunhapkhoct.Where(t => t.id == id).ToList();
        //    var workbook = new SpreadsheetDocument();
        //    workbook.WorkbookPart.Workbook = new Workbook();
        //    var worksheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
        //    worksheetPart.Worksheet = new Worksheet(new SheetData());
        //    var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
        //    foreach (var hangHoa in danhSachHangHoa)
        //    {
        //        var row = new Row();
        //        row.Append(
        //            new Cell() { DataType = CellValues.String, CellValue = new CellValue(hangHoa.IDPhieu.ToString()) }

        //            // Thêm các cell khác tương ứng với các trường dữ liệu khác nếu cần
        //        );
        //        sheetData.AppendChild(row);
        //    }
        //    MemoryStream stream = new MemoryStream();
        //    workbook.SaveAs(stream);
        //    stream.Position = 0;
        //    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PhieuNhapKho.xlsx");
        //}

        public decimal GenerateDecimalId()
        {
            //sử dụng kết hợp của timestamp và một số ngẫu nhiên
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            Random rnd = new Random();
            int randomNumber = rnd.Next(100, 999); // Sinh một số ngẫu nhiên có 3 chữ số
            string ipPart = GetIPv4Address().Replace(".", ""); // Loại bỏ dấu chấm trong địa chỉ IP
            // Tính toán chiều dài cần cắt bớt để đạt được tổng độ dài 20
            int remainingLength = 20 - (timestamp.Length + randomNumber.ToString().Length);
            string ipPartTrimmed = ipPart.Length > remainingLength ? ipPart.Substring(0, remainingLength) : ipPart;
            string combinedId = timestamp + randomNumber.ToString() + ipPartTrimmed;
            // Chuyển đổi chuỗi kết hợp thành decimal
            decimal generatedId = decimal.Parse(combinedId);
            return generatedId;
        }
        private string GetIPv4Address()
        {
            string hostName = Dns.GetHostName(); // Lấy tên máy tính
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName); // Lấy địa chỉ IP từ tên máy tính

            foreach (IPAddress ipAddress in ipAddresses)
            {
                // Kiểm tra nếu là địa chỉ IPv4
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString(); // Trả về địa chỉ IPv4 dưới dạng chuỗi
                }
            }
            return string.Empty; // Trả về chuỗi rỗng nếu không tìm thấy địa chỉ IPv4
        }
    }
}