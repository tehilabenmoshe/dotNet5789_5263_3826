using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
//using DalApi;
using DO;
using System.Security.Principal;
using System.Xml.Linq;

internal class OrderItem : DalApi.IOrderItem
{
    const string s_orderItems = "orderItems"; //XML Serializer
    const string s_orders = "orders";


    //שיטת XML Serializer


    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        //var listOrderItem =XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        //return (filter == null ? listOrderItem.OrderBy(o => ((DO.OrderItem)o!).ID):
        //    listOrderItem.Where(filter).
        //OrderBy(o => ((DO.OrderItem)o!).ID));


        var listOrderItem = (List<DO.OrderItem?>)XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;

        if (filter == null)
            return listOrderItem.Select(p => p).OrderBy(lec => ((DO.OrderItem)lec!).ID);
        else
            return listOrderItem.Where(filter).OrderBy(lec => ((DO.OrderItem)lec!).ID);


    }

    //public DO.OrderItem GetByID(int id) =>
    //    XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(o => o?.ID == id)
    //    ?? throw new Exception("missing id");


    public DO.OrderItem GetById(int id)
    {
        var listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (listOrderItem.Exists(o => o?.ID == id))
            return (DO.OrderItem)(listOrderItem.FirstOrDefault(o => o?.ID == id));
        else
            throw new Exception("missing id");//DalMissingIdException(id, "Lecturer");
    }

    public int Add(DO.OrderItem orderItem)
    {
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (orderItem.ID < 1000 || orderItem.ID > 9999)
            orderItem.ID = ConfigOrderItem.getNumOrder();
        if (orderItems.Exists(o => o?.ID == orderItem.ID))
            throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

        orderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer(orderItems, s_orderItems);

        return orderItem.ID;
    }

    public void Delete(int id)
    {
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);

        if (orderItems.RemoveAll(p => p?.ID == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(orderItems, s_orderItems);
    }

    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.ID);
        Add(orderItem);
    }

    public IEnumerable<DO.OrderItem?> GetItemsList(int orderId)
    {
        var orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        DO.Order? order = orders.Find(x => x.GetValueOrDefault().ID == orderId);

        if (order == null)

            throw new Exception("Order Dosent Exsist");

        List<DO.OrderItem?> listToReturn = new List<DO.OrderItem?>();

        foreach (DO.OrderItem? item in orderItems)
        {
            if (item != null && item?.OrderID == order?.ID)
                listToReturn.Add((DO.OrderItem?)item);
        }
        return listToReturn;

    }

    public DO.OrderItem GetProductByOrderAndID(int orderId, int productId)
    {
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems); //load orderItem list from xelment
        DO.OrderItem? temp = orderItems.Find(o => (o?.OrderID) == orderId);
      
        if (temp == null)
           throw new Exception("The Order Dosen`t Exsist in sistem");

        if (temp?.ProductID == productId)
            return (DO.OrderItem)temp;
        else
            throw new Exception("product doesnt exist");

        //if (XMLTools.LoadListFromXMLElement(s_orderItems)?.Elements()
        //.FirstOrDefault(oi => oi.ToIntNullable("OrderID") == orderId) is not null)
        //    throw new DoesntExistExeption("order doesnt exist");




    }

}
