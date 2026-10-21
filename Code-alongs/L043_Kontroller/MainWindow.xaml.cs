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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            redRun.Text = textBox.Text;
            copyButton.IsEnabled = (textBox.Text != "");
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                anotherRun.Text = textBox2.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = textBox.Text;
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                textBox2.HorizontalScrollBarVisibility = textBox2.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                textBox2.HorizontalScrollBarVisibility = textBox2.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }
    }
}