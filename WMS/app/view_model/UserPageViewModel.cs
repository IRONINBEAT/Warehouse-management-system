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

public class UserPageViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    [Reactive] public string AuthorizedUserFullName { get; set; }
    [Reactive] public string AuthorizedUserRole { get; set; }
    
    [Reactive] public Role AuthorizedUserRoleEnum { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> Quit { get; set; }
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
        AuthorizedUserRoleEnum = role;
        AuthorizedUserRole = _userUseCase.GetEnumDescription(role);

        Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new MainWindowViewModel()));
        
        Quit = ReactiveCommand.CreateFromObservable(() =>
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из системы?","Выход",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                return Router.Navigate.Execute(new AuthorizationViewModel());
            return Router.Navigate.Execute(new UserPageViewModel(AuthorizedUserFullName, AuthorizedUserRoleEnum));
        });
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