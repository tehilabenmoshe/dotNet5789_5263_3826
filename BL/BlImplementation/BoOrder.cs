using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;
//using DalApi;

namespace BlImplementation;

internal class BoOrder : IBoOrder
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    public readonly Random rand = new Random();
    public IEnumerable<BO.OrderForList> getOrderForList()
    {
        IEnumerable<DO.Order?> order = Dal!.Order.GetAll().ToList();
        IEnumerable<BO.OrderForList?> listOfOrders = new List<BO.OrderForList>();

        return
        (from orderDO in order
         let orderFromBL = Dal!.OrderItem.GetItemsList((int)(orderDO?.ID!))
         select new BO.OrderForList()
         {
             ID = (int)(orderDO?.ID!),
             CustomerName = orderDO?.CustomerName,
             Status = Tools.GetStatus((DO.Order)orderDO),
             AmountOfItems = Tools.GetAmountOfItems(orderFromBL),
             TotalPrice = Tools.GetTotalPrice(orderFromBL)
         }).ToList().OrderBy(o=>o.ID);
    }
    public BO.OrderStatus getStatus(BO.Order o) 
    {
         BO.OrderStatus s = new BO.OrderStatus();
        
        if(o.ShipDate == o.DeliveryDate)//if the both dates are the same- the order dosent yet-only approved
        {
            s = BO.OrderStatus.ordered;
        }
        else
        {
            if (o.ShipDate > o.DeliveryDate) //if the order didnt provided yet
            {
                s = BO.OrderStatus.shipped;
            }
            else
                s = BO.OrderStatus.delivered;
        }
        return s;
    }
    public IEnumerable<DO.OrderItem> GetListByOrderID(int id)
    {
        List<DO.OrderItem> tmp = new List<DO.OrderItem>();
        List<DO.OrderItem?> Loi = Dal!.OrderItem.GetAll().ToList();
        foreach (DO.OrderItem o in Loi)
        {
            if (o.OrderID == id)
            {
                tmp.Add(o);
            }
        }
        return tmp;
    }


    public BO.Order GetOrder(int ID)
    {
        if ((ID < 1000) || (ID > 9999))
            throw new BO.InvalidInputExeption("ID is out of range");
        DO.Order o = Dal?.Order.GetById(ID) ?? throw new DO.DoesntExistExeption("order doesnt exists");
        BO.Order temp=new BO.Order();
        temp = DoOrderToBo(o);
        return temp;
    }
    public BO.Order? DoOrderToBo(DO.Order temp)
    {
        
        BO.Order order = new BO.Order();
        order.ID = temp.ID;
        order.CustomerName = temp.CustomerName;
        order.CustomerEmail = temp.CustomerEmail;
        order.CustomerAddress = temp.CustomerAddress;
        order.OrderDate = temp.OrderDate;
        order.ShipDate = temp.ShipDate;
        order.DeliveryDate = temp.DeliveryDate;
        order.PaymantDate = DateTime.MinValue;
        order.Status = getStatus(order);

        List<DO.OrderItem> doList = (List<DO.OrderItem>)GetListByOrderID(order.ID);
        List<BO.OrderItem> boList = new List<BO.OrderItem>();

        int cartTotalPrice = 0;
        foreach (DO.OrderItem d in doList) //copy each order item from do to bo
        {
            DO.Product product = new DO.Product();
            product = Dal!.Product.GetById(d.ProductID);
            BO.OrderItem item = new BO.OrderItem();
            item.ID = d.ID;
            item.ProductID = d.ProductID;
            item.Price = d.Price;
            item.Name = product.Name;
            item.Amount = d.Amount;
            item.TotalPrice = item.Price * item.Amount;
            boList.Add(item);
            cartTotalPrice = (int)(cartTotalPrice + item.TotalPrice);
        }
        order.Items = boList; //adds thr nes list of order items that copied from do to order
        order.TotalPrice = cartTotalPrice;
        return order;
    }
    public BO.Order UpdateShipOrder(int ID)
    {
        if ((ID < 1000) || (ID > 9999))//check the id
            throw new BO.DoesntExistException("ID is out of range");


        DO.Order orderDO = Dal!.Order.GetById(ID);// get order by id from DAL
        IEnumerable<DO.OrderItem?> itemsListDO = Dal.OrderItem.GetItemsList(orderDO.ID);  // orderItem list of order (DO)                                          // copy details
        var v = (from o in itemsListDO
                 let name = Dal.Product.GetById((int)(o?.ProductID!)).Name
                 select new BO.OrderItem
                 {
                     Name = name,
                     ID = (int)(o?.ID!),
                     ProductID = (int)(o?.ProductID!),
                     Price = (int)o?.Price,
                     Amount = (int)o?.Amount,
                     TotalPrice = o?.Price * o?.Amount
                 }).ToList();
        if (orderDO.ShipDate != null && orderDO.ShipDate < DateTime.Now)
        {
            //orderDO.ShipDate = DateTime.Now;
            orderDO.ShipDate = orderDO.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//ship befor deliverd     
            Dal.Order.Update(orderDO);
        }
        BO.Order orderToReturn = new BO.Order()
        {
            ID = orderDO.ID,
            CustomerName = orderDO.CustomerName,
            CustomerEmail = orderDO.CustomerEmail,
            CustomerAddress = orderDO.CustomerAddress,
            Status = BO.OrderStatus.shipped,
            OrderDate = orderDO.OrderDate,
            ShipDate = orderDO.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),//ship befor deliverd                                                                                                                                 // ShipDate = DateTime.Now,
            DeliveryDate = orderDO.DeliveryDate,
            Items = v,
            //(List<BO.OrderItem?>)Tools.getBOList(dal.OrderItem.GetItemsList(orderDO.ID)),
            TotalPrice = Tools.GetTotalPrice(itemsListDO!)
        };
        DO.Order orderToUpdate = (DO.Order)Tools.CopyPropToStruct(orderToReturn, typeof(DO.Order));
        Dal.Order.Update(orderToUpdate);
        return orderToReturn;


    }
    public BO.Order UpdateProvisionOrder(int ID)
    {

        if (ID < 0)
            throw new BO.InvalidInputExeption("המזהה אינו בתחום");
        
        
            DO.Order orderDO = Dal!.Order.GetById(ID);// get order by id from DO
            IEnumerable<DO.OrderItem?>? itemsListDO = Dal.OrderItem.GetItemsList(orderDO.ID);

            // copy details
            var v = (from o in itemsListDO
                     let name = Dal!.Product.GetById((int)(o?.ProductID!)).Name
                     select new BO.OrderItem
                     {
                         Name = name,
                         ID = (int)(o?.ID!),
                         ProductID = (int)(o?.ProductID!),

                         Price =(int)o?.Price,
                         Amount = (int)o?.Amount,
                         TotalPrice = o?.Price * o?.Amount
                     }).ToList();

            if (orderDO.DeliveryDate != null && orderDO.DeliveryDate < DateTime.Now)
            {

              //  orderDO.DeliveryDate = DateTime.Now;
                orderDO.DeliveryDate = orderDO.ShipDate + new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//ship befor deliverd   
                Dal.Order.Update(orderDO);
            }
            // create order to return
            BO.Order orderToReturn = new BO.Order()
            {
                ID = orderDO.ID,
                CustomerName = orderDO.CustomerName,
                CustomerEmail = orderDO.CustomerEmail,
                CustomerAddress = orderDO.CustomerAddress,
                Status = BO.OrderStatus.delivered,
                OrderDate = orderDO.OrderDate,
                ShipDate = orderDO.ShipDate,
                DeliveryDate = orderDO.ShipDate + new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                //DeliveryDate = DateTime.Now,
                Items = v, //(List<BO.OrderItem?>)Tools.getBOList(dal.OrderItem.GetItemsList(orderDO.ID)),
                TotalPrice = Tools.GetTotalPrice(itemsListDO!)

            };

        DO.Order orderToUpdate = (DO.Order)Tools.CopyPropToStruct(orderToReturn, typeof(DO.Order));// convert BO to DO
        Dal.Order.Update(orderToUpdate);// update in DAL
            return orderToReturn;

    }
    public BO.OrderTracking TrackOrder(int ID)
    {
        if ((ID < 1000) || (ID > 9999))//check the id
            throw new BO.InvalidInputExeption("Id is out of range");

        try
        {
            DO.Order order = Dal!.Order.GetById(ID); // order gy id from DO
            List<Tuple<DateTime?, string>>? TrackList = new List<Tuple<DateTime?, string>>();// create the list

            if (order.OrderDate != null)
            {
                TrackList.Add(Tuple.Create(order.OrderDate, "Ordered"));
                if (order.ShipDate != null)
                {
                    TrackList.Add(Tuple.Create(order.ShipDate, "Shipped"));
                    if (order.DeliveryDate != null)
                    {
                        TrackList.Add(Tuple.Create(order.DeliveryDate, "Delivered"));
                    }
                }
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking()
            {
                ID = order.ID,
                Status = BlApi.Tools.GetStatus(order),
                Tracking = TrackList
            };
            return orderTracking;
        }
        catch (DO.DoesntExistExeption ex)
        {
            throw new BO.DoesntExistException(ex.Message, ex);
        }


    }


}