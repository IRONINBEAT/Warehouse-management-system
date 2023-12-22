using System.ComponentModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WMS.app.view_model;

public class UserPageViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    public ReactiveCommand<Unit, IRoutableViewModel> Back { get; set; }
    public event PropertyChangingEventHandler? PropertyChanging;
    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public UserPageViewModel()
    {
        Back = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new MainWindowViewModel()));
    }
    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        throw new System.NotImplementedException();
    }

    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
    public RoutingState Router { get; } = new();
}