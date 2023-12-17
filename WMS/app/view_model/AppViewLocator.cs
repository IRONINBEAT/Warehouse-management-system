﻿using ReactiveUI;
using WMS.app.view;

namespace WMS.app.view_model;

public class AppViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel switch
        {
            AuthorizationViewModel context => new Authorization { DataContext = context },
            MainWindowViewModel context => new MainWindow {DataContext = context},
            ProductAddingViewModel context => new ProductAdding {DataContext = context}
        };
    }
}