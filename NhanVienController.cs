using BaiGiuaKiQuanLiCongViec.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiGiuaKiQuanLiCongViec
{
    public class NhanVienController : Controller
    {
        [Route("NhanVienDangKi")]
        public IActionResult DangKi([FromQuery] NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            int taiKhoanTrung = NhanVienStore.nhanViens.FindIndex(nv => nv.TaiKhoan.Equals(nhanVien.TaiKhoan) || nv.Ten.Equals(nhanVien.Ten));
            if (taiKhoanTrung != -1)
            {
                return Content("Tên tài khoản hoặc tên nhân viên bị trùng");
            }
            NhanVienStore.nhanViens.Add(nhanVien);
            return RedirectToAction("DanhSachCongViec", new { tenNhanVien = nhanVien.Ten });
        }
        [Route("NhanVienDangNhap")]
        public IActionResult DangNhap([FromQuery] string taikhoan, [FromQuery] string matkhau)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            NhanVien nv = NhanVienStore.nhanViens.Find(nv => nv.TaiKhoan.Equals(taikhoan) && nv.MatKhau.Equals(matkhau));
            if (nv != null)
            {
                return RedirectToAction("DanhSachCongViec", new { tenNhanVien = nv.Ten });
            }
            return Content("Tên tài khoản hoặc mật khẩu không chính xác");
        }
        [Route("NhanVien/DanhSachCongViec")]
        public IActionResult DanhSachCongViec([FromQuery] string tenNhanVien)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            return Content($"<h1>Dưới đây là danh sách công việc của ngài {tenNhanVien} :</h1> <p> {InDanhSachCongViec(CongViecStore.ListCongViec, tenNhanVien)}</p>", "text/html; charset=utf-8");
        }
        private string InDanhSachCongViec(List<CongViec> congViecs, string tenNhanVien)
        {

            //List<string> danhSachCongViec = new List<string>();

            //foreach (var cv in congViecs)
            //{
            //    if (cv.TenNhanVien != null && cv.TenNhanVien.Equals(tenNhanVien))
            //    {
            //        danhSachCongViec.Add(cv.TenCongViec);
            //    }
            //}

            List<CongViec> danhSachCongViec = congViecs.FindAll(cv => cv.TenNhanVien.Equals(tenNhanVien));
            string danhSachCongViecString = "";
            foreach (CongViec cv in danhSachCongViec)
            {
                danhSachCongViecString += $" Tên người giao {cv.TenQuanLi} <br> Công việc 1: {cv.TenCongViec} <br><hr>";
            }
            return danhSachCongViecString;
        }
    }
}
