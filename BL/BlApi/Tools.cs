
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


    //DO to BO
    public static Target CopyPropTo<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null)
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p);

            IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

            foreach (var propertyInfo in propertiesInfoSource)
            {
                if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
                    && (propertyInfo.PropertyType == typeof(string) || !propertyInfo.PropertyType.IsClass))
                {
                    propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
                }
            }
        }
        return target;
    }
    // BO to DO
    public static object CopyPropToStruct<S>(this S from, Type type)//get the type we want to copy to 
    {
        object copy = Activator.CreateInstance(type); // new object of the Type
        from.CopyPropTo(copy);//copy הכל value of properties with the same name to the new object
        return copy!;
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
            return OrderStatus.delivered;
        else if (order.ShipDate != null && order.ShipDate < DateTime.Now)
            return OrderStatus.shipped;
        else if (order.OrderDate != null && order.OrderDate < DateTime.Now)
            return OrderStatus.ordered;
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

    //public static int GetAmountOfProductItemInCart(int id)
    //{

    //}
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

