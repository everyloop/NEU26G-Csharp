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

namespace L043_Kontroller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var countries = new Country[] 
            { 
                new Country("Sweden", "Stockholm"),
                new Country("Norway", "Oslo"),
                new Country("Denmark", "Copenhagen")
            };

            countriesComboBox.ItemsSource = countries;
            countriesComboBox.SelectedIndex = 0;
            countriesComboBox.DisplayMemberPath = "Name";

            listBox.ItemsSource = countries;
            listBox.DisplayMemberPath = "Capital";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            redRun.Text = sourceTextBox.Text;
            copyButton.IsEnabled = (sourceTextBox.Text != "");
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                anotherRun.Text = destinationTextBox.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //copyButton.IsEnabled = (DisabledRadioButton.IsChecked == false);

            if (appendRadionButton.IsChecked == true)
            {
                destinationTextBox.Text += sourceTextBox.Text;
            }
            else if (overwriteRadioButton.IsChecked == true)
            {
                destinationTextBox.Text = sourceTextBox.Text;
            }
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                destinationTextBox.HorizontalScrollBarVisibility = destinationTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                destinationTextBox.HorizontalScrollBarVisibility = destinationTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }

        private void countriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            capitalTextBox.Text = (countriesComboBox.SelectedItem as Country)?.Capital;
            listBox.SelectedItem = countriesComboBox.SelectedItem;
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