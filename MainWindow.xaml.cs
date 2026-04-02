using System.Windows;
using StroySite.WPF;  

namespace StroySite.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenContractors_Click(object sender, RoutedEventArgs e)
        {
            // Теперь компилятор знает, где искать ContractorsPage
            MainFrame.Navigate(new ContractorsPage());
        }
    }
}