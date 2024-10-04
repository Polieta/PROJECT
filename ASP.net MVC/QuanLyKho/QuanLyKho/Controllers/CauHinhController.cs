using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKho.Controllers
{
    public class CauHinhController : Controller
    {
        public ActionResult Index()
        {
            // Đường dẫn tới thư mục chứa ảnh làm background (được biết trước)
            string folderPath = Server.MapPath("~/QuanLyKho/Backgrounds");

            // Kiểm tra xem thư mục có tồn tại không
            if (Directory.Exists(folderPath))
            {
                // Lấy danh sách các file trong thư mục
                string[] files = Directory.GetFiles(folderPath);

                // Danh sách tên file ảnh
                List<string> imageFileNames = new List<string>();

                // Lặp qua từng file và lấy tên của nó
                foreach (string file in files)
                {
                    imageFileNames.Add(Path.GetFileName(file));
                }

                // Truyền danh sách tên file ảnh qua view
                ViewBag.ImageFileNames = imageFileNames;
            }

            return View();
        }
    }
}
