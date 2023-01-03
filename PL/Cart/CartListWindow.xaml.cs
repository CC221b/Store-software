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
        BO.ProductItem productItem = new();
        public CartListWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            txtChangeAmount.Visibility = Visibility.Hidden;
            cartDataGrid.ItemsSource = MainWindow.cart.Items;
        }

        private void cartDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            //productItem = (BO.ProductItem)((sender as DataGrid).SelectedItem);
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
    }
}
