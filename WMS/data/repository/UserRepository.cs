using System.Collections.Generic;
using WMS.domain.entity;

namespace WMS.data.repository;


public class UserRepository : SerializationRepository<User>
{
    private static UserRepository? _globalRepositoryInstance;
    
    public UserRepository(string filePath) : base(filePath)
    {
        
    }

    public override bool CompareEntities(User obj1, User obj2)
    {
        return obj1.ID == obj2.ID;
    }

    public void Add(User user)
    {
        Append(user);
    }

    public List<User> Download()
    {
        return Deserialize();
    }
    
    public void Remove(User user)
    {
        RemoveItem(user);
    }
    
    public static UserRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new UserRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Users.json");
    }
}