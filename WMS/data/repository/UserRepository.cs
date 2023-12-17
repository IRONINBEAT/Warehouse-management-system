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
        throw new System.NotImplementedException();
    }

    public void Add(User user)
    {
        Append(user);
    }

    public List<User> Download()
    {
        return Deserialize();
    }
    
    public static UserRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new UserRepository("../../../../data/data_set/Users.json");
    }
}