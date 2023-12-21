using WMS.domain.entity;

namespace WMS.data.repository;

public class CustomerRepository: SerializationRepository<Customer>
{
    private static CustomerRepository? _globalRepositoryInstance;
    
    public void Add(Customer customer)
    {
        Append(customer);
    }
    
    public static CustomerRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new CustomerRepository("../../../../data/data_set/Customers.json");
    }
    public CustomerRepository(string filePath) : base(filePath)
    {
    }

    public override bool CompareEntities(Customer obj1, Customer obj2)
    {
        throw new System.NotImplementedException();
    }
}