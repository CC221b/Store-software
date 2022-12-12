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

        public void FillingControlsForProductUpdate(BO.Product product)
        {
            txtID.Text = product.ID.ToString();
            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            txtInStock.Text = product.InStock.ToString();
            cboxCategory.Text = product.Category.ToString();
        }

        public ProductWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            btnUpdateProduct.Visibility = Visibility.Hidden;
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }

        public ProductWindow(BO.Product product, IBl bl)
        {
            blp = bl;
            InitializeComponent();
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            FillingControlsForProductUpdate(product);
            btnAddProduct.Visibility = Visibility.Hidden;

        }

        private void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? idBeforeTryParse = this.txtID.Text;
            int.TryParse(idBeforeTryParse, out int id);
            product.ID = id;
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? name = this.txtName.Text;
            product.Name = name;
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? priceBeforeTryParse = this.txtPrice.Text;
            int.TryParse(priceBeforeTryParse, out int price);
            product.Price = price;
        }

        private void txtInStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? inStockBeforeTryParse = this.txtInStock.Text;
            int.TryParse(inStockBeforeTryParse, out int inStock);
            product.InStock = inStock;
        }

        private void cboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)cboxCategory.SelectedItem;
            product.Category = selectedCategory;
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
    }
}
