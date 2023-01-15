using PL.Costumer;
using PL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
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
       // public System.Windows.TriggerCollection Triggers { get; }
        Frame temp;
        static string s;
        public CostumerMainPage(Frame c)
        {
            InitializeComponent();
            temp = c;
        }

        private void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // InitializeComponent();
            //this.Content =new ProductItemCatalogPage();
            // p.ShowDialog();
            //this.NavigationService.Navigate(p);
            temp.Content = new ProductItemCatalogPage(temp);

        }

       

        private void BackBotton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m=new MainWindow();
            m.ShowDialog();
        }

        public int MyValue { get; set; }
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(MyValue.ToString());
            temp.Content = new OrderTrackingPage(OrderIdBox.Text, temp);  //send the string of the id to the page of tracking order
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void OrderIdBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // string s;
           // s = OrderIdBox.Text;
          // s= Int32.Parse(OrderIdBox.Text);

        }
    }
}
