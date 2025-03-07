using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestTemplate.Models
{
    public class DatSan
    {
        public string ma_danhmuc { get; set; }
        public string ma_San { get; set; }
        public string ma_KH { get; set; }
        public string ngayDatSan { get; set; } // Thêm thuộc tính ngày đặt sân kiểu string
        public string gioBatDau { get; set; } // Thêm thuộc tính giờ bắt đầu kiểu string
        public string gioKetThuc { get; set; }

        //Thông tin thanh toán
        public bool trangThaiThanhToan { get; set; } // Đã thanh toán (true) hoặc chưa (false)
        public string phuongThucThanhToan { get; set; } // Tiền mặt, chuyển khoản, ví điện tử
        public decimal soTienThanhToan { get; set; } // Số tiền đã thanh toán
        public DateTime? thoiGianThanhToan { get; set; } // Thời gian thanh toán
        public string maGiaoDich { get; set; } // Mã giao dịch thanh toán (nếu

    }
}