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

        public ObservableCollection<BO.ProductForList> productForListCollection
        {
            get { return _productForListCollection; }
            set { _productForListCollection = value; }
        }

        private ObservableCollection<BO.ProductItem> _productItemCollection = new();

        public ObservableCollection<BO.ProductItem> productItemCollection
        {
            get { return _productItemCollection; }
            set { _productItemCollection = value; }
        }


        public ProductListWindow(IBl bl, string status1)
        {
            InitializeComponent();
            status = status1;
            blp = bl;
            if (status == "Admin")
            {
                ProductItemsListview.Visibility = Visibility.Hidden;
                _productForListCollection = new ObservableCollection<BO.ProductForList>(blp.Product.GetAll());
                btnGoToCart.Visibility = Visibility.Hidden;
                ProductForListListview.DataContext = _productForListCollection;
            }
            else
            {
                ProductForListListview.Visibility = Visibility.Hidden;
                btnAddProduct.Visibility = Visibility.Hidden;
                _productItemCollection = new ObservableCollection<BO.ProductItem>(blp.Product.GetCatalog());
                ProductItemsListview.DataContext = _productItemCollection;
            }
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Categories));
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)CategorySelector.SelectedItem;
            if (status == "Admin")
                ProductForListListview.ItemsSource =
                    blp.Product.GetAll(item => (item.Category == null ? null : (int)item.Category) == Convert.ToInt32(selectedCategory));
            ProductItemsListview.ItemsSource =
                blp.Product.GetCatalog(item => (item.Category == null ? null : (int)item.Category) == Convert.ToInt32(selectedCategory));
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (status == "Admin")
            {
                BO.ProductForList item = (BO.ProductForList)ProductForListListview.SelectedItem;
                try
                {
                    BO.Product product = blp.Product.Get(item.ID);
                    new ProductWindow(product, blp).ShowDialog();
                    _productForListCollection = new ObservableCollection<BO.ProductForList>(blp.Product.GetAll());
                    ProductForListListview.DataContext = _productForListCollection;
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
                BO.ProductItem item = (BO.ProductItem)ProductItemsListview.SelectedItem;
                try
                {
                    new ProductWindow(item, blp).ShowDialog();
                    _productItemCollection = new ObservableCollection<BO.ProductItem>(blp.Product.GetCatalog());
                    ProductItemsListview.DataContext = _productItemCollection;
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
            _productForListCollection = new ObservableCollection<BO.ProductForList>(blp.Product.GetAll());
            DataContext = _productForListCollection;
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            new Cart.CartWindow(blp).ShowDialog();
        }
    }
}
