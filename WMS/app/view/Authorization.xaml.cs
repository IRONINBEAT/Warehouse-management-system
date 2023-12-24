using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ReactiveUI;
using WMS.app.view_model;

namespace WMS.app.view;

public partial class Authorization
{

    public Authorization()
    {
        InitializeComponent();

    }
    
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        
    }
}