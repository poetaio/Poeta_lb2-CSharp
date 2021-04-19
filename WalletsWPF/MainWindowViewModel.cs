using Prism.Commands;
using System.Windows.Input;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows;

namespace WalletsWPF
{
    class MainWindowViewModel
    {
        public MainViewModel MainViewModel { get; private set; }

        public MainWindowViewModel()
        {
            MainViewModel = new MainViewModel();
        }
        public async void OnClosingCommand(object sender, CancelEventArgs e)
        {
            await MainViewModel.UploadData();
        }
    }
}
