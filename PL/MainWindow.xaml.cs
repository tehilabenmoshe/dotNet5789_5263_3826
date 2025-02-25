﻿using BlApi;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

    //  enum ManagerOptions { Orders, Products }
        BlApi.IBL? bl = BlApi.Factory.Get()??throw new NullReferenceException("Missing id");
        public MainWindow()
        {
          InitializeComponent();
          mainFrame.Content = new MainPage(mainFrame);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new MainPage(mainFrame);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new MainPage(mainFrame);
        }
    }
}
