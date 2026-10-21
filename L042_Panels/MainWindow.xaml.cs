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

namespace L042_Panels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _stackPanelCounter = 1;
        private int _wrapPanelCounter = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButtonToStackPanel()
        {
            var newButton = new Button()
            {
                Content = ++_stackPanelCounter,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 40,
                Height = 40,
                Margin = new Thickness(10, 10, 0, 0),
            };

            newButton.Click += Button_Click;

            stackPanel.Children.Add(newButton);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (stackPanel.Children.Count % 5 == 0)
            //{
            //    _stackPanelCounter = 0;
            //    stackPanel.Children.Clear();
            //    AddButtonToStackPanel();
            //}

            if (stackPanel.Children.IndexOf((UIElement)sender) == 0)
            {
                AddButtonToStackPanel();
            }
            else if (stackPanel.Children.IndexOf((UIElement)sender) == 4)
            {
                if (stackPanel.Orientation == Orientation.Horizontal)
                {
                    stackPanel.Orientation = Orientation.Vertical;
                }
                else
                {
                    stackPanel.Orientation = Orientation.Horizontal;
                }
            }
            else
            {
                stackPanel.Children.Remove((UIElement)sender);
            }
        }

        private void WrapPanelButton_Click(object sender, RoutedEventArgs e)
        {
            var newButton = new Button()
            {
                Content = ++_wrapPanelCounter,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 40,
                Height = 40,
                Margin = new Thickness(10, 10, 0, 0),
            };

            wrapPanel.Children.Add(newButton);
        }

        private void DockPanelButton_Click(object sender, RoutedEventArgs e)
        {
            Dock dockPosition = (Dock)Random.Shared.Next(4);

            var newButton = new Button()
            {
                Content = dockPosition.ToString()[0],
            };

            // Attaced Property
            // Dock-propertyn finns inte på Button, utan sätts via DockPanel: DockPanel.SetDock(), DockPanel.GetDock(); 
            DockPanel.SetDock(newButton, dockPosition);

            //Även Grid använder Attached properties.
            //Grid.SetRow(dockPanel, 0);
            //Grid.GetRow(dockPanel);

            dockPanel.Children.Add(newButton);
        }
    }

    // Uppgift 1:
    // Uppdatera koden ovan så att räknaren också börjar om på 1 varje gång stackPanel.Children töms.

    // Uppgift 2:
    // Gör om koden så att bara första knappen lägger till nya knappar, medan om man klickar på någon annan knapp så försvinner den.

    // Uppgift 3:
    // Lägg till så att om man klickar på 5:e knappen i listan så (istället för att tömmas enligt Count%5-regeln) byter den orientation på stackPanel.


}