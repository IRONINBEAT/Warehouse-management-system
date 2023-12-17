using System.ComponentModel;
using ReactiveUI;

namespace WMS.app.view_model;

public class MainWindowViewModel : ViewModelBase, IRoutableViewModel, IScreen
{
    
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
    
    public RoutingState Router { get; }
}