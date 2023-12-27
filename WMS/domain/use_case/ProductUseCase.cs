using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.VisualBasic.CompilerServices;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.repository;
using ZXing;
using ZXing.Presentation;

namespace WMS.domain.use_case;

public class ProductUseCase : IProduct
{
    private ProductRepository _productRepository;

    public ProductUseCase(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ProductAddingErrors Add(Product product)
    {
        var result = ValidateProductInfo(product);
        if (result == ProductAddingErrors.SUCCEED)
            _productRepository.Add(product);
        return result;
    }

    public void Change(Product product)
    {
        _productRepository.Change(product);
    }

    public Product Get(int id)
    {
        var productList = _productRepository.GetAll();

        foreach (var product in productList)
            if (product.Id == id)
                return product;
        return productList[0];
    }

    public List<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }

    public void WriteOff(Product product)
    {
        _productRepository.Remove(product);
    }

    public int GenerateId()
    {
        DateTime centuryBegin = new DateTime(2021, 4, 29);
        DateTime currentDate = DateTime.Now;
        string str = (currentDate.Ticks - centuryBegin.Ticks).ToString().Substring(7, 4);
        return Convert.ToInt32(str);
    }

    public ProductAddingErrors ValidateProductInfo(Product product)
    {
        if (string.IsNullOrEmpty(product.Name) || char.IsDigit(product.Name[0])) return ProductAddingErrors.NO_NAME;
        if (string.IsNullOrEmpty(product.Description)) return ProductAddingErrors.NO_DESCRIPTION;
        if (string.IsNullOrEmpty(product.Manufacturer.CompanyName)) return ProductAddingErrors.NO_MANUFACTURER;
        if (double.IsNegative(product.Dimensions.Width) || double.IsNaN(
                product.Dimensions.Width) || product.Dimensions.Width == 0) return ProductAddingErrors.NO_WIDTH;
        if (double.IsNegative(product.Dimensions.Height) || double.IsNaN(
                product.Dimensions.Height) || product.Dimensions.Height == 0) return ProductAddingErrors.NO_HEIGHT;
        if (double.IsNegative(product.Dimensions.Length) || double.IsNaN(
                product.Dimensions.Length) || product.Dimensions.Length == 0) return ProductAddingErrors.NO_LENGTH;
        if (int.IsNegative(product.Quantity) || product.Quantity == 0) return ProductAddingErrors.NO_QUANTITY;
        if (double.IsNegative(product.NetWeight) || double.IsNaN(
                product.NetWeight) || product.NetWeight == 0) return ProductAddingErrors.NO_WEUGHT;
        return ProductAddingErrors.SUCCEED;
    }

    public string GetEnumDescription(Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}