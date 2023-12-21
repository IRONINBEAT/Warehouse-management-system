using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class FillingCustomerInfoViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    
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
        CustomerUseCase _customerUseCase = new CustomerUseCase(
            new CustomerRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Customers.json"));
        
        for (int i = 0; i < 2; i++)
        {
            Type.Add((ResponsibilityType)i);
        }
        
        Done = ReactiveCommand.CreateFromObservable(() =>
        {

            Customer customer = new Customer(productId, Name, PhoneNumber, Email, Country, City, Street, HouseNumber,
                PostalCode, Type[SelectedIndex]);


            if (_customerUseCase.Add(customer) != FillingCustomerInfoErrors.SUCCEED)
            {
                return Router.Navigate.Execute(new FillingCustomerInfoViewModel(productId));
            }
                
            return Router.Navigate.Execute(new MainWindowViewModel());
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