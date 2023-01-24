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
    /// Interaction logic for OrderTrackSimulator.xaml
    /// </summary>
    public partial class OrderTrackSimulator : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        BO.OrderTracking orderTrack=new BO.OrderTracking();
        public OrderTrackSimulator(BO.OrderForList? o=null)
        {
            InitializeComponent();

            if (o != null) {
                orderTrack = bl.Order.TrackOrder(o.ID);
                DataContext = o;

                //o.Tracking



                string s = "";
                foreach (var or in orderTrack.Tracking!)
                    s += (or.ToString()) + "\n";
                DeliveryDateBox.Text = s;
            } 
        }
    }
}
