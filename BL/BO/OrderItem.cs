using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// id of product 
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// the amount of this product in this order
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// the price of all the item from product type
    /// </summary>
    public double? TotalPrice { get; set; }
    /// <summary>
    /// function- print OrderItem class
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        ID={ID}
        Name={Name}
        Product ID={ProductID}
    	Price: {Price}
    	Amount: {Amount}
        Total price: {TotalPrice}
";

}
