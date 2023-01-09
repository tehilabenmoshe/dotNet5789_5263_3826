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
        MainManagerPage temp;
        public MainManagerPage2(MainManagerPage m)
        {
            InitializeComponent();
            temp = m;
            

        }

        private void ProductButton_Click_1(object sender, RoutedEventArgs e)
        {
            
            temp.MainManagerFrame.Content= new ProductListPage();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
           // ManagerOrderWindow m = new ManagerOrderWindow();
           // m.Show();


            temp.MainManagerFrame.Content = new ManagerOrderListPage();
        }
    }
}
