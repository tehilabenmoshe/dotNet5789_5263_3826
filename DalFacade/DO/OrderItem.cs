

using DalApi;

namespace DO;

/// <summary>
/// Structure for the OrderItem entity. Contains the basic product fields
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
    ///ID property receives and returns the orderItem ID
    ///  OrderID property receives and returns the orderItem ID
    ///  ProductID property receives and returns the products ID in orderItem 
    ///  Price property receives and returns the orderItem price
    ///  Amount property receives and returns the orderItems Amount
    /// </summary>

    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; } 
    public double Price { get; set; }
    public int Amount { get; set; }


}
