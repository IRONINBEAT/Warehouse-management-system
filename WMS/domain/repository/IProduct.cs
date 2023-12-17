using WMS.domain.entity;
using WMS.domain.enumerate;

namespace WMS.domain.repository;

public interface IProduct
{
    ProductAddingErrors Add(Product product);
    void WriteOff(int id);
    void SendToClient(int id);
}