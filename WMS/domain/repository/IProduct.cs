using System.Collections.Generic;
using WMS.domain.entity;
using WMS.domain.enumerate;

namespace WMS.domain.repository;

public interface IProduct
{
    ProductAddingErrors Add(Product product);

    Product Get(int id);
    List<Product> GetAllProducts();
    void WriteOff(Product product);
    void SendToClient(int id);
}