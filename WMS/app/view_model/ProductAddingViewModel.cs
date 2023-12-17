﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
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

    public ProductAddingViewModel()
    {
        Type.Add(ProductType.BUILDING_MATERIALS);
        Type.Add(ProductType.TOOLS);
        Type.Add(ProductType.ELECTRONICS);
        
        ProductUseCase _productUseCase = new ProductUseCase(
            new ProductRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Products.json"));

        

        Done = ReactiveCommand.CreateFromObservable(() =>
        {
            Product product = new Product("21541235661", 0, Manufacturer, Name, Quantity, 
                Description, Width, Height, Length, Type[SelectedIndex], Weight);
            
            if (_productUseCase.Add(product) != ProductAddingErrors.SUCCEED)
                return Router.Navigate.Execute(new ProductAddingViewModel());
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