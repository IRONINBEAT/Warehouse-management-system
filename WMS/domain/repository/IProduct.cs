using WMS.domain.entity;

namespace WMS.domain.repository;

public interface IProduct
{
    void Add(Product product);
    void WriteOff(int id);
    void SendToClient(int id);
}