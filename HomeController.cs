using BaiGiuaKiQuanLiCongViec.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiGiuaKiQuanLiCongViec
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Chào mừng bạn đến với ứng dụng quản lí công việc");
        }
        [Route("Login")]
        public IActionResult Login( string taiKhoan, string matKhau)
        {
            if (!ModelState.IsValid)
            {
                //get error messages from model state
                string errors = string.Join("\n", ModelState.Values.SelectMany(value =>
                value.Errors).Select(err => err.ErrorMessage));
                return BadRequest(errors);
            }
            if(taiKhoan == "admin" && matKhau == "Dntu@123")
            {
                return RedirectToAction("IsLogged", "QuanLi", new {tenQuanLi = "Admin Đẹp Trai"});
            }
            // return RedirectToAction("Index", "QuanLi");
            return Content($"Tên tài khoản hoặc mật khẩu không chính xác");
        }
    }
}
