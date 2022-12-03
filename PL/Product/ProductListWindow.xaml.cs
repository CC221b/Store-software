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
        private IBl bl = new Bl();
        public ProductListWindow(IBl bl)
        {
            InitializeComponent();
            ProductsListview.ItemsSource = bl.Product.GetListProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)CategorySelector.SelectedItem;
            ProductsListview.ItemsSource = bl.Product.FilterByCategory(selectedCategory);
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (BO.ProductForList)((sender as ListView).SelectedItem);
            BO.Product product = new BO.Product();
            product.ID= item.ID;
            product.Name = item.Name;
            product.Price= item.Price;
            product.Category= item.Category;
            try
            {
                product.InStock = bl.Product.GetProduct(item.ID).InStock;
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
            new Product.ProductWindow(product).Show();
            this.Close();
        }
    }
}
