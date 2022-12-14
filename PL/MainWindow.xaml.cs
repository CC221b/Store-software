using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int debily = 0;
        IBl bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
            btnShowProducts.Visibility = Visibility.Hidden;
            btnShowOrders.Visibility = Visibility.Hidden;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            btnAdmin.Visibility = Visibility.Hidden;
            btnShowProducts.Visibility = Visibility.Visible;
            btnShowOrders.Visibility = Visibility.Visible;
        }

        private void btnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductListWindow(bl).Show();
        }

        private void btnShowOrders_Click(object sender, RoutedEventArgs e)
        {
            new Order.OrderListWindow(bl).Show();
        }
    }
}
