using System;
using System.Windows;
using System.Windows.Controls;

namespace WalletsWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            Closing += ((MainWindowViewModel)DataContext).OnClosingCommand;
            InitializeComponent();
        }
    }
}
