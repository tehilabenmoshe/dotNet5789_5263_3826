using DO;

namespace DalApi;
///Inherited from Icrud to allow implementation in dalOrderItem
public interface IOrderItem : ICrud<OrderItem>
{
    IEnumerable<OrderItem?> GetItemsList(int orderId);
    OrderItem GetProductByOrderAndID(int orderId, int productId);


}
