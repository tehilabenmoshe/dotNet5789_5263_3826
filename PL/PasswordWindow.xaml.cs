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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        Frame frame;
        public PasswordWindow(Frame temp)
        {
            InitializeComponent();
            frame = temp;
        }

        private void pressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                check_password(sender, e);
        }

      
        public void check_password(object o, RoutedEventArgs e)
        {
            worngPassword.Visibility = Visibility.Hidden;
            if (PasswordBox.Password == "1111" || PasswordBox.Password == "0545")
            {

                frame.Content = new MainManagerPage2(frame);
                PasswordBox.Password = "";
                this.Close();
            }
            else
            {
                worngPassword.Visibility = Visibility.Visible;
                PasswordBox.Clear();
            }
        }

       



    }
}