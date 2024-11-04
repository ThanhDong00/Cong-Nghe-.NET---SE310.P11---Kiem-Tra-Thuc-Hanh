using _21520724_KiemTraThucHanh.Models;
using Microsoft.AspNetCore.Mvc;
using _21520724_KiemTraThucHanh.Repository;

namespace _21520724_KiemTraThucHanh.ViewComponents
{
    public class LoaiSpMenuViewComponent: ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSpRepository;

        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSpMenuRepository)
        {
            _loaiSpRepository = loaiSpMenuRepository;
        }

        public IViewComponentResult Invoke()
        {
            var dsLoaiSp = _loaiSpRepository.GetAllLoaiSp().OrderBy(x => x.Loai);
            return View(dsLoaiSp);
        }
    }
}
