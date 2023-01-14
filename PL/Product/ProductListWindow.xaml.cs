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

        public ProductListWindow(IBl bl, string status1)
        {
            InitializeComponent();
            status = status1;
            blp = bl;
            if (status == "Admin")
            {
                ProductItemsListview.Visibility = Visibility.Hidden;
                blp.Product.GetAll().ToList().ForEach(product => _productForListCollection.Add(product));
                ProductForListListview.DataContext = _productForListCollection;
            }
            else
            {
                ProductForListListview.Visibility = Visibility.Hidden;
                blp.Product.GetCatalog().ToList().ForEach(product => _productItemCollection.Add(product));
                ProductItemsListview.DataContext = _productItemCollection;
            }
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Categories));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)CategorySelector.SelectedItem;
            if (status == "Admin")
            {
                _productForListCollection.Clear();
                blp.Product.GetAll(item => (item.Category == null ? null : (int)item.Category) == Convert.ToInt32(selectedCategory))
                    .ToList().ForEach(product => _productForListCollection.Add(product));
            }
            else
            {
                _productItemCollection.Clear();
                blp.Product.GetCatalog(item => (item.Category == null ? null : (int)item.Category) == Convert.ToInt32(selectedCategory))
                    .ToList().ForEach(product => _productItemCollection.Add(product));
            }
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
                    _productForListCollection.Clear();
                    blp.Product.GetAll().ToList().ForEach(product => _productForListCollection.Add(product));
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
                    _productItemCollection.Clear();
                    blp.Product.GetCatalog().ToList().ForEach(product => _productItemCollection.Add(product));
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
            _productForListCollection.Clear();
            blp.Product.GetAll().ToList().ForEach(product => _productForListCollection.Add(product));
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            new Cart.CartWindow(blp).ShowDialog();
            MainWindow.cart = new();
            MainWindow.cart.Items = new();
        }
    }
}
