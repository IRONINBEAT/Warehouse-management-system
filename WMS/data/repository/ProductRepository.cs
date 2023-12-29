using System.Collections.Generic;
using WMS.domain.entity;

namespace WMS.data.repository;

public class ProductRepository : SerializationRepository<Product>
{
    private static ProductRepository? _globalRepositoryInstance;
    
    public void Add(Product product)
    {
        Append(product);
    }
    
    public static ProductRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new ProductRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Products.json");
    }
    
    public void Remove(Product product)
    {
        RemoveItem(product);
    }

    public void Change(Product product) => ChangeEntity(product);
    public ProductRepository(string filePath) : base(filePath)
    {
    }
    public List<Product> GetAll()
    {
        return Deserialize();
    }

    public override bool CompareEntities(Product obj1, Product obj2)
    {
        return obj1.Id == obj2.Id;
    }
}