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
        /* List<OrderItem>*/

        //tmp = Loi.FindAll(o => o.OrderID == id);
        //return tmp;


        //List<DO.OrderItem> tmp = from var item in Loi
        //where item.OrderID == id
        //select item;

        //return tmp;
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
        //foreach (BO.OrderItem or in order.Items) //summing the total-price of each order item in order
        //{
        //    order.TotalPrice +=or.TotalPrice;
        //}
        return order;
    }
    public BO.Order UpdateShipOrder(int ID)
    {
        if ((ID < 1000) || (ID > 9999))//check the id
            throw new BO.DoesntExistException("ID is out of range");
        BO.Order order = new BO.Order();
        DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException("rder doesnt exists");
        order=DoOrderToBo(temp); //casting from bo to do  

        if(order.Status!=BO.OrderStatus.shipped) //of the order isnt sent yet
        {
            order.Status = BO.OrderStatus.shipped; //update the status to sent 
            order.ShipDate = DateTime.Now;//update the ship date in bo
            temp.ShipDate = order.ShipDate;//update the ship date in do
        }
        Dal?.Order.Update(temp);
        return order;
    }
    public BO.Order UpdateProvisionOrder(int ID)
    {
        if ((ID < 1000) || (ID > 9999))//check the id
            throw new BO.DoesntExistException("ID is out of range");
        BO.Order order = new BO.Order();
        DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException("order doesnt exists");
        order = DoOrderToBo(temp); //casting from bo to do
        
        if(order.Status != BO.OrderStatus.delivered)
        {
            order.Status = BO.OrderStatus.delivered; //update the status to provided
            order.DeliveryDate = DateTime.Now;//update the DeliveryDate date in bo
            temp.DeliveryDate = order.DeliveryDate;//update the DeliveryDate date in do
            //order.Status = BO.OrderStatus.delivered; //update the status
        }
        Dal?.Order.Update(temp);

        //BO.Order orderToReturn = new BO.Order()
        //{
        //    ID = temp.ID,
        //    CustomerName = temp.CustomerName,
        //    CustomerEmail = temp.CustomerEmail,
        //    CustomerAddress = temp.CustomerAddress,
        //    Status = BO.OrderStatus.delivered,
        //    OrderDate = temp.OrderDate,
        //    ShipDate = temp.ShipDate,
        //    DeliveryDate = DateTime.Now,
        //    Items = v, //(List<BO.OrderItem?>)Tools.getBOList(dal.OrderItem.GetItemsList(orderDO.ID)),
        //    TotalPrice = Tools.GetTotalPrice(itemsListDO!)

        //};

        //DO.Order orderToUpdate = (DO.Order)Tools.CopyPropToStruct(orderToReturn, typeof(DO.Order));// convert BO to DO
        //dal.Order.Update(orderToUpdate);// update in DAL
       // return orderToReturn;

        return order;

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


















        //try 
        //{ 
        //    BO.Order order = new BO.Order();
        //    DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException("order doesnt exists");
        //    order = DoOrderToBo(temp); //casting from bo to do
        //    BO.OrderTracking ot = new BO.OrderTracking();
        //    ot.ID = ID;
        //    ot.Status = getStatus(order);
        //    return ot;
        //}
        //catch (DO.DoesntExistExeption exp)
        //{
        //    throw new BO.DoesntExistException(exp.Message, exp); 
        //}



    }


}