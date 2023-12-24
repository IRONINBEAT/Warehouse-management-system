using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.repository;

namespace WMS.domain.use_case;

public class UserUseCase : IUser
{
    private UserRepository _userRepository;

    public UserUseCase(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public bool Register(User user)
    {
        if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
        {
            user.Password = HashPassword(user.Password);
            _userRepository.Add(user);
            return true;
        }

        return false;
    }

    public User Authorize(string login, string password)
    {
        List<User> users = _userRepository.Download();
        User foundUser = users.FirstOrDefault(u => u.Login == login && CheckPassword(password, u.Password));

        if (foundUser == null) return null;
        return foundUser;

    }

    public void ChangeRole(int id)
    {

    }

    public void Dismiss(User user)
    {
        _userRepository.Remove(user);
    }

    public List<User> GetAllUsers()
    {
        return _userRepository.Download();
    }
    public string GetEnumDescription(Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    
    static string HashPassword(string password)
    {
        // Генерация соли и хеширование пароля
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    static bool CheckPassword(string inputPassword, string hashedPassword)
    {
        // Проверка пароля
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }
    
}