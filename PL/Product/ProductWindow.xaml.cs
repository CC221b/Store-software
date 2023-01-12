using BlApi;
using BlImplementation;
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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>

    public partial class ProductWindow : Window
    {
        private IBl blp;
        public BO.Product product = new BO.Product();
        public BO.ProductItem productItem = new BO.ProductItem();
        public bool isEnabledForUserProductWindow { get; set; } = true;
        public string isVisibleForUserProductWindow { get; set; } = "Hidden";
        public string isVisibleForAdminAddProductWindow { get; set; } = "Visible";
        public string isVisibleForAdminUpdateProductWindow { get; set; } = "Visible";
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            isVisibleForAdminUpdateProductWindow = "Hidden";
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            blp = bl;
            DataContext = product;
        }

        public ProductWindow(BO.Product product1, IBl bl)
        {
            InitializeComponent();
            isVisibleForAdminAddProductWindow = "Hidden";
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            blp = bl;
            product = product1;
            DataContext = product;
        }

        public ProductWindow(BO.ProductItem productItem1, IBl bl)
        {
            InitializeComponent();
            isVisibleForUserProductWindow = "Visible";
            isEnabledForUserProductWindow = false;
            isVisibleForAdminUpdateProductWindow = "Hidden";
            isVisibleForAdminAddProductWindow = "Hidden";
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            blp = bl;
            productItem = productItem1;
            DataContext = productItem;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Product.Add(product);
                MessageBox.Show("The product was added successfully!!");
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

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Product.Update(product);
                MessageBox.Show("The product was updated successfully!!");
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

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.AddProduct(MainWindow.cart, productItem.ID);
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

