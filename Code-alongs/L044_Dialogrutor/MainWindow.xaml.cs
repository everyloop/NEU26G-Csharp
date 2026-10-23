using Microsoft.Win32;
using System.Collections.ObjectModel;
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

namespace L044_Dialogrutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Country> _countries = new ObservableCollection<Country>();

        public MainWindow()
        {
            InitializeComponent();

            countryListBox.ItemsSource = _countries;
            countryListBox.DisplayMemberPath = "Name";
        }

        private void MessageBoxButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a message!", "Message to user:");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Add another exlamation mark?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                YesNoButton.Content += "!";
            }

            //if (result == MessageBoxResult.Yes)
            //{
            //    MessageBox.Show("You selected YES!");
            //}
            //else
            //{
            //    MessageBox.Show("You selected NO!");
            //}

        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.FileName = "Test.txt";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.ShowHiddenItems = true;
            dialog.OverwritePrompt = true;
            dialog.Filter = "Office Files|*.doc;*.xls;*.ppt|All Files|*.*";
            dialog.Title = "Save title example!";

            if (dialog.ShowDialog() == true)
            {
                saveFileButton.Content = dialog.FileName;
            }
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.FileName = "Test.txt";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.ShowHiddenItems = true;
            dialog.Filter = "Office Files|*.doc;*.xls;*.ppt|All Files|*.*";
            dialog.Title = "Select file to open:";

            if (dialog.ShowDialog() == true)
            {
                openFileButton.Content = dialog.FileName;
            }
        }

        private void AddCountryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCountry();

            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //dialog.Country = "Denmark";
            //dialog.Capital = "Copenhagen";

            if (dialog.ShowDialog() == true)
            {
                _countries.Add(new Country(dialog.Country, dialog.Capital));
            }
        }
    }
}

class Country
{
    public Country(string name, string capital)
    {
        Name = name;
        Capital = capital;
    }

    public string Name { get; set; }
    public string Capital { get; set; }
}