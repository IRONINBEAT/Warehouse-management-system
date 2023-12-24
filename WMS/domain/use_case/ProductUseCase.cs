using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
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
        if (string.IsNullOrEmpty(product.Name)) return ProductAddingErrors.NO_NAME;
        if (string.IsNullOrEmpty(product.Description)) return ProductAddingErrors.NO_DESCRIPTION;
        if (string.IsNullOrEmpty(product.Manufacturer.CompanyName)) return ProductAddingErrors.NO_MANUFACTURER;
        if (double.IsNegative(product.Dimensions.Width) || double.IsNaN(
                product.Dimensions.Width)) return ProductAddingErrors.NO_WIDTH;
        if (double.IsNegative(product.Dimensions.Height) || double.IsNaN(
                product.Dimensions.Height)) return ProductAddingErrors.NO_HEIGHT;
        if (double.IsNegative(product.Dimensions.Length) || double.IsNaN(
                product.Dimensions.Length)) return ProductAddingErrors.NO_LENGTH;
        if (int.IsNegative(product.Quantity)) return ProductAddingErrors.NO_QUANTITY;
        if (double.IsNegative(product.NetWeight) || double.IsNaN(
                product.NetWeight)) return ProductAddingErrors.NO_WEUGHT;
        
        _productRepository.Add(product);
        return ProductAddingErrors.SUCCEED;
    }

    public Product Get(int id)
    {
            var productList = _productRepository.GetAll();

            foreach (var product in productList)
                    if (product.Id == id) return product;
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
    
    public string GetEnumDescription(Enum value)
    {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    

}