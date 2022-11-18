

namespace DO;

/// <summary>
/// Structure for ...
/// </summary>
public struct OrderItem
{
    public override string ToString() => $@"
    Item ID:{ID}
    Product ID:{ProductID}
    Order ID:{OrderID}
    Price: {Price}
    Amount: {Amount}
    ";

    /// <summary>
    /// Unique ID of ...
    /// </summary>

    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; } 
    public double Price { get; set; }
    public int Amount { get; set; }





}
