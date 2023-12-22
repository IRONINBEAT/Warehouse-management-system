﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.repository;

namespace WMS.domain.use_case;

public class UserUseCase : IUser
{
    private UserRepository _userRepository;

    public UserUseCase(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public void Register(User user)
    {
        //сделать проверку на id, что такого id не задано у какого-то другого пользователя
        if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
            _userRepository.Add(user);
    }

    public bool Authorize(string login, string password)
    {
        List <User> users = _userRepository.Download();

        foreach (var user in users)
            if (user.Login == login && user.Password == password) return true;
        
        return false;
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
}