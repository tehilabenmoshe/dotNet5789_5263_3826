using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DO;

namespace Dal;
public struct ImportentNumbers
{
    public double numberSaved { get; set; }
    public string typeOfnumber { get; set; }
}

sealed class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public IOrder Order { get; } = new Dal.Order();
    public IProduct Product { get; } = new Dal.Product();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}


