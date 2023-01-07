using BlApi;
using BO;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        int orderId = 0;
        private IBl blp;
        BO.OrderTracking orderTracking = new();
        public OrderTrackingWindow(IBl bl, int orderId1)
        {
            InitializeComponent();
            orderId = orderId1;
            blp = bl;
            try
            {
                orderTracking = bl.Order.GetOrderTracking(orderId);
                orderTrackingDataGrid.ItemsSource = orderTracking.DateAndStatus;
                txtOrderID.Text = orderTracking.ID.ToString();
                txtOrderStatus.Text = orderTracking.Status.ToString();
                lblErrorMessage.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                btnOrderData.Visibility = Visibility.Hidden;
                txtOrderID.Text = orderId1.ToString();
                lblErrorMessage.Content = "Sorry, no order found.";
                lblOrderStatus.Visibility = Visibility.Hidden;
                txtOrderStatus.Visibility = Visibility.Hidden;
                orderTrackingDataGrid.Visibility = Visibility.Hidden;
            }
        }

        private void btnOrderData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = blp.Order.Get(orderId);
                new OrderWindow(order, blp, "User").Show();
                this.Close();
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
    }
}
