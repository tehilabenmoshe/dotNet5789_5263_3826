
namespace DO;

/// <summary>
/// Structure for the Order entity. Contains the basic product fields
/// </summary>
public struct Order
{
    public override string ToString() => $@"
    Order ID:{ID} 
    Customer Name:{CustomerName}
    Customer Email:{CustomerEmail}
    Customer Adress:{CustomerAdress}
    Order Date: {OrderDate}
    Ship Date: {ShipDate}
    Delivery Date: {DeliveryDate}
    ";

    /// <summary>
    /// ID property receives and returns the orders ID
    ///  CustomerName property receives and returns the orders customer Name
    ///  CustomerEmail property receives and returns the orders CustomerEmail
    ///  CustomerAdress property receives and returns the orders CustomerAdress
    ///  OrderDate,ShipDate,DeliveryDate properties receives and returns the orders date,ship date, and delivery date.
    /// </summary>

    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }



}
