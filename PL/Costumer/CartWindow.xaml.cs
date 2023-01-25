using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        static Cart? myCart = new Cart() { CustomerAddress = "", CustomerEmail = "", CustomerName = "", Items = new List<BO.OrderItem?>(), TotalPrice = 0 };
        // ObservableCollection<OrderItem> orderItemList = new();

        bool IsEmpty = true;
        public CartWindow()
        {
            InitializeComponent();
            // bl!.cart.MakeCart(myCart);
            CartListView.DataContext= myCart.Items;
        }

    
        public CartWindow(BO.ProductItem p)
        {
            InitializeComponent();
           // bl!.cart.MakeCart(myCart);
            bl!.cart.AddProductToCart(myCart, p.ID);
            CartListView.DataContext = myCart.Items;
            IsEmpty = false;
        }

        public CartWindow(BO.ProductItem p, int num) //for remove
        {
            InitializeComponent();
            BO.OrderItem o = new OrderItem();
            // o= bl!.OrderItem.GetById(p.ID);
            o=myCart.Items!.Find(crt => crt.ProductID == p.ID);
            if (o == null)
                MessageBox.Show("The product doesnt exsist in cart");
            else
            {
                int NewAmount = (int)(o.Amount - 1);
                bl!.cart.UpdateProductInCart(myCart, p.ID, NewAmount);
                CartListView.DataContext = myCart.Items;
            }

            if (myCart.TotalPrice == 0)
                IsEmpty = true;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsEmpty)
                MessageBox.Show("Cart Is Empty");
            else {
                BO.Cart temp = myCart;
                myCart = new Cart() { CustomerAddress = "", CustomerEmail = "", CustomerName = "", Items = new List<BO.OrderItem?>(), TotalPrice = 0 };
                CustomerDetailsCart c = new CustomerDetailsCart(temp);
                c.Show();
            }




        }
    }
}
