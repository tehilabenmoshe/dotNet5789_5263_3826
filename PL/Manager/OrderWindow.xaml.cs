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

namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
       // BO.Order o=new BO.Order();

        public OrderWindow()
        {
            InitializeComponent();
           // CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //UpdateButton.Visibility = Visibility.Hidden;
        }
        
        public OrderWindow(BO.OrderForList orderList)
        {
            InitializeComponent();
            //o = bl.Order.GetOrder(orderList.ID);

           BO.Order ?temp = new BO.Order();
           temp = bl?.Order.GetOrder(orderList.ID);

            
            //NameBox.Text = temp!.Name.ToString();
            IdBox.Text = temp!.ID.ToString();
            //PriceBox.Text = temp!.Price.ToString();
            //InStockBox.Text = temp!.InStock.ToString();


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
