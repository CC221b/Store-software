using BlApi;
using BlImplementation;
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
using System.Windows.Shapes;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartListWindow.xaml
    /// </summary>
    public partial class CartListWindow : Window
    {
        private IBl blp;
        int amount = 0;
        BO.OrderItem orderItem = new();
        public CartListWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            txtChangeAmount.Visibility = Visibility.Hidden;
            cartListView.ItemsSource = MainWindow.cart.Items;
        }

        private void btnChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            txtChangeAmount.Visibility = Visibility.Visible;
        }

        private void txtChangeAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? amountBeforeTryParse = this.txtChangeAmount.Text;
            int.TryParse(amountBeforeTryParse, out amount);
        }

        private void cartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            orderItem = (BO.OrderItem)((sender as ListView).SelectedItem);
        }
    }
}
