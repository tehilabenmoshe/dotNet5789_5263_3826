using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public static class Tools
{
    static private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");

    public static string ToStringProperty<T>(this T? t)
    {
        string str = "";
    }


}
