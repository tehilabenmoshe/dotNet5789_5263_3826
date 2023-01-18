using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//using DalApi;
using DO;
using System.Security.Principal;
namespace Dal;
internal class Order : DalApi.IOrder
{
    const string s_orders = "orders"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        var listOrder = (List<DO.Order?>)XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;

        if (filter == null)
            return listOrder.Select(p => p).OrderBy(o => ((DO.Order)o!).ID);
        else
            return listOrder.Where(filter).OrderBy(o => ((DO.Order)o!).ID);
    }

    //public DO.Order GetByID(int id) =>
    //    XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(o => o?.ID == id)
    //    //DalMissingIdException(id, "Lecturer");
    //    ?? throw new Exception("The Order Dosent Exsist In Sistem");


    public DO.Order GetById(int id)
    {
        var listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrder.Exists(o => o?.ID == id))
            return (DO.Order)(listOrder.FirstOrDefault(o => o?.ID == id));
        else
            throw new Exception("missing id");//DalMissingIdException(id, "Lecturer");
    }

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




