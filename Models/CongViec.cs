namespace BaiGiuaKiQuanLiCongViec.Models
{
    public class CongViec
    {
        public  string TenQuanLi { get; set; }
        public string TenNhanVien { get; set; }
        public string? TenCongViec { get; set; }
        public CongViec()
        {

        } 
        public CongViec(string tenQuanLi, string tenNhanVien, string tenCongViec)
        {
            TenQuanLi = tenQuanLi;
            TenNhanVien = tenNhanVien;
            TenCongViec = tenCongViec;
        }

    }
}
