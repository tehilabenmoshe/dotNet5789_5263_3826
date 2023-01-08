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
    /// Interaction logic for MainManagerPage.xaml
    /// </summary>
    public partial class MainManagerPage : Page
    {
        public MainManagerPage()
        {
            InitializeComponent();
          //  MainManagerFrame.Content = new MainManagerPage();

        }

     
        //private void ProductButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void ProductButton_Click_1(object sender, RoutedEventArgs e)
        {
            //ProductListWindow pld = new ProductListWindow();
           // pld.Show();

           // MainManagerFrame.Navigate(new tempPagexaml());

           MainManagerFrame.Content = new tempPagexaml();

            tempPagexaml t=new tempPagexaml();
            this.Content = t;

        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerOrderWindow m = new ManagerOrderWindow();
            m.Show();
        }

        private void MainManagerFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
