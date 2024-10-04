using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom8
{
    class TaiKhoan
    {
        private string tentaikhoan { get; set; }
        private string matkhau { get; set; }

        public TaiKhoan() { }

        public TaiKhoan(string Taikhoan,string Matkhau) {
            this.tentaikhoan = Taikhoan;
            this.matkhau = Matkhau;
        }
    }
}
