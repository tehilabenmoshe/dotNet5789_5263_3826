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


namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
      //  BO.OrderTracking orderTrack=new BO.OrderTracking();
        ObservableCollection<OrderForList> orderForList = new();
        DateTime time = DateTime.Now;
        BackgroundWorker? worker;
        bool flag = true; //true=not end
        
        public Simulator()
        {
            InitializeComponent();
            AddOrderItemList(bl.Order.getOrderForList());
            DataContext = orderForList;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged!;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;//suppot cancellation
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {

            while (flag)
            {

            
                if(worker?.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (worker!.WorkerReportsProgress! == true)
                    {
                        time =time.AddSeconds(5);
                        Thread.Sleep(500);
                        worker.ReportProgress(4);
                    }

                }
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) //event while there is a change
        {
            AddOrderItemList(bl!.Order.getOrderForList());
            bool IsDelivered = true;
            foreach (OrderForList order in orderForList)
            {
                if (order.Status == BO.OrderStatus.ordered)
                {
                    IsDelivered=false;
                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).OrderDate!;
                    orderDateTime = orderDateTime.AddHours(1);
                    if (orderDateTime <= time)
                    {
                        order.Status = BO.OrderStatus.shipped;
                        bl.Order.UpdateShipOrder((int)order.ID);
                        //break;
                    }
                }
                else if (order.Status == BO.OrderStatus.shipped)
                {
                    IsDelivered = false;
                    // BO.Order o = bl.Order.GetOrder((int)order.ID);
                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).ShipDate!;
                    orderDateTime = orderDateTime.AddMinutes(1);
                    if (orderDateTime <= time)
                    {
                        order.Status = BO.OrderStatus.delivered;
                        bl.Order.UpdateProvisionOrder((int)order.ID);
                        //break;
                    }
                }

                if (IsDelivered)
                    flag = false;
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //event while do-work end
        {
            MessageBox.Show("Delayed simulator");
        }

        private void AddOrderItemList(IEnumerable<BO.OrderForList> orderToCopy) //copy the items list from order to the items list above
        {
            var list = (from o in orderToCopy
                                select new OrderForList
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

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            worker!.RunWorkerAsync();//start running the simulator
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            worker?.CancelAsync();
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
        }

        private void OrderlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderTrackSimulator o=new OrderTrackSimulator((BO.OrderForList)OrderlistView.SelectedItem);
            o.ShowDialog(); 
        }
    }
}
