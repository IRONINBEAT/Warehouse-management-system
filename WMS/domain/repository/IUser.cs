using System.Collections.Generic;
using WMS.domain.entity;

namespace WMS.domain.repository;

public interface IUser
{
    bool Register(User user);
    User Authorize(string login, string password);
    void Dismiss(User user);

    List<User> GetAllUsers();
}