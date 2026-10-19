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

namespace L041_WPF_Intro
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello World!");
            if (sender is Button button)
            {
                button.Content += "!";
                //if (button.HorizontalAlignment == HorizontalAlignment.Left)
                //{
                //    button.HorizontalAlignment = HorizontalAlignment.Right;
                //}
                //else
                //{
                //    button.HorizontalAlignment = HorizontalAlignment.Left;
                //}
            }
        }
    }
}