
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

}
