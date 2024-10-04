using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Data.Entity.Core;

namespace QuanLyKho.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        QuanLyKho_HongLamEntities _db = new QuanLyKho_HongLamEntities();
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        //GET: Register 
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tbldangnhap _user,FormCollection f)
        {
            var Ten = f["name"];
            var Email = f["email"];
            var Password = f["matkhau"];
            var MaCT = f["macongty"];
            var TenCT = f["tencongty"];
            var DiachiCT = f["diachi"];
            var MsthueCT = f["msthue"];
            if (f["matkhau"] != f["rematkhau"])
            {
                ModelState.AddModelError("rematkhau", "Mật khẩu và nhập lại mật khẩu không khớp.");
                ModelState.SetModelValue("name", new ValueProviderResult(f["name"], f["name"], CultureInfo.InvariantCulture));
                ModelState.SetModelValue("email", new ValueProviderResult(f["email"], f["email"], CultureInfo.InvariantCulture));
                ModelState.SetModelValue("macongty", new ValueProviderResult(f["macongty"], f["macongty"], CultureInfo.InvariantCulture));
                ModelState.SetModelValue("tencongty", new ValueProviderResult(f["tencongty"], f["tencongty"], CultureInfo.InvariantCulture));
                ModelState.SetModelValue("diachi", new ValueProviderResult(f["diachi"], f["diachi"], CultureInfo.InvariantCulture));
                ModelState.SetModelValue("msthue", new ValueProviderResult(f["msthue"], f["msthue"], CultureInfo.InvariantCulture));
                return View();
            }
            if (ModelState.IsValid)
            {
                var tenCT = _db.tblcongty.FirstOrDefault(s => s.ten == TenCT);
                var TenTK = _db.tbldangnhap.FirstOrDefault(s => s.ten == Ten);
                var MatKhau = _db.tbldangnhap.FirstOrDefault(s => s.password == Password);
                if (TenTK == null || MatKhau == null)
                {
                    if(tenCT == null)
                    {
                        tblcongty ct = new tblcongty();
                    int kiemtra = _db.tblcongty.Count();
                        if (kiemtra == 0)
                        {
                            ct.id = 0;
                        }
                        else
                        {
                            ct.id = _db.tblcongty.Max(n => n.id) + 1;
                        }
                    ct.ma = MaCT;
                    ct.ten = TenCT;
                    ct.diachi = DiachiCT;
                    ct.msthue = MsthueCT;
                    _db.tblcongty.Add(ct);
                    _db.SaveChanges();
                    }
                    //Thêm người dùng
                    _user.id = capid();
                    _user.ten = Ten;
                    _user.email = Email;
                    _user.password = Password;//GetMD5(Password);
                    _user.ngayud = DateTime.Now.Date;
                    if (tenCT == null)
                    {
                        _user.idCongTy = 0;
                    }
                    else
                    {
                        _user.idCongTy = tenCT.id;
                    }
                    _user.hide = 1;
                    //Session["Hide"] = _user.hide;
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.tbldangnhap.Add(_user);
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = "Đăng ký thành công! Hãy liên hệ số 0935111247 để được kích hoạt tài khoản";
                    return RedirectToAction("Register");
                }
                else
                {
                    TempData["ErrorMessage"] = "Tài khoản đã tồn tại";
                    return RedirectToAction("Register");
                }
            }
            return View();
        }
        public ActionResult Login()
        {
          ViewBag.ct = new SelectList(_db.tblcongty.ToList().OrderBy(n => n.id), "id", "ten");
          return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblcongty ct,FormCollection f)
        {
            var Ten = f["name"];
            var Password = f["password"];
            var Congty = ct.id;
            var data = _db.tbldangnhap.Where(s => s.ten.Equals(Ten) && s.password.Equals(Password) && s.idCongTy.Equals(Congty)).ToList();
            if (ModelState.IsValid)
            {
                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().hide == 1)
                    {
                        TempData["ErrorMessage"] = "Tài khoản chưa được kích hoạt hãy liên hệ số 0935111247";
                        return RedirectToAction("Login");
                    }
                     //var f_password = Password; GetMD5(Password);            
                    //thêm session
                    Session["Name"] = data.FirstOrDefault().ten;
                    Session["Email"] = data.FirstOrDefault().email;
                    Session["idUser"] = data.FirstOrDefault().id;
                    Session["Congty"] = data.FirstOrDefault().idCongTy;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Đăng nhập thất bại";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");          
            }
            return byte2String;
        }
        private int capid()
        {
            int id = 0;
            int tess = _db.tbldangnhap.Count();
            if (tess == 0)
            {
                return id;
            }
            else
            {
                return _db.tbldangnhap.Max(n => n.id) + 1;
            }
        }
        public ActionResult Error()
        {
            TempData["ErrorMessage"] = "Đang trong quá trình phát triển!";
            return RedirectToAction("Index");
        }
	}
}