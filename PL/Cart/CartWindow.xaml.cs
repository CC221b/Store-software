using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using static System.Windows.Forms.AxHost;

namespace PL.Cart;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private IBl blp;
    int amount = 0;
    double totalPrice = 0;
    BO.OrderItem orderItem = new();
    public string stateUpdatingItemInOrder { get; set; } = "Hidden";
    public string stateUpdatingAmountOfItemInOrder { get; set; } = "Hidden";

    private ObservableCollection<BO.OrderItem> _orderItemCollection = new();

    public CartWindow(IBl bl)
    {
        InitializeComponent();
        blp = bl;
        MainWindow.cart?.Items?.ToList().ForEach(item => _orderItemCollection.Add(item ?? new BO.OrderItem()));
        //cartListView.DataContext = _orderItemCollection;
        //txtTotalPrice.DataContext = totalPrice;
        //txtChangeAmount.DataContext = amount;
        totalPrice = MainWindow.cart != null ? MainWindow.cart.TotalPrice : 0;
        DataContext = new
        {
            StateUpdatingItemInOrder = stateUpdatingItemInOrder,
            StateUpdatingAmountOfItemInOrder = stateUpdatingAmountOfItemInOrder,
            cartListView = _orderItemCollection,
            txtChangeAmount = amount
        };
    }

    private void btnChangeAmount_Click(object sender, RoutedEventArgs e)
    {
        stateUpdatingAmountOfItemInOrder = "Visible";
    }

    private void cartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        orderItem = (BO.OrderItem)cartListView.SelectedItem;
        DataContext = new
        {
            StateUpdatingItemInOrder = "Visible",
            StateUpdatingAmountOfItemInOrder = stateUpdatingAmountOfItemInOrder,
            cartListView = _orderItemCollection,
            txtChangeAmount = amount
        };
      
    }

    private void btnOkChangeAmount_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            blp.Cart.UpdateAmountOfProduct(MainWindow.cart, orderItem.ProductID, amount);
            txtChangeAmount.Text = "";
            stateUpdatingAmountOfItemInOrder = "Hidden";
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

public class IsVisibleForUpdatingItemInOrder : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "Hidden")
        {
            return Visibility.Hidden;
        }
        else
        {
            return Visibility.Visible;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IsVisibleForUpdatingAmountOfItemInOrder : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "Hidden")
        {
            return Visibility.Hidden;
        }
        else
        {
            return Visibility.Visible;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
