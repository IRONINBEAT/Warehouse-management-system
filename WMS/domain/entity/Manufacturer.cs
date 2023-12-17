using WMS.domain.structure;

namespace WMS.domain.entity;

public class Manufacturer
{
    public Manufacturer(string companyName)
    {
        CompanyName = companyName;
    }
    public Address Address { get; set; }
    
    public string CompanyName{ get; set; }
    public string Email{ get; set; }
    public int Id{ get; set; }
    public string PhoneNumber{ get; set; }
}