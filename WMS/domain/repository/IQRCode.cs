using System.Windows.Media.Imaging;
using WMS.domain.entity;

namespace WMS.domain.repository;

public interface IQrCode
{
    void Generate(Product product);
    BitmapImage Load(Product product);
}