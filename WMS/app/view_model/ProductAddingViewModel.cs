using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows;
using System.Windows.Media.Imaging;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class ProductAddingViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private ProductUseCase _productUseCase;
    private QRCodeUseCase _qrCodeUseCase;
    private AuthorizationUseCase _authorizationUseCase;
    [Reactive] public string Name { get; set; }
    [Reactive] public string Description { get; set; }
    [Reactive] public string Manufacturer { get; set; }
    [Reactive] public double Width { get; set; }
    [Reactive] public double Height { get; set; }
    [Reactive] public double Length { get; set; }
    [Reactive] public double Weight { get; set; }
    [Reactive] public int Quantity { get; set; }
    [Reactive] public ObservableCollection<ProductType> Type { get; set; } = new();
    [Reactive] public int SelectedIndex { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> Done { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; }

    
    public ProductAddingViewModel(Product product)
    {
        _productUseCase = new ProductUseCase(
            new ProductRepository(@"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\Products.json"));
        
        _authorizationUseCase = new AuthorizationUseCase(
            new AuthorizedUserRepository(
                @"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\AuthorizedUser.json"));

        _qrCodeUseCase = new QRCodeUseCase();
        
        for (int i = 0; i < Enum.GetNames(typeof(ProductType)).Length; i++)
            Type.Add((ProductType)i);
        
        Name = product.Name;
        Description = product.Description;
        Manufacturer = product.Manufacturer.CompanyName;
        Width = product.Dimensions.Width;
        Height = product.Dimensions.Height;
        Length = product.Dimensions.Length;
        Weight = product.NetWeight;
        Quantity = product.Quantity;
        
        
        Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new MainWindowViewModel()));
        
        Done = ReactiveCommand.CreateFromObservable(() =>
        {

            try
            {
                Product newProduct = new Product("21541235661", product.Id, Manufacturer, Name, Quantity,
                    Description, Width, Height, Length, Type[SelectedIndex],
                    Weight, _authorizationUseCase.GetUser());

                if (_productUseCase.ValidateProductInfo(newProduct) == ProductAddingErrors.SUCCEED &&
                    Name == product.Name)
                {
                    _productUseCase.Change(newProduct);
                    return Router.Navigate.Execute(new MainWindowViewModel());
                }

                MessageBox.Show("Имя товара не соответствует исходному или иная информация введена неверно");
                return Router.Navigate.Execute(new ProductAddingViewModel(product));
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return Router.Navigate.Execute(new ProductAddingViewModel(product));
            }
            
        });
    }

    public ProductAddingViewModel()
    {
        for (int i = 0; i < Enum.GetNames(typeof(ProductType)).Length; i++)
            Type.Add((ProductType)i);

        _productUseCase = new ProductUseCase(
            new ProductRepository(@"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\Products.json"));

        _authorizationUseCase = new AuthorizationUseCase(
            new AuthorizedUserRepository(
                @"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\AuthorizedUser.json"));

        _qrCodeUseCase = new QRCodeUseCase();

        Done = ReactiveCommand.CreateFromObservable(() =>
        {
            try
            {
                Product product = new Product("21541235661", _productUseCase.GenerateId(), Manufacturer, Name, Quantity,
                    Description, Width, Height, Length, Type[SelectedIndex],
                    Weight, _authorizationUseCase.GetUser());
                product.Status = ProductStatus.IN_STOCK;
                _productUseCase.Add(product);
                _qrCodeUseCase.Generate(product);
                return Router.Navigate.Execute(new MainWindowViewModel());
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return Router.Navigate.Execute(new ProductAddingViewModel());
            }
        });

    Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new MainWindowViewModel()));
    }
    
    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; } = new();
}