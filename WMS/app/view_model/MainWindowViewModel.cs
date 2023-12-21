using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Windows;
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
    private ProductUseCase _productUseCase;
    public ReactiveCommand<Unit, IRoutableViewModel> AddProduct { get; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> SendToClient { get; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> WriteOff { get; set; }
    
    [Reactive] public User AuthorizedUser { get; set; }
    [Reactive] public string ProductType => _productUseCase.GetEnumDescription(SelectedProduct.Type);
    

    [Reactive] public ObservableCollection<Product> AllProducts { get; set; }

    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get { return _selectedProduct; }
        set
        {
            _selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
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
        
        
        var bufferAllProducts = _productUseCase.GetAllProducts();

        

        AllProducts = new ObservableCollection<Product>();

        foreach (var product in bufferAllProducts)
            AllProducts.Add(product);
            

        SelectedProduct = AllProducts[0];
        
        
        AddProduct = ReactiveCommand.CreateFromObservable(() =>
        {
            if(AuthorizedUser.Role <= Role.SENIORSTOREKEEPER) 
                return Router.Navigate.Execute(new ProductAddingViewModel());
            return Router.Navigate.Execute(new MainWindowViewModel());
        });
        
        SendToClient = ReactiveCommand.CreateFromObservable(() =>
        {
            if(AuthorizedUser.Role <= Role.SENIORSTOREKEEPER) 
                return Router.Navigate.Execute(new FillingCustomerInfoViewModel(SelectedProduct.Id));
            return Router.Navigate.Execute(new MainWindowViewModel());
        });

        WriteOff = ReactiveCommand.CreateFromObservable(() =>
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите списать товар?","Списание товара",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _productUseCase.WriteOff(SelectedProduct);
                return Router.Navigate.Execute(new MainWindowViewModel());
            }
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