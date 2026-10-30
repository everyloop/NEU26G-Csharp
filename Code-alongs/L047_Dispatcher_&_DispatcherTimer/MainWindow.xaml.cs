using System.Diagnostics;
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
using System.Windows.Threading;

namespace L047_Dispatcher___DispatcherTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private int _counter = 0;

        private Stopwatch _stopwatch = new();

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new();


            _stopwatch.Start();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            button.Content = $"{++_counter / 10.0}\n{_stopwatch.Elapsed.TotalSeconds}";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Thread.Sleep(5000);

            //await Task.Delay(5000);
            //button.Content = "New value";

            await Task.Run(async () =>
            {
                Thread.Sleep(5000);
                //await Task.Delay(5000);
                Dispatcher.Invoke(() => button.Content = "New value");
            });
        }
    }
}