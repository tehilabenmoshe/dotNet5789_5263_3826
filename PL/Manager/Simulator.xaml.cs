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
using System.ComponentModel;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using BO;
using DO;
using BlApi;


namespace PL.Manager
{
    public partial class Simulator : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        ObservableCollection<OrderForList> orderForList = new();
        ObservableCollection<BO.Order> order = new();
        DateTime time = DateTime.Now;
        BackgroundWorker? worker;
        bool flag = true; //true=not end

        public Simulator()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged!;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

           AddOrderItemList(bl.Order.getOrderForList());
           DataContext = orderForList;
        }

        private void AddOrderItemList(IEnumerable<BO.OrderForList> orderToCopy)
        {
            var list = (from o in orderToCopy
                        select new BO.OrderForList
                        {
                            ID = o.ID,
                            CustomerName = o.CustomerName,
                            Status = o.Status,
                            AmountOfItems = o.AmountOfItems,
                            TotalPrice = o.TotalPrice,
                        }).ToList();
            orderForList.Clear();
            foreach (var temp in list)
                orderForList.Add(temp); 
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
                MessageBox.Show("Delayed Simulator");
            else if (flag == false)
            {
                Stop.IsEnabled = false;
                Start.IsEnabled = true;
                MessageBox.Show("End Simulator");
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            AddOrderItemList(bl!.Order.getOrderForList());
            flag = false;
            foreach (/*BO.OrderForList*/  var order in orderForList)
            {

                if ((order.Status == BO.OrderStatus.ordered))
                {
                    flag = true;
                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).OrderDate!;
                    orderDateTime = orderDateTime.AddDays(3);
                    if (orderDateTime <= time)
                    {
                        order.Status = BO.OrderStatus.shipped;
                        bl.Order.UpdateShipOrder((int)order.ID);
                        break;
                    }
                }
                else if (order.Status == BO.OrderStatus.shipped)
                {
                    flag = true;
                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).ShipDate!;
                    orderDateTime = orderDateTime.AddDays(4).AddHours(8);
                    if (orderDateTime <= time)
                    {
                        order.Status = BO.OrderStatus.delivered;
                        bl.Order.UpdateProvisionOrder((int)order.ID);
                        break;
                    }
                }

            }
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (flag)
            {
                if (worker?.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (worker!.WorkerReportsProgress! == true)
                    {
                        time = time.AddHours(20);
                        Thread.Sleep(2000);
                        worker.ReportProgress(4);
                    }
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            worker!.RunWorkerAsync();//start running the simulator
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            worker!.CancelAsync();
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
        }

        private void doubleClickShowOrder(object sender, MouseButtonEventArgs e)
        {
            new OrderTrackSimulator((BO.OrderForList)OrderlistView.SelectedItem).ShowDialog();
        }
      
    }
}

