using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using DoAnNhom8.Areas.Admin.Controllers;
using DoAnNhom8.Models;
namespace DoAnNhom8.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        DataClasses1DataContext DB = new DataClasses1DataContext();
        public ActionResult List_NhanVien()
        {
            var sp = DB.NHANVIENs.ToList();
            return View(sp);
        }

        private string apiUrl = "http://localhost:50715/Areas/Admin/api/NhanVien/GetNhanViens"; 
        public async Task<ActionResult> List_NhanVien1()
        {
            IEnumerable<NHANVIEN> nhanViens = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(""); // hoặc client.GetAsync("/")

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    nhanViens = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NHANVIEN>>(result);
                }
                else
                {
                    // Xử lý lỗi nếu có
                    nhanViens = Enumerable.Empty<NHANVIEN>();
                    ModelState.AddModelError(string.Empty, "Lỗi khi gọi API");
                }
            }

            return RedirectToAction("List_NhanVien");
        }
    }
}