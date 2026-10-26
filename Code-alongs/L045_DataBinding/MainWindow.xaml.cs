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

namespace L045_DataBinding;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new Person() { FirstName = "Anders", LastName = "Svensson" };
    }

    //private void sourceTextBox_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    destinationTextBox.Text = sourceTextBox.Text;
    //}

    //private void destinationTextBox_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    sourceTextBox.Text = destinationTextBox.Text;
    //}
}

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}