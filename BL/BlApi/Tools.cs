
using BO;
using DalApi;
using System.Reflection;
using System.Collections;
using DO;
using System.Diagnostics;

namespace BlApi;

public static class Tools
{

    public static string ToStringProperty<T>(this T t, string suffix = "")
    //מתודה להפיכת ישות למחרוזת לצורך הצגת הפרטים
    {
        string str = "";
        foreach (PropertyInfo prop in t!.GetType().GetProperties())
        {
            var value = prop.GetValue(t, null);
            if (value is not string && value is IEnumerable)
            {
                str = str + "\n" + prop.Name + ":";
                foreach (var item in (IEnumerable)value)
                    str += item.ToStringProperty("      ");
            }

            else
                str += "\n" + suffix + prop.Name + ": " + value;
        }
        str += "\n";
        return str;
    }
    public static int getAmountOfProduct(List<BO.OrderItem> orderItemsList, int productId)
    {
        BO.OrderItem? temp = orderItemsList.Find(x => x?.ProductID == productId);
        if (temp != null)
        {
            return (int)temp?.Amount!;
        }
        return 0;

    }
    public static OrderStatus GetStatus(DO.Order order)
    {
        if (order.DeliveryDate != null && order.DeliveryDate < DateTime.Now)
            return OrderStatus.provided;
        else if (order.ShipDate != null && order.ShipDate < DateTime.Now)
            return OrderStatus.sent;
        else if (order.OrderDate != null && order.OrderDate < DateTime.Now)
            return OrderStatus.approved;
        else
            return OrderStatus.none;

    }
    public static int GetAmountOfItems(IEnumerable<DO.OrderItem?> orderFromBL)
    {
        int? sum = 0;
        foreach (DO.OrderItem? o in orderFromBL)
        {
            sum = sum + o?.Amount;
        }
        return (int)sum;
    }
    public static double GetTotalPrice(IEnumerable<DO.OrderItem?> ListItems)
    {
        double? total = 0;
        foreach (DO.OrderItem? o in ListItems)
        {
            total = total + o?.Price * o?.Amount;
        }
        return (int)total;
    }
}

