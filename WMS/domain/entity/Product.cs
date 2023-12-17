﻿using System.Text.Json.Serialization;
using WMS.domain.enumerate;
using WMS.domain.structure;
using ZXing.QrCode.Internal;

namespace WMS.domain.entity;

public class Product
{
    [JsonConstructor]
    public Product(string code, int id, string nameOfTheCompany, string name, int quantity, string description, double width, double height,
        double length, ProductType type, double netWeight)
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
    }
    
    public string Code { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public Dimension Dimensions { get; set; }
    public int Id { get; set; }
    public User IntegratingPerson { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public QRCode QrCode { get; set; }
    public double NetWeight { get; set; }
}