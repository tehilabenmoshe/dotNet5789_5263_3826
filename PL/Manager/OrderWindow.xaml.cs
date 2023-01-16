using BO;
using DalApi;
using DO;
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

namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
       BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
       BO.Order order=new BO.Order();
       ObservableCollection<BO.OrderItem> items = new();

        public OrderWindow()
        {
            InitializeComponent();
             BO.Order temp = new BO.Order();
            AddOrderItemList(temp);
            OrderItems.DataContext = items;
            //UpdateButton.Visibility = Visibility.Hidden;

        }
        
        public OrderWindow(BO.OrderForList orderList)
        {
           InitializeComponent();
           // IEnumerableToPL(bl!.Product.getProductForList());
           BO.Order temp = new BO.Order();
           temp = bl.Order.GetOrder(orderList.ID);
           DataContext=temp;

           //OrderItems.DataContext = items;
            // ItemsBox.ItemsSource = temp.Items.);


            AddOrderItemList(temp);
            OrderItems.DataContext = items;
            if(orderList.AmountOfItems== 0)
            MessageBox.Show("there is no items in the order");
        }

        public OrderWindow(BO.OrderTracking orderTracking)
        {
            InitializeComponent();
            
            BO.Order temp = new BO.Order();
            temp = bl.Order.GetOrder(orderTracking.ID);
            DataContext = temp;

            ShipDateBox.IsReadOnly = true;
            DeliveryDateBox.IsReadOnly = true;


            //fill the list view:
            AddOrderItemList(temp);
            OrderItems.DataContext = items;

            //ItemsBox.ItemsSource = Enum.GetValues(typeof(BO.Category));

        }

        private void AddOrderItemList(BO.Order o) //copy the items list from order to the items list above
        {
            items.Clear();
            foreach (var temp in o.Items)
                items.Add(temp);
        }



        private void GetItemList(IEnumerable<BO.OrderItem> item)
        {
            
            //foreach (var temp in item)
                //items.Add(temp);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void ItemsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    BO.Order order=new BO.Order();  
        //  ObservableCollection<Items> ItemsList = new();
        //var orderitems = (from o in order.Items
        //                  select o).ToList();
        //DataContext = orderitems;
        //}



       


    }
}
