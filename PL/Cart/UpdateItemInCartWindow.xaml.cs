using BlApi;
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
    /// Interaction logic for UpdateItemInCartWindow.xaml
    /// </summary>
    public partial class UpdateItemInCartWindow : Window
    {
        private IBl blp;
        int ProductID;
        public string newAmount { get; set; } = "0";
        public UpdateItemInCartWindow(IBl bl, int productID)
        {
            InitializeComponent();
            blp = bl;
            ProductID = productID;
        }

        private void btnUpdateAmount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.UpdateAmountOfProduct(MainWindow.cart, ProductID, Convert.ToInt32(newAmount));
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
