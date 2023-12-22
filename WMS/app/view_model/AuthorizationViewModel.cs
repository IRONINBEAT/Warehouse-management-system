using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Printing;
using System.Reactive;
using System.Windows;
using DynamicData.Tests;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.enumerate;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class AuthorizationViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public RoutingState Router { get; } = new();
    [Reactive] public string Login { get; set; }
    [Reactive] public string Password { private get; set; }
    
    public ReactiveCommand<Unit, IRoutableViewModel> ToNavigate { get; }
    public ReactiveCommand <Unit, Unit> ForgotPassword { get; }
    
    [Reactive] public bool IsForgot { get; set; }
    [Reactive] public bool IsAuthorized { get; set; } = true;

    public AuthorizationViewModel()
    {
        ToNavigate = ReactiveCommand.CreateFromObservable(() =>
        {
            UserRepository _userRepository =
                new UserRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Users.json");
            UserUseCase _userUseCase =
                new UserUseCase( _userRepository);

            AuthorizationUseCase _authorizationUseCase = new AuthorizationUseCase(
                new AuthorizedUserRepository(
                    @"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\AuthorizedUser.json"));

            if (IsAuthorized = _userUseCase.Authorize(Login, Password))
            {
                List <User> users = _userRepository.Download();

                foreach (var user in users)
                {
                    if (user.Login == Login)
                    {
                        _authorizationUseCase.SaveUser(user);
                        break;
                    }
                }
                if (_authorizationUseCase.GetUser().Role == Role.ADMIN)
                    return Router.Navigate.Execute(new AdminPageViewModel());
                return Router.Navigate.Execute(new MainWindowViewModel());
            }
            return Router.Navigate.Execute(new AuthorizationViewModel());
        });
    }

    
    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new NotImplementedException();
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}