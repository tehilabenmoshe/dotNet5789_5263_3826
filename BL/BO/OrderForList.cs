using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// id of the order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// purchaser name 
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public Status? OrderStatus { get; set; }
    /// <summary>
    /// The amount of items the customer ordered
    /// </summary>
    public int? AmountOfItems { get; set; }
    /// <summary>
    /// the total price of this order
    /// </summary>
    public double? TotalPrice { get; set; }

    /// <summary>
    /// function- print OrderForList class
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        ID={ID}
        Customer name={CustomerName}
        Order status={OrderStatus}
        Amount of items: {AmountOfItems}
    	Total price: {TotalPrice}
";

}
