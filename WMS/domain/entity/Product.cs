using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using ReactiveUI.Fody.Helpers;
using WMS.domain.enumerate;
using WMS.domain.structure;
using ZXing.QrCode.Internal;

namespace WMS.domain.entity;

public class Product
{
    public Product()
    {
        
    }
    public Product(string code, int id, string nameOfTheCompany, string name, int quantity, string description, double width, double height,
        double length, ProductType type, double netWeight, User user)
    {
        Dimensions = new Dimension(width, height, length);
        Manufacturer = new Manufacturer(nameOfTheCompany);
        
        Code = code;
        Id = id;
        Name = name;
        Quantity = quantity;
        Description = description;
        Type = type;
        NetWeight = netWeight;
        IntegratingPerson = user;
    }
    
    public string Code { get; set; }
    public int Quantity { get; set; }
    [Reactive] public string Description { get; set; }
    [Reactive] public Dimension Dimensions { get; set; }
    [Reactive] public int Id { get; set; }
    public User IntegratingPerson { get; set; }
    [Reactive] public Manufacturer Manufacturer { get; set; }
    [Reactive] public string Name { get; set; }
    [Reactive] public ProductType Type { get; set; }
    //public QRCode QrCode { get; set; }
    public double NetWeight { get; set; }

    
}