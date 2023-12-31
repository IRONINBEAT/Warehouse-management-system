﻿using System.Collections.Generic;
using WMS.domain.entity;

namespace WMS.data.repository;

public class AuthorizedUserRepository: SerializationRepository<User>
{
    private static UserRepository? _globalRepositoryInstance;
    
    public static UserRepository GetInstance()
    {
        return _globalRepositoryInstance ??= new UserRepository("../../../../data/data_set/AuthorizedUser.json");
    }
    
    public void Add(User user)
    {
        Append(user);
    }

    public void Remove(User user)
    {
        Remove(user);
    }
    
    public User Download()
    {
        var deserialized = Deserialize();
        return deserialized[^1];
    }
    
    public AuthorizedUserRepository(string filePath) : base(filePath)
    {
    }

    public override bool CompareEntities(User obj1, User obj2)
    {
        throw new System.NotImplementedException();
    }
}