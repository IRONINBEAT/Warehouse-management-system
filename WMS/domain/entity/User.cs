using WMS.domain.enumerate;

namespace WMS.domain.entity;

public class User
{
    private Role _role;
    public int id;
    public string login;


    public string name;
    public string pasword;
    public string patronymic;
    public string surName;
    public Role Role { get; }

    public void ChangeRole(Role newRole)
    {
        _role = newRole;
    }
}