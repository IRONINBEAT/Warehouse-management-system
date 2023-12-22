using System.Collections.Generic;
using WMS.domain.entity;

namespace WMS.domain.repository;

public interface IUser
{
    void Register(User user);
    bool Authorize(string login, string password);
    void ChangeRole(int id);
    void Dismiss(User user);

    List<User> GetAllUsers();
}