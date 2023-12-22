using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class UserPageViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    [Reactive] public string AuthorizedUserFullName { get; set; }
    [Reactive] public string AuthorizedUserRole { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; set; }
    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public UserPageViewModel(string fullName, Role role)
    {
        UserUseCase _userUseCase = new UserUseCase(
            new UserRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Users.json"));
        AuthorizedUserFullName = fullName;
        AuthorizedUserRole = _userUseCase.GetEnumDescription(role);

        Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new MainWindowViewModel()));
    }
    
    public UserPageViewModel()
    {
        
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; } = new();
}