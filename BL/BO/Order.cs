using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    /// <summary>
    /// id of order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// purchaser name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// purchaser email
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// purchaser address
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// the date of making order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// date of the pay on the order
    /// </summary>
    public DateTime? PaymantDate { get; set; }
    /// <summary>
    /// ship date of order
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// delivery date of order 
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// List of ordered items
    /// </summary>
    public List<OrderItem?> Items { get; set; }
    /// <summary>
    /// total price of order
    /// </summary>
    public double? TotalPrice { get; set; }
    /// <summary>
    /// function- print order class
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        ID={ID}
        Customer name: {CustomerName}
        Customer email: {CustomerEmail}
        Customer address: {CustomerAddress}
        Date of order: {OrderDate}
        Order status: {Status}
        Date of paymant: {PaymantDate}
        Date of ship:{ShipDate}
        Date of delivery: {DeliveryDate}
        Items: {Items}
        Total price {TotalPrice}
";
}
