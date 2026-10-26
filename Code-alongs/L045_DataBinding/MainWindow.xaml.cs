using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    private Person _person;

    public MainWindow()
    {
        InitializeComponent();

        _person = new Person() { FirstName = "Anders", LastName = "Svensson" };
        DataContext = _person;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _person.FirstName = "Maria";
        _person.LastName = "Johansson";
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

// Interfacet INotifyPropertyChanged används av WPF för att en klass ska kunna meddela UI-elementet som bindar mot klassen att en property uppdaterats.
// interface gör det via ett Event PropertyChanged som det bindade UI-elementet subscribar på så den kan bli meddelad när något ändrat (och behöver ritas om).
class Person : INotifyPropertyChanged
{
    // När vi ändrar en property så kan vi meddela UI genom att göra en invoke på PropertyChanged-eventet.
    public string FirstName
    {
        get => field;  // Keyword field används för att göra en automatisk backingfield som används inom en property.
        set
        {
            field = value;
            NotifyPropertyChanged(nameof(FirstName));  // använd hellre nameof(FirstName) än hårdkodad "FirstName", då den sistnämda inte uppdateras vid refactoring/rename
        }
    } = string.Empty;


    private string? _lastName;   // Här är det äldre sättet: innan keyword 'field' fanns var man alltid tvungen att göra en egen backing field.
    public string? LastName
    {
        get { return _lastName; }
        set
        {
            _lastName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
        }
    }

    // [CallerMemberName] säger till kompilatorn att sätta värdet på propertyName till den anropande metodens/propertyns namn.
    private void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) 
    {
        // PropertyChange är null om ingen subscriber finns - därför behövs null-conditional operatorn ?.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}