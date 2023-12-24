using System.ComponentModel;
using System.Reactive;
using ReactiveUI;

namespace WMS.app.view_model;

public class StartWindowViewModel: ViewModelBase, IRoutableViewModel, IScreen
{
    public  ReactiveCommand<Unit, IRoutableViewModel> Start { get; set; }


    public StartWindowViewModel()
    {
        Start = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new AuthorizationViewModel()));
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