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
        IBl bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
            btnShowProductsAdmin.Visibility = Visibility.Hidden;
            btnShowOrdersAdmin.Visibility = Visibility.Hidden;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            btnAdmin.Visibility = Visibility.Hidden;
            btnProducts.Visibility = Visibility.Hidden;
            btnShowProductsAdmin.Visibility = Visibility.Visible;
            btnShowOrdersAdmin.Visibility = Visibility.Visible;
        }

        private void btnShowProductsAdmin_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductListWindow(bl, "Admin").Show();
        }

        private void btnShowOrdersAdmin_Click(object sender, RoutedEventArgs e)
        {
            new Order.OrderListWindow(bl).Show();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductListWindow(bl, "User").Show();
        }
    }
}
