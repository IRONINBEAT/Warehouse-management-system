using WMS.domain.entity;
using WMS.domain.repository;

namespace WMS.domain.use_case;

public class UserUseCase : IUser
{
    public void Register(User user)
    {
        throw new System.NotImplementedException();
    }

    public void Authorize(string login, string password)
    {
        throw new System.NotImplementedException();
    }

    public void ChangeRole(int id)
    {
        throw new System.NotImplementedException();
    }

    public void Dismiss(int id)
    {
        throw new System.NotImplementedException();
    }
}