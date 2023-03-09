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
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        BlApi.IBL? bl = BlApi.Factory.Get() ?? throw new NullReferenceException("missing bl");
        //  BO.OrderTracking orderTrack=new BO.OrderTracking();
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




            //InitializeComponent();
            //AddOrderItemList(bl.Order.getOrderForList());
            //DataContext = orderForList;

            //worker = new BackgroundWorker();
            //worker.DoWork += Worker_DoWork;
            //worker.ProgressChanged += Worker_ProgressChanged!;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            //worker.WorkerReportsProgress = true;
            //worker.WorkerSupportsCancellation = true;//suppot cancellation
        }


//        private void OrderlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
////        {
////            OrderTrackSimulator o=new OrderTrackSimulator((BO.OrderForList)OrderlistView.SelectedItem);
////            o.ShowDialog(); 
////        }


        //private void doubleClickShowOrder(object sender, MouseButtonEventArgs e)
        //{
        //    //DataContext = orderListPO;
        //    // var order = (OrderForListPO)orderListV.SelectedItem;
        //    new Simulator((PO.OrderForListPO)orderListV.SelectedItem).ShowDialog();
        //    // new orderWindow((PO.OrderForListPO)orderListV.SelectedItem
        //    // DataContext = orderListPO;
        //    //  IEnumerableToObservable(bl.Order.getOrderForList());
        //}
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderToCopy"></param> //המקורי
        //        private void AddOrderItemList(IEnumerable<BO.OrderForList> orderToCopy) //copy the items list from order to the items list above
        ////        {
        //            var list = (from o in orderToCopy
        //                        select new BO.OrderForList
        //                        {
        //                            ID = o.ID,
        //                            CustomerName = o.CustomerName,
        //                            Status = o.Status,
        //                            AmountOfItems = o.AmountOfItems,
        //                            TotalPrice = o.TotalPrice,
        //                        }).ToList();

        //        orderForList.Clear();
        //                    foreach (var temp in list)
        //                        orderForList.Add(temp);


        //        //            //orderForList.Clear();
        //        //            //foreach (var temp in orderToCopy)
        //        //            // orderForList.Add(temp);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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


            //MessageBox.Show("Delayed simulator");


            //            //if (e.Cancelled == true)
            //                MessageBox.Show("Delayed Simulator");
            //            //else if (flag == false)
            //            //{
            //            //    Stop.IsEnabled = false;
            //            //    Start.IsEnabled = true;
            //            //    MessageBox.Show("End Simulator");
            //            //}



        }




        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {


            // if (orderForList == null)
            //            {
            //                AddOrderItemList(bl!.Order.getOrderForList());
            //            }
            //               // bl!.ListOrder.

            //            bool IsDelivered = true;
            //            foreach (OrderForList order in orderForList)
            //            {
            //                if (order.Status == BO.OrderStatus.ordered)
            //                {
            //                    IsDelivered=false;
            //                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).OrderDate!;
            //                    orderDateTime = orderDateTime.AddHours(1);
            //                    if (orderDateTime <= time)
            //                    {
            //                        order.Status = BO.OrderStatus.shipped;
            //                        bl.Order.UpdateShipOrder((int)order.ID);

            //                    }
            //                }
            //                else if (order.Status == BO.OrderStatus.shipped)
            //                {
            //                    IsDelivered = false;
            //                    // BO.Order o = bl.Order.GetOrder((int)order.ID);
            //                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).OrderDate!;
            //                    orderDateTime =orderDateTime.AddMinutes(50);
            //                    if (orderDateTime <= time)
            //                    {
            //                        order.Status = BO.OrderStatus.delivered;
            //                        bl.Order.UpdateProvisionOrder((int)order.ID);


            //                    }
            //                }

            //                if (IsDelivered)
            //                    flag = false;
            //            }

            ///////////

            AddOrderItemList(bl!.Order.getOrderForList());
            flag = false;
            foreach (/*BO.OrderForList*/   var order in orderForList)
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
                    // BO.Order o = bl.Order.GetOrder((int)order.ID);
                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).ShipDate!;
                    orderDateTime = orderDateTime.AddDays(4);
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

        //private void OrderlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //     new OrderTrackSimulator((BO.OrderForList)OrderlistView.SelectedItem).ShowDialog(); 

        //}

        private void doubleClickShowOrder(object sender, MouseButtonEventArgs e)
        {
            new OrderTrackSimulator((BO.OrderForList)OrderlistView.SelectedItem).ShowDialog();

        }


      
    }
}










































//        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
//        {

//            while (flag)
//            {


//                if(worker?.CancellationPending == true)
//                {
//                    e.Cancel = true;
//                    break;
//                }
//                else
//                {

//                    if (worker!.WorkerReportsProgress! == true)
//                    {
//                        time=time.AddSeconds(5);
//                        Thread.Sleep(500);
//                        worker.ReportProgress(4);
//                    }

//                }
//            }
//        }

//        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) //event while there is a change
//        {
//            if (orderForList == null)
//            {
//                AddOrderItemList(bl!.Order.getOrderForList());
//            }
//               // bl!.ListOrder.

//            bool IsDelivered = true;
//            foreach (OrderForList order in orderForList)
//            {
//                if (order.Status == BO.OrderStatus.ordered)
//                {
//                    IsDelivered=false;
//                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).OrderDate!;
//                    orderDateTime = orderDateTime.AddHours(1);
//                    if (orderDateTime <= time)
//                    {
//                        order.Status = BO.OrderStatus.shipped;
//                        bl.Order.UpdateShipOrder((int)order.ID);

//                    }
//                }
//                else if (order.Status == BO.OrderStatus.shipped)
//                {
//                    IsDelivered = false;
//                    // BO.Order o = bl.Order.GetOrder((int)order.ID);
//                    DateTime orderDateTime = (DateTime)bl!.Order.GetOrder((int)order!.ID!).OrderDate!;
//                    orderDateTime =orderDateTime.AddMinutes(50);
//                    if (orderDateTime <= time)
//                    {
//                        order.Status = BO.OrderStatus.delivered;
//                        bl.Order.UpdateProvisionOrder((int)order.ID);


//                    }
//                }

//                if (IsDelivered)
//                    flag = false;
//            }

//        ///


//        }

//        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //event while do-work end
//        {
//            //MessageBox.Show("Delayed simulator");


//            //if (e.Cancelled == true)
//                MessageBox.Show("Delayed Simulator");
//            //else if (flag == false)
//            //{
//            //    Stop.IsEnabled = false;
//            //    Start.IsEnabled = true;
//            //    MessageBox.Show("End Simulator");
//            //}
//        }

//        private void AddOrderItemList(IEnumerable<BO.OrderForList> orderToCopy) //copy the items list from order to the items list above
//        {
//            var list = (from o in orderToCopy
//                        select new BO.OrderForList
//                        {
//                            ID = o.ID,
//                            CustomerName = o.CustomerName,
//                            Status = o.Status,
//                            AmountOfItems = o.AmountOfItems,
//                            TotalPrice = o.TotalPrice,
//                        }).ToList();

//            orderForList.Clear();
//            foreach (var temp in list)
//                orderForList.Add(temp);


//            //orderForList.Clear();
//            //foreach (var temp in orderToCopy)
//            // orderForList.Add(temp);

//        }

//        private void Start_Click(object sender, RoutedEventArgs e)
//        {
//            worker!.RunWorkerAsync();//start running the simulator
//            Start.IsEnabled = false;
//            Stop.IsEnabled = true;
//        }

//        private void Stop_Click(object sender, RoutedEventArgs e)
//        {
//            worker?.CancelAsync();
//            Start.IsEnabled = true;
//            Stop.IsEnabled = false;
//        }

//        private void OrderlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            OrderTrackSimulator o=new OrderTrackSimulator((BO.OrderForList)OrderlistView.SelectedItem);
//            o.ShowDialog(); 
//        }
//    }
//}
