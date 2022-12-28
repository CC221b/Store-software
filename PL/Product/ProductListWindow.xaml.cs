﻿using System;
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
        int debily = 0;
        private IBl blp;
        string type = "";
        public ProductListWindow(IBl bl, string type1)
        {
            InitializeComponent();
            blp = bl;
            type = type1;
            if (type == "User")
            {
                btnAddProduct.Visibility = Visibility.Hidden;
            }
            ProductsListview.ItemsSource = blp.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            debily = ProductsListview.Items.Count;
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Categories selectedCategory = (BO.Categories)CategorySelector.SelectedItem;
            ProductsListview.ItemsSource = blp.Product.GetAll(item => (int)item.Category == (int)selectedCategory);
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (BO.ProductForList)((sender as ListView).SelectedItem);
            BO.Product product = new BO.Product();
            try
            {
                product = blp.Product.Get(item.ID);
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
            new Product.ProductWindow(product, blp).Show();
            this.Close();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            new Product.ProductWindow(blp).ShowDialog();
            ProductsListview.ItemsSource = blp.Product.GetAll();
            debily = ProductsListview.Items.Count;
        }
    }
}
