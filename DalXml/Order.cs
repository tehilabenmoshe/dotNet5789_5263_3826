using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DalApi;
using DO;
using System.Security.Principal;
namespace Dal;
internal class Order : IOrder
{
    const string s_orders = "orders"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOfOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return (filter == null ? listOfOrders.OrderBy(o => ((DO.Order)o!).ID)
                              : listOfOrders.Where(filter).OrderBy(o => ((DO.Order)o!).ID));
    }
   
    public DO.Order GetByID(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(o => o?.ID == id)
        //DalMissingIdException(id, "Lecturer");
        ?? throw new Exception("The Order Dosent Exsist In Sistem");

    public int Add(DO.Order order)
    {
        var listOfOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (order.ID < 1000 || order.ID > 9999)
            order.ID = ConfigOrder.getNumOrder();

        if (listOfOrders.Exists(o => o?.ID == order.ID))
            throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

        listOfOrders.Add(order);

        XMLTools.SaveListToXMLSerializer(listOfOrders, s_orders);

        return order.ID;
    }

    public void Delete(int id)
    {
        var ListOfOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (ListOfOrders.RemoveAll(p => p?.ID == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(ListOfOrders, s_orders);
    }

    public void Update(DO.Order o)
    {
        Delete(o.ID);
        Add(o);
    }


}




