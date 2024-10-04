using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;
using PagedList;
using PagedList.Mvc;

namespace QuanLyKho.Controllers
{
    public class HangHoaController : Controller
    {
        //
        // GET: /HangHoa/
        QuanLyKho_HongLamEntities db = new QuanLyKho_HongLamEntities();
        [HttpGet]
        public ActionResult HangHoa(int? size, int? page, string searchstring)
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
            var links = db.tblhanghoa.OrderBy(x => x.ma).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.ma.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemHangHoa()
        {
            ViewBag.dvt = new SelectList(db.tbldvt.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.lh = new SelectList(db.tblloaihang.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.nh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
            return View();
        }
        [HttpPost]
        public ActionResult ThemHangHoa(tblhanghoa nh)
        {
            if (ModelState.IsValid)
            {
                var existingHangHoa = db.tblhanghoa.FirstOrDefault(k => k.ma == nh.ma);
                var existingHangHoa1 = db.tblhanghoa.FirstOrDefault(k => k.ten == nh.ten);
                if (existingHangHoa != null)
                {
                    TempData["ErrorMessage"] = "Mã hàng hoá đã tồn tại!";
                    ViewBag.dvt = new SelectList(db.tbldvt.ToList().OrderBy(n => n.id), "id", "ten");
                    ViewBag.lh = new SelectList(db.tblloaihang.ToList().OrderBy(n => n.id), "id", "ten");
                    ViewBag.nh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
                    return View(nh);
                }
                else if (existingHangHoa1 != null)
                {
                    TempData["ErrorMessage"] = "Tên hàng hoá đã tồn tại!";
                    ViewBag.dvt = new SelectList(db.tbldvt.ToList().OrderBy(n => n.id), "id", "ten");
                    ViewBag.lh = new SelectList(db.tblloaihang.ToList().OrderBy(n => n.id), "id", "ten");
                    ViewBag.nh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
                    return View(nh);
                }
                    nh.userid = Convert.ToInt32(Session["idUser"]);
                    if (db.tblhanghoa.Count() == 0)
                    {
                        nh.id = 0;
                    }
                    else
                    {
                        nh.id = db.tblhanghoa.Max(n => n.id) + 1;
                    }
                    nh.hide = 0;
                    nh.idcongty = Convert.ToInt32(Session["Congty"]);
                    nh.ngayud = DateTime.Now.Date;
                    db.tblhanghoa.Add(nh);
                    db.SaveChanges();
                
                return RedirectToAction("HangHoa");     
             }
            return View(nh);
        }
        public ActionResult XoaHangHoa(string ma, string ten)
        {
            tblhanghoa hh = db.tblhanghoa.FirstOrDefault(m => m.ma == ma);
            if (hh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã hàng hóa!";
                return RedirectToAction("HangHoa", "HangHoa");
            }
            
            try
            {
                db.tblhanghoa.Remove(hh);
                db.SaveChanges();
                return RedirectToAction("HangHoa", "HangHoa");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("HangHoa", "HangHoa");
        }
        [HttpGet]
        public ActionResult SuaHangHoa(string ma)
        {
            tblhanghoa hh = db.tblhanghoa.FirstOrDefault(n => n.ma == ma);
            ViewBag.dvt = new SelectList(db.tbldvt.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.lh = new SelectList(db.tblloaihang.ToList().OrderBy(n => n.id), "id", "ten");
            ViewBag.nh = new SelectList(db.tblnhomhang.ToList().OrderBy(n => n.id), "id", "ten");
            if (hh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã hàng hóa!";
                return RedirectToAction("HangHoa", "HangHoa");
            }
            return View(hh);
        }
        [HttpPost]
        public ActionResult SuaHangHoa(string ma, string ten)
        {
            tblhanghoa hh = db.tblhanghoa.FirstOrDefault(n => n.ma == ma);
            if (ModelState.IsValid)
            {
                UpdateModel(hh);
                db.SaveChanges();
            }
            return RedirectToAction("HangHoa", "HangHoa");
        }
        public ActionResult CapNhatTrangThaiHangHoa(int id, int selectedValue)
        {
            var khoToUpdate = db.tblhanghoa.FirstOrDefault(k => k.id == id);
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
                return RedirectToAction("HangHoa");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy kho!";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult NhaCC(int? size, int? page, string searchstring)
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
            var links = db.tblnhacc.OrderBy(x => x.ma).AsQueryable(); // Đưa truy vấn vào biến links
            if (!string.IsNullOrEmpty(searchstring)) // Kiểm tra xem searchstring có giá trị không
            {
                links = links.Where(s => s.ma.Contains(searchstring)); // Sử dụng Contains để kiểm tra chuỗi tồn tại trong ma
            }
            var pagedLinks = links.ToPagedList(pageNumber, pageSize); // Áp dụng phân trang cho kết quả
            return View(pagedLinks);
        }
        [HttpGet]
        public ActionResult ThemNhaCC()
        {
            List<SelectListItem> danhSachLuaChon = new List<SelectListItem>
            {
                new SelectListItem { Text = "Khách hàng", Value = "KhachHang" },
                new SelectListItem { Text = "Nhà cung cấp", Value = "NhaCungCap" },
                new SelectListItem { Text = "Nhà cung cấp/Khách hàng", Value = "NhaCungCapKH" }
            };
            ViewBag.DanhSachLuaChon = danhSachLuaChon; // Truyền danh sách lựa chọn vào ViewBag
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhaCC(tblnhacc ncc, string LoaiLuaChon)
        {
            if (ModelState.IsValid)
            {
                var existingHangHoa = db.tblnhacc.FirstOrDefault(k => k.ma == ncc.ma);
                var existingHangHoa1 = db.tblnhacc.FirstOrDefault(k => k.ten == ncc.ten);
                if (existingHangHoa != null)
                {
                    TempData["ErrorMessage"] = "Mã nhà cung cấp đã tồn tại!";
                    List<SelectListItem> danhSachLuaChon = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Khách hàng", Value = "KhachHang" },
                        new SelectListItem { Text = "Nhà cung cấp", Value = "NhaCungCap" },
                        new SelectListItem { Text = "Nhà cung cấp/Khách hàng", Value = "NhaCungCapKH" }
                    };
                    ViewBag.DanhSachLuaChon = danhSachLuaChon;
                    return View(ncc);
                } else 
                    if(existingHangHoa1 != null)
                {
                    TempData["ErrorMessage"] = "Tên nhà cung cấp đã tồn tại!";
                    List<SelectListItem> danhSachLuaChon = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Khách hàng", Value = "KhachHang" },
                        new SelectListItem { Text = "Nhà cung cấp", Value = "NhaCungCap" },
                        new SelectListItem { Text = "Nhà cung cấp/Khách hàng", Value = "NhaCungCapKH" }
                    };
                    ViewBag.DanhSachLuaChon = danhSachLuaChon;
                    return View(ncc);
                }
                    ncc.userid = Convert.ToInt32(Session["idUser"]);
                    if (db.tblnhacc.Count() == 0)
                    {
                        ncc.id = 0;
                    }
                    else
                    {
                        ncc.id = db.tblnhacc.Max(n => n.id) + 1;
                    }
                    if (LoaiLuaChon == "KhachHang")
                    {
                        ncc.loai = 0; // Nếu chọn Khách hàng
                    }
                    else if (LoaiLuaChon == "NhaCungCap")
                    {
                        ncc.loai = 1; // Nếu chọn Nhà cung cấp
                    }
                    else if (LoaiLuaChon == "NhaCungCapKH")
                    {
                        ncc.loai = 2; // Nếu chọn Nhà cung cấp/Khách hàng
                    }
                    ncc.hide = 0;
                    ncc.idcongty = Convert.ToInt32(Session["Congty"]);
                    ncc.ngayud = DateTime.Now.Date;
                    db.tblnhacc.Add(ncc);
                    db.SaveChanges();
                    return RedirectToAction("NhaCC");
            }
            return View(ncc);
        }
        public ActionResult XoaNhaCC(string ma, string ten)
        {
            tblnhacc hh = db.tblnhacc.FirstOrDefault(m => m.ma == ma);
            if (hh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã nhà cung cấp!";
                return RedirectToAction("NhaCC", "HangHoa");
            }
           
            try
            {
                    db.tblnhacc.Remove(hh);
                    db.SaveChanges();
                    return RedirectToAction("NhaCC", "HangHoa");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Dữ liệu đang được sử dụng và không thể xóa!";
            }
            return RedirectToAction("NhaCC", "HangHoa");
        }
        [HttpGet]
        public ActionResult SuaNhaCC(string ma)
        {
            tblnhacc hh = db.tblnhacc.SingleOrDefault(n => n.ma == ma);
            if (hh == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mã nhà cung cấp!";
                return RedirectToAction("NhaCC", "HangHoa");
            }

            List<SelectListItem> danhSachLuaChon = new List<SelectListItem>
            {
                new SelectListItem { Text = "Khách hàng", Value = "KhachHang" },
                new SelectListItem { Text = "Nhà cung cấp", Value = "NhaCungCap" }
            };
            ViewBag.DanhSachLuaChon = danhSachLuaChon;

            return View(hh);
        }
        [HttpPost]
        public ActionResult SuaNhaCC(string ma, string ten, string LoaiLuaChon)
        {
            tblnhacc ncc = db.tblnhacc.SingleOrDefault(n => n.ma == ma);
            if (ModelState.IsValid)
            {
                if (LoaiLuaChon == "KhachHang")
                {
                    ncc.loai = 2; // Nếu chọn Khách hàng
                }
                else if (LoaiLuaChon == "NhaCungCap")
                {
                    ncc.loai = 1; // Nếu chọn Nhà cung cấp
                }
                UpdateModel(ncc);
                db.SaveChanges();
            }
            return RedirectToAction("NhaCC", "HangHoa");
        }
        public ActionResult CapNhatTrangThaiNhaCC(int id, int selectedValue)
        {
            var khoToUpdate = db.tblnhacc.FirstOrDefault(k => k.id == id);
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
                return RedirectToAction("NhaCC");
            }
            else
            {
                ViewBag.ErrorMessage = "Không tìm thấy nhà cung cấp!";
                return View("Error");
            }
        }
    }
}