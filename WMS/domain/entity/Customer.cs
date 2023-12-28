using System;
using System.Linq;
using WMS.domain.enumerate;

namespace WMS.domain.entity;

public class Customer
{
    private string _name;
    private string _phoneNumber;
    private string _email;
    private string _country;
    private string _city;
    private string _street;
    private string _houseNumber;
    private string _postalCode;
    
    public Customer()
    {
        
    }
    public Customer(int productId, string name, string phoneNumber, string email, string country,
        string city, string street, string houseNumber, string postalCode, ResponsibilityType responsibilityType)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        ResponsibilityType = responsibilityType;
        ProductId = productId;
    }
    
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Имя не может быть пустым");
            }
            _name = value;
        }
    }

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            const string phoneNumberPattern = "0-000-000-00-00"; 

            if (!IsPhoneNumber(value, phoneNumberPattern))
            {
                throw new Exception("Некорректный формат телефонного номера");
            }
            _phoneNumber = value;
        }
    }

    public string Email
    {
        get { return _email; }
        set
        {
            if (!IsValidEmail(value))
            {
                throw new Exception("Некорректный формат электронной почты");
            }
            _email = value;
        }
    }

    public string Country
    {
        get { return _country; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Страна не может быть пустой");
            }
            _country = value;
        }
    }

    public string City
    {
        get { return _city; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Город не может быть пустым");
            }
            _city = value;
        }
    }

    public string Street
    {
        get { return _street; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Улица не может быть пустой");
            }
            _street = value;
        }
    }

    public string HouseNumber
    {
        get { return _houseNumber; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Номер дома не может быть пустым");
            }
            _houseNumber = value;
        }
    }

    public string PostalCode
    {
        get { return _postalCode; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Почтовый индекс не может быть пустым");
            }
            if (!value.All(char.IsDigit))
            {
                throw new Exception("Почтовый индекс должен состоять только из цифр");
            }
            _postalCode = value;
        }
    }

    public ResponsibilityType ResponsibilityType { get; set; }
    public int ProductId { get; set; }
    
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private bool IsPhoneNumber(string input, string pattern)
    {
        if (input.Length != pattern.Length) return false;

        for (int i = 0; i < input.Length; ++i)
        {
            bool ith_character_ok;
            if (Char.IsDigit(pattern, i))
                ith_character_ok = Char.IsDigit(input, i);
            else
                ith_character_ok = (input[i] == pattern[i]);

            if (!ith_character_ok) return false;
        }
        return true;
    }

}