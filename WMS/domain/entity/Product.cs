using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;
using ReactiveUI.Fody.Helpers;
using WMS.domain.enumerate;
using WMS.domain.structure;
using ZXing.QrCode.Internal;

namespace WMS.domain.entity;

public class Product
{
    private string _code;
    private int _quantity;
    private string _description;
    private string _name;
    private double _netWeight;

    public Product()
    {

    }

    public Product(string code, int id, string nameOfTheCompany, string name, int quantity, string description, double width, double height,
        double length, ProductType type, double netWeight, User user)
    {
        Dimensions = new Dimension(width, height, length);
        Manufacturer = new Manufacturer(nameOfTheCompany);
        Type = type;

        Code = code;
        Id = id;
        Name = name;
        Quantity = quantity;
        Description = description;
        NetWeight = netWeight;
        IntegratingPerson = user;
    }
    public string Code
    {
        get { return _code; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Код не может быть пустым или состоять только из пробелов");
            }
            _code = value;
        }
    }

    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (value <= 0)
            {
                throw new Exception("Количество не может быть отрицательным или нулевым");
            }
            _quantity = value;
        }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public Dimension Dimensions { get; set; }
    public int Id { get; set; }
    public User IntegratingPerson { get; set; }
    public Manufacturer Manufacturer { get; set; }

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value) || char.IsDigit(value[0]))
            {
                throw new Exception("Некорректное имя");
            }
            _name = value;
        }
    }

    public ProductType Type { get; set; }

    public double NetWeight
    {
        get { return _netWeight; }
        set
        {
            if (value <= 0)
            {
                throw new Exception("Вес не может быть отрицательным или нулевым");
            }
            _netWeight = value;
        }
    }

    public ProductStatus Status { get; set; }
}