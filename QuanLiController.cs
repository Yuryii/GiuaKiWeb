using BaiGiuaKiQuanLiCongViec.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaiGiuaKiQuanLiCongViec
{
    public class QuanLiController : Controller
    {
        [Route("QuanLi/Index")]
        public IActionResult IsLogged(string tenQuanLi)
        {
            if (tenQuanLi != null && tenQuanLi.Equals("Admin Đẹp Trai"))
            {
                return RedirectToAction("QuanLiCongViec", new { tenQuanLi = tenQuanLi });
            }
            return Content($"Chào mừng bạn đến với ứng dụng quản lí công việc action Islogged");
        }
        [Route("QuanLi/Index/{tenQuanLi?}")]
        public IActionResult QuanLiCongViec(string tenQuanLi, List<CongViec> listCongViec)
        {

            // string In ra danh sach cong viec

            return Content($"<h1>Xin chào  {tenQuanLi} dưới đây là danh sách công việc mà bạn đã giao</h1> <p>{InRaDanhSachCongViec(CongViecStore.ListCongViec)}</p> <br>", "text/html; charset=utf-8");
        }
        [Route("QuanLi/Index/{tenQuanLi?}/add")]
        public IActionResult Add([FromRoute] string tenQuanLi, [FromQuery] string tenNhanVien, [FromQuery] string tenCongViec)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            bool coTenNhanVien = NhanVienStore.nhanViens.Any(cv => cv.Ten == tenNhanVien);
            if (coTenNhanVien)
            {
                CongViec cv = new CongViec(tenQuanLi, tenNhanVien, tenCongViec);
                CongViecStore.ListCongViec.Add(cv);
                return RedirectToAction("QuanLiCongViec", new { tenQuanLi });
            }
            return Content("Nhân viên không tồn tại");
        }
        [Route("QuanLi/Index/{tenQuanLi?}/delete")]
        public IActionResult Delete([FromQuery] string tenCongViec, [FromQuery] string tenNhanVien, [FromRoute] string tenQuanLi)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            int removeCount = CongViecStore.ListCongViec.RemoveAll(cv => cv.TenCongViec != null && cv.TenCongViec.Equals(tenCongViec) && cv.TenNhanVien == tenNhanVien);
            return RedirectToAction("QuanLiCongViec", new { tenQuanLi });
        }
        [Route("QuanLi/Index/{tenQuanLi?}/edit")]
        public IActionResult Edit([FromQuery] string tenCongViecCu, [FromQuery] string tenCongViecMoi, [FromQuery] string tenNhanVien, [FromRoute] string tenQuanLi)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            int index = CongViecStore.ListCongViec.FindIndex(cv => cv.TenCongViec != null && cv.TenCongViec.Equals(tenCongViecCu) && cv.TenNhanVien == tenNhanVien);
            if (index != -1)
            {
                CongViecStore.ListCongViec[index].TenCongViec = tenCongViecMoi;
            }
            return RedirectToAction("QuanLiCongViec", new { tenQuanLi });
        }

        private string InRaDanhSachCongViec(List<CongViec> congViecs)
        {
            string danhSachCongViecString = "";
            foreach (CongViec cv in congViecs)
            {
                danhSachCongViecString += $" Tên người giao việc: {cv.TenQuanLi} <br> Tên nhân viên: {cv.TenNhanVien} <br> Tên công việc: {cv.TenCongViec} <br><hr>";
            }
            return danhSachCongViecString;
        }
    }
}
