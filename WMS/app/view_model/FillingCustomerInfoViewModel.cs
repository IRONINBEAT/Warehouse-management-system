using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class FillingCustomerInfoViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private ProductUseCase _productUseCase;
    [Reactive] public string Name { get; set; }
    [Reactive] public string PhoneNumber { get; set; }
    [Reactive] public string Email { get; set; }
    [Reactive] public string Country { get; set; }
    [Reactive] public string City { get; set; }
    [Reactive] public string Street { get; set; }
    [Reactive] public string HouseNumber { get; set; }
    [Reactive] public string PostalCode { get; set; }
    

    public ReactiveCommand<Unit, IRoutableViewModel> Done { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; }
    [Reactive] public ObservableCollection<ResponsibilityType> Type { get; set; } = new();
    [Reactive] public int SelectedIndex { get; set; }

    public FillingCustomerInfoViewModel()
    {
        
    }
    
    public FillingCustomerInfoViewModel(int productId)
    {
        _productUseCase = new ProductUseCase(ProductRepository.GetInstance());
        
        CustomerUseCase _customerUseCase = new CustomerUseCase(CustomerRepository.GetInstance());
        
        for (int i = 0; i < Enum.GetNames(typeof(ResponsibilityType)).Length; i++)
        {
            Type.Add((ResponsibilityType)i);
        }
        
        Done = ReactiveCommand.CreateFromObservable(() =>
        {
            try
            {
                Customer customer = new Customer(productId, Name, PhoneNumber, Email, Country, City, Street,
                    HouseNumber,
                    PostalCode, Type[SelectedIndex]);
                var currentProduct = _productUseCase.Get(productId);
                currentProduct.Status = ProductStatus.ON_THE_WAY_TO_CLIENT;
                _productUseCase.Change(currentProduct);
                _customerUseCase.Add(customer);
                return Router.Navigate.Execute(new MainWindowViewModel());
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return Router.Navigate.Execute(new FillingCustomerInfoViewModel(productId));
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