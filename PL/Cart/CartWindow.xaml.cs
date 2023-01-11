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

        public ObservableCollection<BO.OrderItem> orderItemCollection
        {
            get { return _orderItemCollection; }
            set { _orderItemCollection = value; }
        }

        /// <summary>
        /// A constructive action, which initializes the list and the totalPrice control.
        /// and also schedule the management functions of concealment and disclosure according to what is consumed.
        /// </summary>
        /// <param name="bl"></param>
        public CartWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            _orderItemCollection = new ObservableCollection<BO.OrderItem>(MainWindow.cart.Items);
            cartListView.DataContext = _orderItemCollection;
            totalPrice = MainWindow.cart.TotalPrice;
            txtTotalPrice.DataContext = totalPrice;
        }

        private void btnChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            isVisibleForUpdatingTheAmountOfAnItemInAnOrder = "Visible";
        }

        private void txtChangeAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? amountBeforeTryParse = txtChangeAmount.Text;
            int.TryParse(amountBeforeTryParse, out amount);
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
                _orderItemCollection = new ObservableCollection<BO.OrderItem>(MainWindow.cart.Items);
                cartListView.DataContext = _orderItemCollection;
                txtTotalPrice.Text = MainWindow.cart.TotalPrice.ToString();
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
                cartListView.ItemsSource = MainWindow.cart.Items;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }
        /// <summary>
        /// Creates a screen like a new screen for entering user information to place an order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            new UserWindow(blp).ShowDialog();
            Close();
        }
    }
}
