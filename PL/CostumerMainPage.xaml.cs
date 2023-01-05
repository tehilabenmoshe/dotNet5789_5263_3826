using PL.Costumer;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for CostumerMainPage.xaml
    /// </summary>
    public partial class CostumerMainPage : Page
    {
        public CostumerMainPage()
        {
            InitializeComponent();
        }

        //private void TrackOrderBottun_Click(object sender, RoutedEventArgs e)
        //{
           
        //   // this.Close();
        //}

        private void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
           InitializeComponent();
            ProductItemPage p = new ProductItemPage();
            // p.ShowDialog();
            //this.NavigationService.Navigate(p);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBotton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationWindow window = new NavigationWindow();
            //window.Source = new Uri("ProductItemPage.xaml", UriKind.Relative);
            //window.Show();



            MainWindow m=new MainWindow();
            ////this.Content = m;
          //  this.Hide();
            m.ShowDialog();
            //NavigationService.Navigate(m);
            //this.Visibility = Visibility.Hidden;
         //  this.Close();

            // var m=new MainWindow(this);
            //NavigationService.Navigate(mTemp);
        }

        

    }
}
