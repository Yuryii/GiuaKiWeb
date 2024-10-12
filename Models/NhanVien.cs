using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaiGiuaKiQuanLiCongViec.Models
{
    public class NhanVien
    {
        //[Required(ErrorMessage ="Tên không được trống")]
        //[StringLength(100, MinimumLength = 5, ErrorMessage = "Tên tài khoản không hợp lệ")]
        public string? Ten { get; set; }
        [Required(ErrorMessage ="Tên tài khoản không hợp lệ")]
        [StringLength(100, MinimumLength =5, ErrorMessage ="Tên tài khoản không hợp lệ")]
        public string? TaiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải bao gồm ít nhất 1 chữ hoa, 1 chữ thường, 1 số và 1 ký tự đặc biệt")]
        public string? MatKhau { get; set; }

        public NhanVien()
        {
        }
        public NhanVien(string ten, string taiKhoan, string matKhau)
        {
            Ten = ten;
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
        }
    }
}
