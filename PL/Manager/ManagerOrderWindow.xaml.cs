using BO;
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
    /// Interaction logic for ManagerOrderWindow.xaml
    /// </summary>
    public partial class ManagerOrderWindow : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        ObservableCollection<OrderForList> orderList = new();
        public ManagerOrderWindow()
        {
            InitializeComponent();
            IEnumerableToPL(bl.Order.getOrderForList());
            OrdersCatalog.DataContext = orderList;
            //CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        //private void OrdersCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e){}

        

      


        private void IEnumerableToPL(IEnumerable<OrderForList> list)
        {
            orderList.Clear();
            foreach (var temp in list)
                orderList.Add(temp);
        }

        private void OrdersCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerableToPL(bl!.Order.getOrderForList());
        }



        private void OrdersCatalog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            OrderWindow o = new OrderWindow((BO.OrderForList)OrdersCatalog.SelectedItem);
            InitializeComponent();
            o.Show();
        }

    }
}
