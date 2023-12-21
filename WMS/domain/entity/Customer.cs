using WMS.domain.enumerate;

namespace WMS.domain.entity;

public class Customer
{
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
    
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string PostalCode { get; set; }
    public ResponsibilityType ResponsibilityType { get; set; }
    public int ProductId { get; set; }
}