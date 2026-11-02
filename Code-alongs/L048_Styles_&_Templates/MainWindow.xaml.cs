using System.Windows;

namespace L048_Styles___Templates;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}

class Country
{
    public string Name { get; set; }
    public string Capital { get; set; }
}