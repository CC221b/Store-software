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
        private IBl blp;
        BO.OrderTracking orderTracking = new();
        public string isVisibleForErrorInOrderTrackingWindow { get; set; } = "Visible";

        public OrderTrackingWindow(IBl bl, int orderId1)
        {
            InitializeComponent();
            blp = bl;
            orderTracking.ID = orderId1;
            try
            {
                orderTracking = bl.Order.GetOrderTracking(orderTracking.ID);
                lblErrorMessage.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                btnOrderData.Visibility = Visibility.Hidden;
                lblErrorMessage.Content = "Sorry, no order found.";
                isVisibleForErrorInOrderTrackingWindow = "Hidden";
            }
            DataContext = orderTracking;
        }

        private void btnOrderData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = blp.Order.Get(orderTracking.ID);
                new OrderWindow(order, blp, "User").Show();
                Close();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }
    }
}
