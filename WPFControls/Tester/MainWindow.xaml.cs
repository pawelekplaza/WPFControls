using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Tester
{    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnMainWindowLoaded;
        }

        private async void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            var x = new List<string>();
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 2_000_000; i++)
                    x.Add($"Item no. { i + 1 }");
            });            
            list.ItemsSource = x;
        }
    }
}
