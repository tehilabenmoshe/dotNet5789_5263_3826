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

namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for MainManagerPage2.xaml
    /// </summary>
    public partial class MainManagerPage2 : Page
    {
        Frame temp;
        public MainManagerPage2(Frame m)
        {
            InitializeComponent();
            temp = m;
            

        }

        private void ProductButton_Click_1(object sender, RoutedEventArgs e)
        {
            
            temp.Content= new ProductListPage();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
           // ManagerOrderWindow m = new ManagerOrderWindow();
           // m.Show();


            temp.Content = new ManagerOrderListPage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Simulator s=new Simulator();
            s.Show();

        }
    }
}
