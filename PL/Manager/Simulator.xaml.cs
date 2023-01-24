using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        BO.OrderTracking orderTrack=new BO.OrderTracking();
        ObservableCollection<BO.Order> order = new();
        public Simulator()
        {
            InitializeComponent();
            orderTrack = bl.Order.TrackOrder((int)order.);
            
            OrderlistView.DataContext = orders;
        }
    }
}
