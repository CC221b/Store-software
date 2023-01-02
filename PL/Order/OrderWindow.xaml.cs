using BlApi;
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
using System.Xml.Linq;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl blp;
        BO.Order order1;
        public void FillingControlsForOrderUpdate(BO.Order order)
        {
            txtID.Text = order.ID.ToString();
            txtCustomerName.Text = order.CustomerName;
            txtCustomerEmail.Text = order.CustomerEmail;
            txtCustomerAdress.Text = order.CustomerAdress;
            txtOrderDate.Text = order.OrderDate.ToString();
            txtShipDate.Text = order.ShipDate.ToString();
            txtDeliveryDate.Text = order.DeliveryDate.ToString();
            txtStatus.Text = order.Status.ToString();
            txtTotalPrice.Text = order.TotalPrice.ToString();
            itemsListView.ItemsSource = order.Items;
            if (order.Status.ToString() == "ProvidedCustomerOrder" || order.Status.ToString() == "SendOrder")
            {
                btnUpdateDeliveryDate.IsEnabled = false;
            }
            else if (order.Status.ToString() == "SendOrder")
            {
                btnUpdateShipDate.IsEnabled = false;
            }
        }

        public OrderWindow(BO.Order order, IBl bl)
        {
            InitializeComponent();
            blp = bl;
            order1 = order;
            FillingControlsForOrderUpdate(order);
        }

        private void btnUpdateShipDate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateShipDate.IsEnabled = false;
            try
            {
                blp.Order.UpdateOrderShipping(order1.ID);
                order1 = blp.Order.Get(order1.ID);
                FillingControlsForOrderUpdate(order1);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateDeliveryDate.IsEnabled = false;
            try
            {
                blp.Order.UpdateOrderDelivery(order1.ID);
                order1 = blp.Order.Get(order1.ID);
                FillingControlsForOrderUpdate(order1);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }
        private void itemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
