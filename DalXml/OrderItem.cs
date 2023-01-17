using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Security.Principal;

internal class OrderItem : IOrderItem
{

    const string s_orderItems = "orderItems"; //XML Serializer
    const string s_orders = "orders";


    public IEnumerable<DO.OrderItem?> getAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        return (List<OrderItem?>)(filter == null ? orderItems.OrderBy(o => ((DO.OrderItem)o!).ID)
                              : orderItems.Where(filter).OrderBy(o => ((DO.OrderItem)o!).ID));
    }

    public DO.OrderItem GetByID(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems).FirstOrDefault(o => o?.ID == id)
        //DalMissingIdException(id, "Lecturer");
        ?? throw new Exception("missing id");

    public int Add(DO.OrderItem orderItem)
    {
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (orderItem.ID < 1000 || orderItem.ID > 10000)
            orderItem.ID = ConfigOrderItem.getNumOrder();
        if (orderItems.Exists(lec => lec?.ID == orderItem.ID))
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
    public IEnumerable<OrderItem?> GetItemsList(int orderId)
    {
        var orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        Order? order = orders.Find(x => x.GetValueOrDefault().ID == orderId);

        if (order == null)

            throw new DoesntExistException("ההזמנה אינה קיימת");

        List<OrderItem?> listToReturn = new List<OrderItem?>();

        foreach (OrderItem? item in orderItems)
        {
            if (item != null && item?.OrderID == order?.ID)
                listToReturn.Add((OrderItem?)item);
        }
        return listToReturn;

    }

    public OrderItem GetProductByOrderAndID(int orderId, int productId)
    {
        var orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        OrderItem? temp = orderItems.Find(x => x?.OrderID == orderId);

        if (temp == null)
            throw new DoesntExistException("ההזמנה אינה קיימת");

        if (temp?.ProductID == productId)
            return (OrderItem)temp;

        else throw new DoesntExistException("הפריט אינו נמצא בהזמנה");
    }

    public OrderItem? getByFilter(Func<OrderItem?, bool>? filter)
    {
        var listOfOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems)!;
        listOfOrderItems.Where(filter).OrderBy(o => ((DO.OrderItem)o!).ID);
        return listOfOrderItems.FirstOrDefault();
    }
}
