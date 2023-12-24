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

public class RegistrationViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    [Reactive] public string Name { get; set; }
    
    [Reactive] public string Surname { get; set; }
    
    [Reactive] public string Patronymic { get; set; }
    
    [Reactive] public string Login { get; set; }
    
    [Reactive] public string Password { get; set; }
    
    [Reactive] public ObservableCollection<Role> Roles { get; set; } = new();
    
    [Reactive] public int SelectedIndex { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> Done { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; set; }

    public RegistrationViewModel()
    {
        UserUseCase _userUseCase = new UserUseCase(
            new UserRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Users.json"));
        
        ProductUseCase _productUseCase = new ProductUseCase(
            new ProductRepository(@"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\Products.json"));
        
        
        for (int i = 0; i < 3; i++)
        {
            Roles.Add((Role)i);
        }
        
        Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new AdminPageViewModel()));

        
        
        Done = ReactiveCommand.CreateFromObservable(() =>
        {
            User user = new User(_productUseCase.GenerateId(), Login, Name, Password, Patronymic, Surname, Roles[SelectedIndex]);
            
            if (_userUseCase.Register(user))
                return ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new AdminPageViewModel()));
            return ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new RegistrationViewModel()));
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