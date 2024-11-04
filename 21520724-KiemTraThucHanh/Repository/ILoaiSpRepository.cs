using _21520724_KiemTraThucHanh.Models;

namespace _21520724_KiemTraThucHanh.Repository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp GetLoaiSp(string maLoaiSp);
        TLoaiSp Add(TLoaiSp loaiSp);
        TLoaiSp Update(TLoaiSp loaiSp);
        TLoaiSp Delete(string maLoaiSp);

        IEnumerable<TLoaiSp> GetAllLoaiSp();
    }
}
