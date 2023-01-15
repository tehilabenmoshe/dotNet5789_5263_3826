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
        // BO.Order o=new BO.Order();
        ObservableCollection<BO.OrderItem> items = new();
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

           BO.Order temp = new BO.Order();
           temp = bl.Order.GetOrder(orderList.ID);
            DataContext=temp;
            //ItemsBox.Items.Clear();
            //foreach (var item in listMaterial)
            //{
            //comboBox1.Items.Add(new ComboBoxItem(Convert.ToString(item.code), Convert.ToString(item.value)));
            //
           




        }

        public OrderWindow(BO.OrderTracking orderTracking)
        {
            InitializeComponent();
            
            BO.Order temp = new BO.Order();
            temp = bl.Order.GetOrder(orderTracking.ID);
            DataContext = temp;

            ShipDateBox.IsReadOnly = true;
            DeliveryDateBox.IsReadOnly = true;



        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void ItemsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
