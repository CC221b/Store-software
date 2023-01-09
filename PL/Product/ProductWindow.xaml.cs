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
        public void ManagingUserControls()
        {
            lblAmountInCart.Visibility = Visibility.Hidden;
            txtAmountInCart.Visibility = Visibility.Hidden;
            btnAddToCart.Visibility = Visibility.Hidden;
        }
        public ProductWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            this.DataContext = product;
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            btnUpdateProduct.Visibility = Visibility.Hidden;
            ManagingUserControls();
        }

        public ProductWindow(BO.Product product, IBl bl)
        {
            blp = bl;
            InitializeComponent();
            ManagingUserControls();
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            this.DataContext = product;
            btnAddProduct.Visibility = Visibility.Hidden;
        }

        public ProductWindow(BO.ProductItem productItem, IBl bl)
        {
            blp = bl;
            InitializeComponent();
            btnUpdateProduct.Visibility = Visibility.Hidden;
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            this.DataContext = productItem;
            btnAddProduct.Visibility = Visibility.Hidden;
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
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
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
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.AddProduct(MainWindow.cart, product.ID);
                this.Close();
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
}

