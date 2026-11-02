using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace L049_Windows_in_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel = new ViewModel();

            _viewModel.OpenSettings += _viewModel_OpenSettings;
        }

        private void _viewModel_OpenSettings(object? sender, EventArgs e)
        {
            var dialog = new SettingsDialog()
            {
                DataContext = _viewModel,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (dialog.ShowDialog() == true)
            {

            }

        }
    }
}