using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using BlApi;
using BlImplementation;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl blp;
        string status = "";
        private ObservableCollection<BO.ProductForList> _productForListCollection = new();
        private ObservableCollection<BO.ProductItem> _productItemCollection = new();

        public void WindowProductForListRefresh(Func<DO.Product, bool>? func = null)
        {
            _productForListCollection.Clear();
            blp.Product.GetAll(func).ToList().ForEach(product => _productForListCollection.Add(product));
        }

        public void WindowProductItemsRefresh(Func<DO.Product, bool>? func = null)
        {
            _productItemCollection.Clear();
            blp.Product.GetCatalog(func).ToList().ForEach(product => _productItemCollection.Add(product));
        }

        public ProductListWindow(IBl bl, string status1)
        {
            InitializeComponent();
            status = status1;
            blp = bl;
            if (status == "Admin")
            {
                ProductItemsListview.Visibility = Visibility.Hidden;
                WindowProductForListRefresh();
                ProductForListListview.DataContext = _productForListCollection;
            }
            else
            {
                ProductForListListview.Visibility = Visibility.Hidden;
                WindowProductItemsRefresh();
                ProductItemsListview.ItemsSource = _productItemCollection;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductItemsListview.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
                view.GroupDescriptions.Add(groupDescription);
            }
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Categories));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)CategorySelector.SelectedItem;
            if (status == "Admin")
                WindowProductForListRefresh(item => (item.Category == null ? null : (int)item.Category) == Convert.ToInt32(selectedCategory));
            else
                WindowProductItemsRefresh(item => (item.Category == null ? null : (int)item.Category) == Convert.ToInt32(selectedCategory));
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (status == "Admin")
            {
                try
                {
                    BO.ProductForList item = (BO.ProductForList)ProductForListListview.SelectedItem;
                    BO.Product product = blp.Product.Get(item.ID);
                    new ProductWindow(product, blp).ShowDialog();
                    WindowProductForListRefresh();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is null)
                        MessageBox.Show(ex.Message);
                    else
                        MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
            else
            {
                try
                {
                    BO.ProductItem item = (BO.ProductItem)ProductItemsListview.SelectedItem;
                    new ProductWindow(item, blp).ShowDialog();
                    WindowProductItemsRefresh();
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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(blp).ShowDialog();
            WindowProductForListRefresh();
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            new Cart.CartWindow(blp).ShowDialog();
            MainWindow.cart = new();
            MainWindow.cart.Items = new();
        }
    }
}
