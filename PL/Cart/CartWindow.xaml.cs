using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private IBl blp;
        int amount = 0;
        double totalPrice = 0;
        BO.OrderItem orderItem = new();
        public string isVisibleForUpdatingAnItemInAnOrder { get; set; } = "Hidden";
        public string isVisibleForUpdatingTheAmountOfAnItemInAnOrder { get; set; } = "Hidden";

        private ObservableCollection<BO.OrderItem> _orderItemCollection = new();

        public CartWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            MainWindow.cart?.Items?.ToList().ForEach(item => _orderItemCollection.Add(item ?? new BO.OrderItem()));
            cartListView.DataContext = _orderItemCollection;
            txtTotalPrice.DataContext = totalPrice;
            txtChangeAmount.DataContext = amount;
            totalPrice = MainWindow.cart != null ? MainWindow.cart.TotalPrice : 0;
        }

        private void btnChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            isVisibleForUpdatingTheAmountOfAnItemInAnOrder = "Visible";
        }

        private void cartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            orderItem = (BO.OrderItem)cartListView.SelectedItem;
            isVisibleForUpdatingAnItemInAnOrder = "Visible";
        }

        private void btnOkChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.UpdateAmountOfProduct(MainWindow.cart, orderItem.ProductID, amount);
                txtChangeAmount.Text = "";
                isVisibleForUpdatingTheAmountOfAnItemInAnOrder = "Hidden";
                _orderItemCollection.Clear();
                MainWindow.cart?.Items?.ToList().ForEach(item => _orderItemCollection.Add(item ?? new BO.OrderItem()));
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.UpdateAmountOfProduct(MainWindow.cart, orderItem.ProductID, 0);
                MessageBox.Show("The item remove from the cart successfully!!");
                _orderItemCollection.Clear();
                MainWindow.cart?.Items?.ToList().ForEach(item => _orderItemCollection.Add(item ?? new BO.OrderItem()));
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow(blp).ShowDialog();
            Close();
        }
    }
}
