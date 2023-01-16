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
        }

        public CartWindow(BO.ProductItem p, int num) //for remove
        {
            InitializeComponent();
            BO.OrderItem o = new OrderItem();
            // o= bl!.OrderItem.GetById(p.ID);
            o=myCart.Items!.Find(crt => crt.ProductID == p.ID);
            int NewAmount =(int)( o.Amount - 1);
            bl!.cart.UpdateProductInCart(myCart,p.ID, NewAmount);
            CartListView.DataContext = myCart.Items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsCart c = new CustomerDetailsCart(myCart);
            c.Show();

           


        }
    }
}
