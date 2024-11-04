using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _21520724_KiemTraThucHanh.Models;
using X.PagedList;
using _21520724_KiemTraThucHanh.Models.Authentication;

namespace _21520724_KiemTraThucHanh.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [Route("")]
        [Route("index")]

        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 12;
            int pageIndex = page == null || page < 0 ? 1 : page.Value;

            var dsDanhMuc = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> list = new PagedList<TDanhMucSp>(dsDanhMuc, pageIndex, pageSize);

            return View(list);
        }

        //Them san pham
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        //Sua san pham
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanPham = db.TDanhMucSps.Find(maSanPham);

            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges(); 
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }

        //Xoa san pham
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham (string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.TChiTietSanPhams.Where(x => x.MaSp == maSanPham).ToList();
            if (chiTietSanPham.Count > 0)
            {
                TempData["Message"] = "Khong the xoa san pham nay";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }

            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSanPham).ToList();
            if (anhSanPham.Any())
            {
                db.RemoveRange(anhSanPham);
            }
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "San pham da duoc xoa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }

        //User
        [Route("danhmucnguoidung")]
        [Authentication]
        public IActionResult DanhMucNguoiDung()
        {
            var lstUsers = db.TUsers.ToList();
            return View(lstUsers);
        }
        [Route("ThemNguoiDungMoi")]
        [HttpGet]
        public IActionResult ThemNguoiDungMoi()
        {
            return View();
        }

        [Route("ThemNguoiDungMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguoiDungMoi(TUser user)
        {
            if (ModelState.IsValid)
            {
                db.TUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("DanhMucNguoiDung");
            }
            return View(user);
        }

        [Route("EditNguoiDung")]
        [HttpGet]
        public IActionResult EditNguoiDung(string username)
        {
            var user = db.TUsers.Find(username);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Route("EditNguoiDung")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNguoiDung(TUser user)
        {
            if (ModelState.IsValid)
            {
                db.Update(user);
                db.SaveChanges();
                return RedirectToAction("DanhMucNguoiDung");
            }
            return View(user);
        }

        [Route("XoaNguoiDung")]
        [HttpGet]
        public IActionResult XoaNguoiDung(string username)
        {
            var user = db.TUsers.Find(username);
            if (user == null)
            {
                TempData["Message"] = "Người dùng không tồn tại.";
                return RedirectToAction("DanhMucNguoiDung");
            }

            db.TUsers.Remove(user);
            db.SaveChanges();
            TempData["Message"] = "Người dùng đã được xóa";
            return RedirectToAction("DanhMucNguoiDung");
        }
    }
}
