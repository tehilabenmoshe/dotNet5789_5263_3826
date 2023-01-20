using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DO;

//namespace Dal;
//public struct ImportentNumbers
//{
//    public double numberSaved { get; set; }
//    public string typeOfnumber { get; set; }
//}

//sealed class DalXml : IDal
//{
//    public static IDal Instance { get; } = new DalXml();
//    private DalXml() { }

//    public IOrder Order { get; } = new Dal.Order();
//    public IProduct Product { get; } = new Dal.Product();
//    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
//}


namespace DO;

public enum Category
{
    garden, kitchen, livingRoom, bedRoom, diningRoom

}


public struct Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public double? Price { get; set; }
    public int? InStock { get; set; }

    public override string ToString() => this.ToStringProperty();
}


public struct Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public override string ToString() => this.ToStringProperty();
}


public struct OrderItem
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }


    public override string ToString() => this.ToStringProperty();
}