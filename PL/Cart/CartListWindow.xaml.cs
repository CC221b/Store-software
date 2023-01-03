﻿using BlApi;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartListWindow.xaml
    /// </summary>
    public partial class CartListWindow : Window
    {
        private IBl blp;
        int amount = 0;
        string? customerName = "", customerEmail = "", customerAdress = "";
        BO.OrderItem orderItem = new();
        public void ManagingTheControlViewOfMakeAnOrder(string visibility)
        {
            if (visibility == "Hidden")
            {
                lblCustomerAdress.Visibility = Visibility.Hidden;
                lblCustomerName.Visibility = Visibility.Hidden;
                lblCustomerEmail.Visibility = Visibility.Hidden;
                txtCustomerAdress.Visibility = Visibility.Hidden;
                txtCustomerEmail.Visibility = Visibility.Hidden;
                txtCustomerName.Visibility = Visibility.Hidden;
                btnConfirmationFillingTheDetails.Visibility = Visibility.Hidden;
            }
            else
            {
                lblCustomerAdress.Visibility = Visibility.Visible;
                lblCustomerName.Visibility = Visibility.Visible;
                lblCustomerEmail.Visibility = Visibility.Visible;
                txtCustomerAdress.Visibility = Visibility.Visible;
                txtCustomerEmail.Visibility = Visibility.Visible;
                txtCustomerName.Visibility = Visibility.Visible;
                btnConfirmationFillingTheDetails.Visibility = Visibility.Visible;
            }
        }

        public void ManagingControlsForUpdatingAnItemInAnOrder(string visibility)
        {
            if (visibility == "Hidden")
            {
                btnChangeAmount.Visibility = Visibility.Hidden;
                btnRemoveItem.Visibility = Visibility.Hidden;
            }
            else
            {
                btnChangeAmount.Visibility = Visibility.Visible;
                btnRemoveItem.Visibility = Visibility.Visible;
            }
        }

        public void ManagingControlsForUpdatingTheAmountOfAnItemInAnOrder(string visibility)
        {
            if (visibility == "Hidden")
            {
                txtChangeAmount.Visibility = Visibility.Hidden;
                btnOkChangeAmount.Visibility = Visibility.Hidden;
            }
            else
            {
                txtChangeAmount.Visibility = Visibility.Visible;
                btnOkChangeAmount.Visibility = Visibility.Visible;
            }
        }

        public CartListWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            ManagingTheControlViewOfMakeAnOrder("Hidden");
            ManagingControlsForUpdatingAnItemInAnOrder("Hidden");
            ManagingControlsForUpdatingTheAmountOfAnItemInAnOrder("Hidden");
            cartListView.ItemsSource = MainWindow.cart.Items;
            txtTotalPrice.Text = MainWindow.cart.TotalPrice.ToString();
        }

        private void btnChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            ManagingControlsForUpdatingTheAmountOfAnItemInAnOrder("Visible");
        }

        private void txtChangeAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? amountBeforeTryParse = this.txtChangeAmount.Text;
            int.TryParse(amountBeforeTryParse, out amount);
        }

        private void cartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            orderItem = (BO.OrderItem)((sender as ListView).SelectedItem);
            ManagingControlsForUpdatingAnItemInAnOrder("Visible");
        }

        private void btnOkChangeAmount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.UpdateAmountOfProduct(MainWindow.cart, orderItem.ProductID, amount);
                txtChangeAmount.Text = "";
                ManagingControlsForUpdatingTheAmountOfAnItemInAnOrder("Hidden");
                cartListView.ItemsSource = MainWindow.cart.Items;
                txtTotalPrice.Text = MainWindow.cart.TotalPrice.ToString();
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

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.UpdateAmountOfProduct(MainWindow.cart, orderItem.ProductID, 0);
                MessageBox.Show("The item remove from the cart successfully!!");
                cartListView.ItemsSource = MainWindow.cart.Items;
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

        private void txtCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerName = txtCustomerName.Text;
        }

        private void txtCustomerEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerEmail = txtCustomerEmail.Text;
        }

        private void txtCustomerAdress_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerAdress = txtCustomerAdress.Text;
        }

        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            ManagingTheControlViewOfMakeAnOrder("Visible");
            ManagingControlsForUpdatingAnItemInAnOrder("Hidden");
            ManagingControlsForUpdatingTheAmountOfAnItemInAnOrder("Hidden");
            cartListView.Visibility = Visibility.Hidden;
            lblTotalPrice.Visibility = Visibility.Hidden;
            txtTotalPrice.Visibility = Visibility.Hidden;
            btnMakeOrder.Visibility = Visibility.Hidden;
        }

        private void btnConfirmationFillingTheDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.MakeAnOrder(MainWindow.cart, customerName, customerEmail, customerAdress);
                MainWindow.cart = new();
                MainWindow.cart.Items = new();
                cartListView.ItemsSource = null;
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
