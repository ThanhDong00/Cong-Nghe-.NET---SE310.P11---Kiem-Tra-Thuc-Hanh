using System;
using System.Collections.Generic;

namespace _21520724_KiemTraThucHanh.Models;

public partial class TKichThuoc
{
    public string MaKichThuoc { get; set; } = null!;

    public string? KichThuoc { get; set; }

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; } = new List<TChiTietSanPham>();
}
