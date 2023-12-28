using System;
using WMS.domain.structure;

namespace WMS.domain.entity;

public class Manufacturer
{
    private string _companyName;

    public Manufacturer(string companyName)
    {
        CompanyName = companyName;
    }

    public Address Address { get; set; }

    public string CompanyName
    {
        get { return _companyName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Имя компании не может быть пустым или состоять только из пробелов");
            }
            _companyName = value;
        }
    }

    public string Email { get; set; }
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
}