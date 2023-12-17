using System;
using Microsoft.VisualBasic.CompilerServices;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.repository;

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
        if (product.Dimensions.Width == null || double.IsNegative(product.Dimensions.Width) || double.IsNaN(
                product.Dimensions.Width)) return ProductAddingErrors.NO_WIDTH;
        if (product.Dimensions.Height == null || double.IsNegative(product.Dimensions.Height) || double.IsNaN(
                product.Dimensions.Height)) return ProductAddingErrors.NO_HEIGHT;
        if (product.Dimensions.Length == null || double.IsNegative(product.Dimensions.Length) || double.IsNaN(
                product.Dimensions.Length)) return ProductAddingErrors.NO_LENGTH;
        if (int.IsNegative(product.Quantity) || product.Quantity == null) return ProductAddingErrors.NO_QUANTITY;
        if (product.NetWeight == null || double.IsNegative(product.NetWeight) || double.IsNaN(
                product.NetWeight)) return ProductAddingErrors.NO_WEUGHT;
        
        _productRepository.Add(product);
        return ProductAddingErrors.SUCCEED;
    }

    public void WriteOff(int id)
    {
        
    }

    public void SendToClient(int id)
    {
        
    }
}