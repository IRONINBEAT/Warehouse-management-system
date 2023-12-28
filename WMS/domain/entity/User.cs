using System;
using WMS.domain.enumerate;

namespace WMS.domain.entity;

public class User
{
    private string _login;
    private string _password;
    private string _name;
    private string _patronymic;
    private string _surName;

    public User(int id, string login, string name, string password, string patronymic, string surName, Role role)
    {
        ID = id;
        Login = login;
        Name = name;
        Password = password;
        Patronymic = patronymic;
        SurName = surName;
        Role = role;
    }

    public int ID { get; private set; }

    public string Login
    {
        get { return _login; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Логин не может быть пустым или состоять только из пробелов");
            }
            _login = value;
        }
    }

    public string Name
    {
        get { return _name; }
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Имя не может быть пустым");
            }
            _name = value;
        }
    }

    public string Password
    {
        get { return _password;}
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Пароль не может быть пустым или состоять только из пробелов");
            }
            _password = value;
        }
    }

    public string Patronymic
    {
        get { return _patronymic; }
        private set { _patronymic = value; }
    }

    public string SurName
    {
        get { return _surName; }
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Фамилия не может быть пустой");
            }
            _surName = value;
        }
    }

    public Role Role { get; private set; }

    public string GetFio => $"{SurName} {Name} {Patronymic}";

    public void ChangeRole(Role newRole)
    {
        Role = newRole;
    }
}