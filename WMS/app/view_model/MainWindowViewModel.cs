using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.app.view;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class MainWindowViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private QRCodeUseCase _qrCodeUseCase;
    private ProductUseCase _productUseCase;
    public ReactiveCommand<Unit, IRoutableViewModel> AddProduct { get; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> SendToClient { get; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> WriteOff { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel> PersonalAccount { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> ChangeInfo { get; set; }
    
    [Reactive] public User AuthorizedUser { get; set; }

    [Reactive] public BitmapImage QrCode { get; set; }
    [Reactive] public string Status { get; set; }

    [Reactive] public ObservableCollection<Product> AllProducts { get; set; }
    
    [Reactive] public string ProductType { get; set; }

    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            ProductType = _productUseCase.GetEnumDescription(_selectedProduct.Type);
            QrCode = _qrCodeUseCase.Load(_selectedProduct);
            Status = _productUseCase.GetEnumDescription(_selectedProduct.Status);
            OnPropertyChanged(nameof(QrCode));
            OnPropertyChanged(nameof(SelectedProduct));
            OnPropertyChanged(nameof(ProductType));
            OnPropertyChanged(nameof(Status));
        }
    }

    
     





    public MainWindowViewModel()
    {
        
        
        AuthorizationUseCase _authorizationUseCase = new AuthorizationUseCase(
            new AuthorizedUserRepository(
                "C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\AuthorizedUser.json"));

        AuthorizedUser = _authorizationUseCase.GetUser();

        _productUseCase = new ProductUseCase(
            new ProductRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Products.json"));
        
        _qrCodeUseCase = new QRCodeUseCase();
        
        var bufferAllProducts = _productUseCase.GetAllProducts();

        
        
        AllProducts = new ObservableCollection<Product>();

        foreach (var product in bufferAllProducts)
            AllProducts.Add(product);

        
        ChangeInfo = ReactiveCommand.CreateFromObservable(() =>
        {
            if (SelectedProduct != null)
            {
                if(AuthorizedUser.Role <= Role.SENIORSTOREKEEPER) 
                    return Router.Navigate.Execute(new ProductAddingViewModel(SelectedProduct));
                MessageBox.Show("Для вашей должности данная функция недоступна.");
                return Router.Navigate.Execute(new MainWindowViewModel());
            }
            MessageBox.Show("Товар не выбран.");
            return Router.Navigate.Execute(new MainWindowViewModel());
        });
        
        AddProduct = ReactiveCommand.CreateFromObservable(() =>
        {
            if(AuthorizedUser.Role <= Role.SENIORSTOREKEEPER) 
                return Router.Navigate.Execute(new ProductAddingViewModel());
            MessageBox.Show("Для вашей должности данная функция недоступна.");
            return Router.Navigate.Execute(new MainWindowViewModel());
            
        });

        PersonalAccount = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new UserPageViewModel(AuthorizedUser.GetFIO, AuthorizedUser.Role)));
        
        SendToClient = ReactiveCommand.CreateFromObservable(() =>
        {
            if (SelectedProduct != null)
            {
                if(AuthorizedUser.Role <= Role.SENIORSTOREKEEPER) 
                    return Router.Navigate.Execute(new FillingCustomerInfoViewModel(SelectedProduct.Id));
                MessageBox.Show("Для вашей должности данная функция недоступна.");
                return Router.Navigate.Execute(new MainWindowViewModel());
            }

            MessageBox.Show("Товар не выбран.");
            return Router.Navigate.Execute(new MainWindowViewModel());
        });

        WriteOff = ReactiveCommand.CreateFromObservable(() =>
        {
            if (SelectedProduct != null)
            {
                if (AuthorizedUser.Role <= Role.SENIORSTOREKEEPER)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите списать товар?","Списание товара",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _productUseCase.WriteOff(SelectedProduct);
                        return Router.Navigate.Execute(new MainWindowViewModel());
                    }
                    return Router.Navigate.Execute(new MainWindowViewModel());
                }
                MessageBox.Show("Для вашей должности данная функция недоступна.");
                return Router.Navigate.Execute(new MainWindowViewModel());
            }
            MessageBox.Show("Товар не выбран.");
            return Router.Navigate.Execute(new MainWindowViewModel());
        });

        
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