using WMS.domain.entity;

namespace WMS.domain.repository;

public interface IUser
{
    void Register(User user);
    void Authorize(string login, string password);
    void ChangeRole(int id);
    void Dismiss(int id);
}