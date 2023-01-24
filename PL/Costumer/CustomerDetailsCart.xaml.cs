using BlImplementation;
using BO;
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

namespace PL.Costumer
{
    /// <summary>
    /// Interaction logic for CustomerDetailsCart.xaml
    /// </summary>
    public partial class CustomerDetailsCart : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        BO.Cart tempCart;
        public CustomerDetailsCart(BO.Cart myCart)
        {
            InitializeComponent();
            tempCart = myCart;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tempCart.CustomerName = NameBox.Text;
            tempCart.CustomerEmail = EmailBox.Text;
            tempCart.CustomerAddress=AddressBox.Text;
            try{
                bl!.cart.MakeCart(tempCart); //insert the cart to the order
                MessageBox.Show("The order was successfully placed!");

            }
            catch (Exception ex) {
                MessageBox.Show("ERROR");

            }


        }
    }
}
