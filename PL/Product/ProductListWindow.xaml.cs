using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ProductListWindow(IBl bl, string status1)
        {
            InitializeComponent();
            status = status1;
            blp = bl;
            if (status == "Admin")
            {
                ProductsListview.ItemsSource = blp.Product.GetAll();
                btnGoToCart.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAddProduct.Visibility = Visibility.Hidden;
                ProductsListview.ItemsSource = blp.Product.GetCatalog();
            }
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)CategorySelector.SelectedItem;
            if (status == "Admin")
                ProductsListview.ItemsSource = 
                    blp.Product.GetAll(item => (item.Category == null ? null : (int)item.Category) == (int)selectedCategory);
            ProductsListview.ItemsSource = 
                blp.Product.GetCatalog(item => (item.Category == null ? null : (int)item.Category) == (int)selectedCategory);
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Product product = new BO.Product();
            if (status == "Admin")
            {
                BO.ProductForList item = (BO.ProductForList)ProductsListview.SelectedItem;
                try
                {
                    product = blp.Product.Get(item.ID);
                    new Product.ProductWindow(product, blp).Show();
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
            else
            {
                BO.ProductItem item = (BO.ProductItem)ProductsListview.SelectedItem;
                try
                {
                    new Product.ProductWindow(item, blp).Show();
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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductWindow(blp).ShowDialog();
            ProductsListview.ItemsSource = blp.Product.GetAll();
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            new Cart.CartWindow(blp).ShowDialog();
        }
    }
}
