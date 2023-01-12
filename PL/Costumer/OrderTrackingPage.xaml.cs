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
        public int MyValue { get; set; }

        public OrderTrackingPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int id = TextBox.Hori ;

        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MyValue.ToString());
        }
    }
}
