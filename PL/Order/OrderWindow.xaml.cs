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

        public OrderWindow(BO.Order order, IBl bl, string status = "")
        {
            InitializeComponent();
            blp = bl;
            order1 = order;
            DataContext = order1;
            if (status == "User")
            {
                btnUpdateDeliveryDate.Visibility = Visibility.Hidden;
                btnUpdateShipDate.Visibility = Visibility.Hidden;
            }
        }

        private void btnUpdateShipDate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateShipDate.IsEnabled = false;
            try
            {
                blp.Order.UpdateOrderShipping(order1.ID);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        private void btnUpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateDeliveryDate.IsEnabled = false;
            try
            {
                blp.Order.UpdateOrderDelivery(order1.ID);
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
