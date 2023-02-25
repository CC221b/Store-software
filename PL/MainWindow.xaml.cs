using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using SimulatorLib;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        IBl bl = BlApi.Factory.Get();
        public static BO.Cart cart = new BO.Cart();
        int orderId = 0;
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _AdminWindowVisible;
        public bool AdminWindowVisible
        {
            get { return _AdminWindowVisible; }
            set
            {
                if (_AdminWindowVisible != value)
                {
                    _AdminWindowVisible = value;
                    OnPropertyChanged(nameof(AdminWindowVisible));
                }
            }
        }

        private bool _UserWindowVisible;
        public bool UserWindowVisible
        {
            get { return _UserWindowVisible; }
            set
            {
                if (_UserWindowVisible != value)
                {
                    _UserWindowVisible = value;
                    OnPropertyChanged(nameof(UserWindowVisible));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            cart.Items = new();
            AdminWindowVisible = false;
            UserWindowVisible = true;
            DataContext = this;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminWindowVisible = true;
            UserWindowVisible = false;
        }

        private void btnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            AdminWindowVisible = false;
            UserWindowVisible = true;
        }

        private void btnShowProductsAdmin_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductListWindow(bl, "Admin").Show();
            AdminWindowVisible = false;
            UserWindowVisible = true;
        }

        private void btnShowOrdersAdmin_Click(object sender, RoutedEventArgs e)
        {
            new Order.OrderListWindow(bl).Show();
            AdminWindowVisible = false;
            UserWindowVisible = true;
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductListWindow(bl, "User").Show();
        }

        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            if (orderId != 0)
            {
                new Order.OrderTrackingWindow(bl, orderId).Show();
                txtOrderTracking.Text = "";
            }
            else
            {
                MessageBox.Show("Sorry, please enter an order number Thank you!");
            }
        }

        private void txtOrderTracking_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? orderIdBeforeParse = this.txtOrderTracking.Text;
            int.TryParse(orderIdBeforeParse, out orderId);
        }

        private void btnStartSimulator_Click(object sender, RoutedEventArgs e)
        {
            new SimulatorWin.SimulatorWindow().Show();
        }
    }
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;

            if (isVisible)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


