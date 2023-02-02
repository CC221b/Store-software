using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private IBl blp;
        private ObservableCollection<BO.OrderForList> _orderForListCollection = new();

        public void WindowRefresh()
        {
            _orderForListCollection.Clear();
            blp.Order.GetAll().ToList().ForEach(order => _orderForListCollection.Add(order));
        }

        public OrderListWindow(IBl bl)
        {
            InitializeComponent();
            blp = bl;
            WindowRefresh();
            OrderListview.DataContext = _orderForListCollection;
        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (BO.OrderForList)OrderListview.SelectedItem;
            BO.Order order = new BO.Order();
            try
            {
                order = blp.Order.Get(item.ID);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is null)
                    MessageBox.Show(ex.Message);
                else
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
            }
            new OrderWindow(order, blp).Show();
            WindowRefresh(); 
            Close();
        }
    }
}
