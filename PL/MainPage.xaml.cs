using PL.Costumer;
using PL.Manager;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        Frame frame;

       // MainWindow temp = new MainWindow();
        public MainPage(Frame temp)
        {
            InitializeComponent();
            frame=temp;
        }

        //public MainPage()
        //{
        //    InitializeComponent();

        // // temp = m;
        //}

        private void CostumerButton_Click(object sender, RoutedEventArgs e)
        {
            CostumerMainPage c = new CostumerMainPage(frame);
            frame.Content = c;
        }

        private void ManagerButton2_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow p = new PasswordWindow(frame);
            p.Show();



            //MainManagerPage2 m = new MainManagerPage2(frame);
            //frame.Content = m;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    PasswordWindow p = new PasswordWindow();  
        //    p.ShowDialog();
        //}
    }
}
