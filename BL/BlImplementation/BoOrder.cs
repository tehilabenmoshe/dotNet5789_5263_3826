using System;
using System.Collections.Generic;
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
        List<BO.OrderForList?> listOfOrders=new List<BO.OrderForList>();
        BO.OrderForList temp = new BO.OrderForList();
       foreach(DO.Order or in order)
       {
             temp.ID = or.ID;
             temp.CustomerName = or.CustomerName;
             listOfOrders.Add(temp); 
       }
        return listOfOrders;
    }


    public BO.Order GetOrder(int ID)
    {
        if ((ID <= 100000) || (ID >= 999999))
            throw new BO.DoesntExistException();
        DO.Order temp = Dal?.Order.GetById(ID) ?? throw new BO.DoesntExistException();
        BO.Order order = new BO.Order();
        order.ID = temp.ID; 
        order.CustomerName = temp.CustomerName;
        order.CustomerEmail = temp.CustomerEmail;
        order.CustomerAddress = temp.CustomerAddress;
        order.OrderDate=temp.OrderDate;
        order.ShipDate = temp.ShipDate;
        order.DeliveryDate = temp.DeliveryDate;
        order.PaymantDate= DateTime.MinValue;
        order.Status = getStatus(order); //create a function that return the current status of the order
        List<DO.OrderItem> doList = Dal!.OrderItem.GetById(order.ID);
        //  List<BO.OrderItem> o =new List<BO.OrderItem>();
     // foreach()


       // order.Items=
       // order.TotalPrice=




    internal BO.OrderStatus getStatus(BO.Order o)
    {
        BO.OrderStatus s=new BO.OrderStatus();
        if (o.ShipDate == null)
        {
            s= BO.OrderStatus.approved;
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



}
