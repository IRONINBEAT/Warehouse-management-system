using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Reactive;
using System.Reactive.Linq;
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
    public ReactiveCommand<Unit, Unit> ForgotPassword { get; }

    [Reactive] public bool IsForgot { get; set; }
    [Reactive] public bool IsAuthorized { get; set; } = true;

    public AuthorizationViewModel()
    {
        ForgotPassword = ReactiveCommand.Create(() =>
        {
            MessageBox.Show("Позовите администратора", "Забыли пароль",
                MessageBoxButton.OK, MessageBoxImage.Question);
        });

        ToNavigate = ReactiveCommand.CreateFromObservable(() =>
        {
            UserRepository _userRepository =
                new UserRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Users.json");
            UserUseCase _userUseCase =
                new UserUseCase(_userRepository);

            AuthorizationUseCase _authorizationUseCase = new AuthorizationUseCase(
                new AuthorizedUserRepository(
                    @"C:\Users\IRONIN\RiderProjects\WMS\WMS\data\data_set\AuthorizedUser.json"));
            try
            {
                User foundUser = _userUseCase.Authorize(Login, Password);
                if (foundUser != null)
                {
                    _authorizationUseCase.SaveUser(foundUser);

                    if (_authorizationUseCase.GetUser().Role == Role.ADMIN)
                    {
                        return Router.Navigate.Execute(new AdminPageViewModel());
                    }
                    return Router.Navigate.Execute(new MainWindowViewModel());
                }
                MessageBox.Show("Пользователь с указанным логином или паролем не найден.");
                return Router.Navigate.Execute(new AuthorizationViewModel());
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
                return Router.Navigate.Execute(new AuthorizationViewModel());
            }
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