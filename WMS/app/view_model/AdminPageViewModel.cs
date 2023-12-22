﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WMS.data.repository;
using WMS.domain.entity;
using WMS.domain.use_case;

namespace WMS.app.view_model;

public class AdminPageViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    private User _selectedUser;
    [Reactive] public ObservableCollection<User> AllUsers { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> AddUser { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> DeleteUser { get; }
    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));

        }
    }

    public AdminPageViewModel()
    {
        UserUseCase _userUseCase = new UserUseCase(
            new UserRepository("C:\\Users\\IRONIN\\RiderProjects\\WMS\\WMS\\data\\data_set\\Users.json"));

        var bufferAllUsers = _userUseCase.GetAllUsers();
        
        AllUsers = new ObservableCollection<User>();

        foreach (var user in bufferAllUsers)
            AllUsers.Add(user);

        Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new AuthorizationViewModel()));


        DeleteUser = ReactiveCommand.CreateFromObservable(() =>
        {
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя {SelectedUser.Login}?", "Удаление пользователя",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _userUseCase.Dismiss(SelectedUser);
                return Router.Navigate.Execute(new AdminPageViewModel());
            }

            return Router.Navigate.Execute(new AdminPageViewModel());

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