//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for PasswordWindow.xaml
//    /// </summary>
//    public partial class PasswordWindow : Window
//    {
//        public PasswordWindow()
//        {
//            InitializeComponent();
//        }
//    }
//}

using BlApi;
using BlImplementation;
using DalApi;
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
    /// Interaction logic for InsertManegerPage.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        IBl bl;
        //private MenuWindow menuWindow;
        public event EventHandler InsertButtonClick;
        public event EventHandler ExitProgramClick;
        public event EventHandler BackToMainPageClick;
    //    public BO.User insertUser;
        public PasswordWindow()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

        }

        private void tryOpenManegerWindow()
        {
            if (!bl.SearchUser(UserNameBox.Text, PasswordBox.Password) || UserNameBox.Text == null || PasswordBox.Password == null)
            {
                NotValid.Text = "שם משתשמש או ססמא שגויים";
                NotValid.Visibility = Visibility.Visible;
            }
            else
            {
                insertUser = bl.GetUserDetails(UserNameBox.Text);
                Connect.IsEnabled = true;
                NotValid.Visibility = Visibility.Hidden;
                if (insertUser.UserAccessManagement)
                {
                    if (InsertButtonClick != null)
                    {
                        InsertButtonClick(this, EventArgs.Empty);
                    }
                }
                else
                {
                    NotValid.Text = "נדרשת הרשאת ניהול על מנת להתחבר למערכת זו";
                    NotValid.Visibility = Visibility.Visible;
                }
            }
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            tryOpenManegerWindow();
        }

        private void checkIfEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                tryOpenManegerWindow();
        }

        //private void exitProgram_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ExitProgramClick != null)
        //    {
        //        ExitProgramClick(this, EventArgs.Empty);
        //    }
        //}

        private void BackToMainPage_Click(object sender, RoutedEventArgs e)
        {
            if (BackToMainPageClick != null)
            {
                BackToMainPageClick(this, EventArgs.Empty);
            }
        }
    }
}
