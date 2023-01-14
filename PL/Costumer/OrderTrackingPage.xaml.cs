using BO;
using PL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Costumer
{
    /// <summary>
    /// Interaction logic for OrderTrackingPage.xaml
    /// </summary>
    public partial class OrderTrackingPage : Page
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        BO.OrderTracking orderTracking=new OrderTracking();
       // BO.Order order = new Order();
        CustomerMainFrame tempFrame;

        //public int MyValue { get; set; }

        public OrderTrackingPage(string s, CustomerMainFrame c)
        {
            InitializeComponent();
            int idNum= Int32.Parse(s);
            orderTracking = bl.Order.TrackOrder(idNum);

            StatusBox.Text= orderTracking.Status.ToString();
            IdBox.Text = s;
            tempFrame=c;

        }

        private void OrderDetails_Click(object sender, RoutedEventArgs e)
        {
            //tempFrame.CustomerFrame.Content = new OrderWindow(orderTracking);

            OrderWindow o = new OrderWindow(orderTracking);
            o.InitializeComponent();
            o.Show();   
        }

        private void StatusBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
