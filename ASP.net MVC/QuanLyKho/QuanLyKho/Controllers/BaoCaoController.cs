using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using QuanLyKho.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Drawing;
using Tesseract;
using System.Text.RegularExpressions;
namespace QuanLyKho.Controllers
{
    public class BaoCaoController : Controller
    {
        //
        // GET: /BaoCao/
        QuanLyKho_HongLamEntities db = new QuanLyKho_HongLamEntities();
        [HttpGet]
        public ActionResult KiemKeKho(int? size, int? page, string searchstring)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "25", Value = "25" }
            };
            foreach (var item in items)
            {
                if (item.Value == size.ToString())
                {
                    item.Selected = true;
                }
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            var links = db.tblkiemkekho.OrderBy(x => x.id).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.nguoikiemke.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemKiemKeKho()
        {
            ViewBag.kiemkekho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        [HttpPost]
        public ActionResult ThemKiemKeKho(tblkiemkekho nh)
        {
            if (ModelState.IsValid)
            {
                ThongTinKiemKe(nh);
                //return RedirectToAction("ScanQR");
                return RedirectToAction("ScanQR");
            }
            return View(nh);
        }
        [HttpGet]
        public ActionResult ThemKiemKeKho_DuLieu()
        {
            ViewBag.kiemkekho = new SelectList(db.tblkho.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        [HttpPost]
        public ActionResult ThemKiemKeKho_DuLieu(tblkiemkekho nh)
        {
            if (ModelState.IsValid)
            {
                ThongTinKiemKe(nh);
                return RedirectToAction("ThemKiemKeKho_DuLieu_Nhap_Tay");
            }
            return View(nh);
        }
        [HttpGet]
        public ActionResult ThemKiemKeKho_DuLieu_Nhap_Tay()
        {
            ViewBag.lh = new SelectList(db.tblloaihang.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.hh = new SelectList(db.tblhanghoa.ToList().OrderBy(n => n.id), "id", "ma");
            tblkiemkeCTkho nh = TempData["NhValues"] as tblkiemkeCTkho;
            // Truyền giá trị vào View
            return View(nh ?? new tblkiemkeCTkho());
        }
        [HttpPost]
        public ActionResult ThemKiemKeKho_DuLieu_Nhap_Tay(tblkiemkeCTkho KiemKeKhoChitiet)
        {
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
        public ActionResult XoaKiemKeKho(string ten)
        {
            tblkiemkekho kk = db.tblkiemkekho.FirstOrDefault(m => m.nguoikiemke == ten);
            if (kk == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy tên người kiểm kê!";
                return RedirectToAction("KiemKeKho", "BaoCao");
            }
            try
            {
                db.tblkiemkekho.Remove(kk);
                db.SaveChanges();
                return RedirectToAction("KiemKeKho", "BaoCao");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("KiemKeKho", "BaoCao");
        }
        private void ThongTinKiemKe(tblkiemkekho nh)
        {
            nh.id = GenerateDecimalId();
            Session["idKiemKeKho"] = nh.id;
            Session["idKho"] = nh.idkho;
            nh.mmyyyy = DateTime.Now.ToString("MM/yyyy");
            nh.userid = Convert.ToInt32(Session["idUser"]);
            nh.idcongty = Convert.ToInt32(Session["Congty"]);
            nh.hide = 0;
            nh.ngayud = DateTime.Now;
            db.tblkiemkekho.Add(nh);
            db.SaveChanges();
        }
        public ActionResult ScanQR()
        {
            return View();
        }


        /// <summary>
        /// Sử dụng Camera để quét Văn bản
        /// </summary>
        /// <returns></returns>
        public ActionResult ScanText()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProcessCapturedImage(string imageData)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(imageData.Split(',')[1]);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image image = Image.FromStream(ms);
                    string text = PerformOCR(image);
                    TempData["ScannedText"] = text;
                    return RedirectToAction("XuLyText", "BaoCao");
                }
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
        private string PerformOCR(Image image)
        {
            try
            {
                // Convert Image to Pix
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var pix = Pix.LoadFromMemory(ms.ToArray()))
                    {
                        // Initialize Tesseract engine
                        using (var engine = new TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", EngineMode.Default))
                        {
                            using (var page = engine.Process(pix, PageSegMode.Auto))
                            {
                                return page.GetText();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error performing OCR: " + ex.Message;
            }
        }
        public ActionResult XuLyText()
        {
            string scannedText = TempData["ScannedText"] as string;
            tbltaotem a = new tbltaotem();

            // Phân tách dữ liệu quét thành các dòng riêng biệt
            string[] lines = scannedText.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                  .Where(line => !string.IsNullOrWhiteSpace(line))
                                  .ToArray();
            // Xử lý dòng thứ nhất
            if (lines.Length > 0)
            {
                a.ma = ExtractNumberFromString(lines[0]);
                // Xử lý dòng thứ hai
                if (lines.Length > 1)
                {
                    a.lot = ExtractSpecialString(lines[1]);
                }
                // Xử lý dòng thứ ba
                if (lines.Length > 2)
                {
                    string dateFromLine3 = ExtractDateFromString(lines[2]);
                    if (!string.IsNullOrEmpty(dateFromLine3))
                    {
                        a.nsx = Convert.ToDateTime(dateFromLine3);
                    }
                }
                // Xử lý dòng thứ tư
                if (lines.Length > 3)
                {
                    string dateFromLine4 = ExtractDateFromString(lines[3]);
                    if (!string.IsNullOrEmpty(dateFromLine4))
                    {
                        a.hsd = Convert.ToDateTime(dateFromLine4);
                    }
                }
                if (a.ma == 0)
                    return RedirectToAction("ScanText");
                else
                {
                    ViewBag.ma = a.ma;
                    ViewBag.lot = a.lot;
                    ViewBag.nsx = a.nsx;
                    ViewBag.hsd = a.hsd;
                    return View("XuLyText");
                }
            }
            else
                return RedirectToAction("ScanText");
        }
        private int ExtractNumberFromString(string text)
        {
            // Tạo một biểu thức chính quy để tìm dãy số
            Regex regex = new Regex(@"\d+");
            // Tìm tất cả các dãy số trong chuỗi
            MatchCollection matches = regex.Matches(text);
            // Khởi tạo một biến string để lưu trữ dãy số kết quả
            string result = "";
            // Duyệt qua tất cả các kết quả từ biểu thức chính quy
            foreach (Match match in matches)
            {
                result += match.Value;
            }
            if (!string.IsNullOrEmpty(result))
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }
        }
        private string ExtractDateFromString(string text)
        {
            Regex regex = new Regex(@"\b\d{4}-\d{2}-\d{2}\b|\b\d{2}-\d{2}-\d{4}\b|\b\d{2}-\d{2}-\d{2}\b|\b\d{4}\.\d{2}\.\d{2}\b");
            Match match = regex.Match(text);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return "";
            }
        }
        private string ExtractSpecialString(string text)
        {
            string result = Regex.Replace(text, @"[^0-9\-]", " ");
            result = result.Trim();
            return result;
        }


        public ActionResult CapNhatTrangThai(int id, int selectedValue)
        {
            var khoToUpdate = db.tblkiemkekho.FirstOrDefault(k => k.id == id);
            if (selectedValue == 1)
            {
                selectedValue = 0;
            }
            else
            {
                selectedValue = 1;
            }
            if (khoToUpdate != null)
            {
                khoToUpdate.hide = selectedValue;
                db.SaveChanges();
                return RedirectToAction("KiemKeKho");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy kiểm kê!";
                return View("Error");
            }
        }
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
        [HttpGet]
        public ActionResult BaoCaoNhap(int? size, int? page, int? searchstring)
        {
            List<SelectListItem> items = new List<SelectListItem>
        {
            new SelectListItem { Text = "5", Value = "5" },
            new SelectListItem { Text = "10", Value = "10" },
            new SelectListItem { Text = "20", Value = "20" },
            new SelectListItem { Text = "25", Value = "25" }
        };
            foreach (var item in items)
            {
                if (item.Value == size.ToString())
                {
                    item.Selected = true;
                }
            }
            ViewBag.size = items;
            ViewBag.currentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.PageNumber = pageNumber;
            var links = (from l in db.tbltonkhoth select l).OrderBy(x => x.mahanghoa).ToPagedList(pageNumber, pageSize);
            if (searchstring != null)
            {
                links = db.tbltonkhoth.Where(s => s.mahanghoa == searchstring).OrderBy(x => x.mahanghoa).ToPagedList(pageNumber, pageSize);
            }
            return View(links);
        }
    }
}