﻿using System.Windows;
using ReactiveUI;
using WMS.app.view_model;

namespace WMS.app.view;

public partial class MainWindow : ReactiveUserControl<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}