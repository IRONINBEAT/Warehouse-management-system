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
        return _globalRepositoryInstance ??= new ProductRepository("../../../../data/data_set/Products.json");
    }
    
    public void Remove(Product product)
    {
        Remove(product);
    }
    public ProductRepository(string filePath) : base(filePath)
    {
    }
    public List<Product> GetAll()
    {
        return Deserialize();
    }

    public override bool CompareEntities(Product obj1, Product obj2)
    {
        throw new System.NotImplementedException();
    }
}