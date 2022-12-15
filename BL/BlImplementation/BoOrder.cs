using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;
using DalApi;

namespace BlImplementation;

internal class BoOrder : IBoOrder
{
    private IDal? Dal = DalApi.Factory.Get();
    public IEnumerable<BO.OrderForList> getOrderForList()
    {
        List<DO.Order> order = Dal.Order.GetAll().ToList();
        List<BO.OrderForList?> listOfOrders = new List<BO.OrderForList>();
        BO.OrderForList temp = new BO.OrderForList();
        foreach (DO.Order or in order)
        {
            temp.ID = or.ID;
            temp.CustomerName = or.CustomerName;
            listOfOrders.Add(temp);
        }
        return listOfOrders;
    }
    internal BO.OrderStatus getStatus(BO.Order o) //create a function that return the current status of the order
    {
        BO.OrderStatus s = new BO.OrderStatus();
        if (o.ShipDate == null)
        {
            s = BO.OrderStatus.approved;
        }
        else
        {
            if (o.DeliveryDate == null)
            {
                s = BO.OrderStatus.sent;
            }
            else
                s = BO.OrderStatus.provided;
        }
        return s;
    }
    public BO.Order GetOrder(int ID)
    {
        if ((ID <= 100000) || (ID >= 999999))
            throw new BO.DoesntExistException();
        DO.Order o = Dal?.Order.GetById(ID) ?? throw new DO.InvalidInputExeption();
        BO.Order temp=new BO.Order();
        temp = DoOrderToBo(o);
        return temp;
    }
    internal BO.Order? DoOrderToBo(DO.Order temp)
    {
        //if(temp==null)
        //return null;
       // DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException();
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

        List<DO.OrderItem> doList = Dal!.OrderItem.GetListByOrderID(order.ID);
        List<BO.OrderItem> boList = new List<BO.OrderItem>();
        foreach (DO.OrderItem d in doList) //copy each order item from do to bo
        {
            BO.OrderItem item = new BO.OrderItem();
            item.ID = d.ID;
            item.ProductID = d.ProductID;
            item.Price = d.Price;
            item.Amount = d.Amount;
            item.TotalPrice = item.Price * item.Amount;
            boList.Add(item);
        }
        order.Items = boList; //adds thr nes list of order items that copied from do to order

        foreach (BO.OrderItem or in order.Items) //summing the total-price of each order item in order
        {
            order.TotalPrice += or.TotalPrice;
        }
        return order;
    }
    public BO.Order UpdateShipOrder(int ID)
    {
        if ((ID <= 100000) || (ID >= 999999))//check the id
            throw new BO.DoesntExistException();
        BO.Order order = new BO.Order();
        DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException();
        order=DoOrderToBo(temp); //casting from bo to do  




        if(order.Status!=BO.OrderStatus.sent) //of the order isnt sent yet
        {
            order.Status = BO.OrderStatus.sent; //update the status to sent 
            order.ShipDate = DateTime.Now;//update the ship date in bo
            temp.ShipDate = order.ShipDate;//update the ship date in do
        }
        return order;
    }
    public BO.Order UpdateProvisionOrder(int ID)
    {
        if ((ID <= 100000) || (ID >= 999999))//check the id
            throw new BO.DoesntExistException();
        BO.Order order = new BO.Order();
        DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException();
        order = DoOrderToBo(temp); //casting from bo to do
        
        if(order.Status != BO.OrderStatus.provided)
        {
            order.Status = BO.OrderStatus.provided; //update the status to provided
            order.DeliveryDate = DateTime.Now;//update the DeliveryDate date in bo
            temp.DeliveryDate = order.ShipDate;//update the DeliveryDate date in do
        }
        return order;

    }
    public BO.OrderTracking TrackOrder(int ID)
    {
        if ((ID <= 100000) || (ID >= 999999))//check the id
            throw new BO.DoesntExistException();
        BO.Order order = new BO.Order();
        DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException();
        order = DoOrderToBo(temp); //casting from bo to do
        BO.OrderTracking ot=new BO.OrderTracking();
        ot.ID = ID;
        ot.Status = getStatus(order);
        return ot;
    }

    //אפשר להוסיף מתודה של בונוססססססססס



}