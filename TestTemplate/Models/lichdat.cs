//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public partial class LichDat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LichDat()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }

        public string MaLichDat { get; set; }

        public DateTime ThoiGianTao {get; set;}
        
        public string MaKhachHang { get; set; }
        public string MaSan { get; set; }
        public Nullable<System.DateTime> ThoiGianBatDau { get; set; }
        public Nullable<System.DateTime> ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual user_KhachHang user_KhachHang { get; set; }
        public virtual San San { get; set; }
    }
}
