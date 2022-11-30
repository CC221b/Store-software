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
        private IBl bl = new Bl();
        public BO.Product product = new BO.Product();
        public ProductWindow(IBl bl)
        {
            InitializeComponent();
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }

        public ProductWindow(BO.Product product)
        {
            InitializeComponent();
            cboxCategory.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            txtID.Text = product.ID.ToString();
            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            txtInStock.Text = product.InStock.ToString();
            cboxCategory.Text = product.Category.ToString();
            btnProduct.Content = "updateProduct";
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

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnProduct.Content == "AddProduct")
            {
                try
                {
                    bl.Product.AddProduct(product);
                    this.Close();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            else
            {
                try
                {
                    bl.Product.UpdateProduct(product);
                    this.Close();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            
        }

        private void btnDeleteFromCart_Click(object sender, RoutedEventArgs e)
        {
            //איך יהיה לי עגלה כאן???
            //try
            //{
            //    bl.Cart.UpdateAmountOfProduct(,Convert.ToInt32(txtID.Text), 0);
            //}
            //catch (Exception)
            //{
            //    throw ;
            //}
        }
    }
}
