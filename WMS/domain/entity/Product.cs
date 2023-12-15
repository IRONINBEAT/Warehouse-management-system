using WMS.domain.enumerate;
using WMS.domain.structure;
using ZXing.QrCode.Internal;

namespace WMS.domain.entity;

public class Product
{
    
    
    
    
    public string code;
    public int cuantityOfProducts;
    public string description;
    public Dimension dimensions;
    public int id;
    public User integratingPerson;
    public Manufacturer manufacturer;
    public string name;
    public ProductType productType;
    public QRCode qrCode;
    public double netWeight;
}