using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
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
    public ReactiveCommand<Unit, IRoutableViewModel> AddProduct { get; }
    [Reactive] public ObservableCollection<Product> AllProducts { get; set; }

    [Reactive] public Product SelectedProduct { get; private set; }
    

    [Reactive] public int SelectedIndex { get; set; }
    

    
    

public MainWindowViewModel()
    {
        AuthorizationUseCase _authorizationUseCase = new AuthorizationUseCase(
            new AuthorizedUserRepository(
                "C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\AuthorizedUser.json"));

        ProductUseCase _productUseCase = new ProductUseCase(
            new ProductRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Products.json"));
        
        var bufferAllProducts = _productUseCase.GetAllProducts();

        AllProducts = new ObservableCollection<Product>();

        foreach (var product in bufferAllProducts)
        {
            AllProducts.Add(product);
        }
        
        AddProduct = ReactiveCommand.CreateFromObservable(() =>
        {
            if(_authorizationUseCase.GetUser().Role <= Role.SENIORSTOREKEEPER) 
                return Router.Navigate.Execute(new ProductAddingViewModel());
            return Router.Navigate.Execute(new MainWindowViewModel());
        });

        SelectedProduct = AllProducts[0];
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