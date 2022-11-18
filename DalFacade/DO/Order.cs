
namespace DO;


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
    /// Unique ID of ...
    /// </summary>

    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }



}
