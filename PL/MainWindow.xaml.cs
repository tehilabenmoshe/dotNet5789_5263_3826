using BlApi;
using PL.Manager;
//using PL.Costumer;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        enum ManagerOptions { Orders, Products }
        BlApi.IBL? bl = BlApi.Factory.Get()??throw new NullReferenceException("Missing id");
        public MainWindow()
        {
            InitializeComponent();
            ManagerButton.ItemsSource = Enum.GetValues(typeof(ManagerOptions));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow pld =new ProductListWindow();
            pld.Show();

        }

        private void CostumerButton_Click(object sender, RoutedEventArgs e)
        {
            CostumerMainPage c = new CostumerMainPage();
            this.Content=c;
           // this.Close();


         
           // new CostumerMainPage() c.ShowDialog();
        }

        private void ManagerButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManagerButton.SelectedItem is ManagerOptions.Products)
            {
                ProductListWindow pld = new ProductListWindow();
                pld.Show();
            }
            if (ManagerButton.SelectedItem is ManagerOptions.Orders)
            {
                ManagerOrdersPage m = new ManagerOrdersPage();
                this.Content = m;
            }
        }
    }
}
