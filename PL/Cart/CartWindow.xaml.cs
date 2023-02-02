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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace PL.Cart;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private IBl blp;
    public double totalPrice { get; set; } = 0;
    BO.OrderItem orderItem = new();
    public string stateUpdatingItemInOrder { get; set; } = "Hidden";

    private ObservableCollection<BO.OrderItem> _orderItemCollection = new();

    public void WindowRefresh()
    {
        _orderItemCollection.Clear();
        MainWindow.cart?.Items?.ToList().ForEach(item => _orderItemCollection.Add(item ?? new BO.OrderItem()));
        totalPrice = MainWindow.cart != null ? MainWindow.cart.TotalPrice : 0;
        DataContext = new
        {
            StateUpdatingItemInOrder = stateUpdatingItemInOrder,
            CartListView = _orderItemCollection,
            TotalPrice = totalPrice
        };
    }

    public CartWindow(IBl bl)
    {
        InitializeComponent();
        blp = bl;
        WindowRefresh();
    }

    private void cartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        orderItem = (BO.OrderItem)CartListView.SelectedItem;
        stateUpdatingItemInOrder = "Visible";
        WindowRefresh();
    }

    private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
    {
        new UserWindow(blp).ShowDialog();
        Close();
    }

    private void cboxUpdateItemInCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cboxUpdateItemInCart.SelectedValue.ToString() == "DeleteItemFromCart")
        {
            try
            {
                blp.Cart.UpdateAmountOfProduct(MainWindow.cart, orderItem.ProductID, 0);
                MessageBox.Show("The item remove from the cart successfully!!");
                stateUpdatingItemInOrder = "Hidden";
                WindowRefresh();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
        }
        else if (cboxUpdateItemInCart.SelectedValue.ToString() == "UpdateAmountOfItem")
        {
            new UpdateItemInCartWindow(blp, orderItem.ProductID).ShowDialog();
            stateUpdatingItemInOrder = "Hidden";
            WindowRefresh();
        }
        cboxUpdateItemInCart.SelectedItem = ClearTheSelect;
    }

}

public class IsVisibleForUpdatingItemInOrder : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string StringValue = (string)value;
        if (StringValue == "Hidden")
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

