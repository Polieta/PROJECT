//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKho.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblloaihang
    {
        public tblloaihang()
        {
            this.tblhanghoa = new HashSet<tblhanghoa>();
            this.tblkiemkeCTkho = new HashSet<tblkiemkeCTkho>();
            this.tbltaotem = new HashSet<tbltaotem>();
        }
    
        public int id { get; set; }
        public int nhomhang { get; set; }
        public string ma { get; set; }
        public string ten { get; set; }
        public Nullable<System.DateTime> ngayud { get; set; }
        public Nullable<int> userid { get; set; }
        public Nullable<int> hide { get; set; }
        public int idcongty { get; set; }
    
        public virtual tblcongty tblcongty { get; set; }
        public virtual tbldangnhap tbldangnhap { get; set; }
        public virtual ICollection<tblhanghoa> tblhanghoa { get; set; }
        public virtual ICollection<tblkiemkeCTkho> tblkiemkeCTkho { get; set; }
        public virtual tblnhomhang tblnhomhang { get; set; }
        public virtual ICollection<tbltaotem> tbltaotem { get; set; }
    }
}
