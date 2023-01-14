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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public class UserDetails
        {
            public string? customerName { get; set; } = "";
            public string? customerEmail { get; set; } = "";
            public string? customerAdress { get; set; } = "";
        }
        UserDetails userDetails = new();
        private IBl blp;

        public UserWindow(IBl bl)
        {
            blp = bl;
            InitializeComponent();
            DataContext = userDetails;
        }

        private void btnConfirmationFillingTheDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blp.Cart.MakeAnOrder(MainWindow.cart,userDetails.customerName ?? throw new BO.ExceptionNull(),userDetails.customerEmail ?? throw new BO.ExceptionNull(),userDetails.customerAdress ?? throw new BO.ExceptionNull());
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
