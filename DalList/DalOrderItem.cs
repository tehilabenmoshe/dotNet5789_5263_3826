
using DalApi;
using DO;

namespace Dal;
public class DalOrderItem:IOrderItem
{

    DataSource ds = DataSource.s_instance;
    public readonly Random rand = new Random();
    public int Add(OrderItem o)
    {
        OrderItem? temp = ds.ListOrderItem.Find(i => i?.ID == o.ID);
        if (temp != null) //if the list empty- retruns value. else-return a value
            throw new Exception("allready exist");
        ds.ListOrderItem.Add(o);
        return o.ID; //return the id of the order item
    }

    public void Delete(int id)
    {
        if (ds?.ListOrderItem.RemoveAll(o => o?.ID == id) == 0)
            throw new Exception("can't delete that does not exist");
    }


    public void Update(OrderItem o)
    {
        OrderItem? tempRemove = ds.ListOrderItem.Find(i => i?.ID == o.ID);
        if (tempRemove==null) //chek if exsist
            throw new Exception("cannot update the order item witch not exists");
       Delete(o.ID); //remove
       Add(o); //adds
    }


    public OrderItem GetById(int id)
    {
        OrderItem? temp = ds.ListOrderItem.Find(x => x?.ID == id);
        if (temp == null)
            throw new Exception("order is not exists");
        return (OrderItem)temp;
    }

    public OrderItem GetProductByOrderAndID(int orderId, int productId)
    {
        OrderItem? temp = ds.ListOrderItem.Find(x => x?.OrderID == orderId);

        //OrderItem? temp1 = ds.OrderItems.Find(x => x?.ProductID == productId);
        if (temp == null)
            throw new Exception("order not exist");
        if (temp?.ProductID == productId)
            return (OrderItem)temp;
        else throw new Exception(" product not exist");
    }
    public IEnumerable<OrderItem> GetAll()
    {
        return (from OrderItem o in ds.ListOrderItem select o).ToList<OrderItem>();

    }


}
