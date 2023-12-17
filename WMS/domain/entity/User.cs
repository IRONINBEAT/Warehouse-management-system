using WMS.domain.enumerate;

namespace WMS.domain.entity;

public class User
{
    public User(int id, string login, string name, string password, string patronymic, string surName )
    {
        ID = id;
        Login = login;
        Name = name;
        Password = password;
        Patronymic = patronymic;
        SurName = surName;
    }
    
    public int ID { get; private set; }
    public string Login { get; private set; }


    public string Name { get; private set; }
    public string Password { get; private set; }
    public string Patronymic { get; private set; }
    public string SurName { get; private set; }
    
    public Role Role { get; private set; }

    public void ChangeRole(Role newRole)
    {
        Role = newRole;
    }
}