
using DalApi;
using DO;

namespace Dal;
public class DalOrderItem:IOrderItem
{

    DataSource ds = DataSource.s_instance;
    public readonly Random rand = new Random();
    public int Add(OrderItem o)//adding a new orderItem to the list
    {
        OrderItem? temp = ds.ListOrderItem.Find(i => i?.ID == o.ID);
        if (temp != null) //if orderItem with the received id exists throw
            throw new Exception("allready exist");
        ds.ListOrderItem.Add(o); //pushing the orderItem to the list
        return o.ID; //return the id of the order item
    }

    public void Delete(int id)//deleting existing orderItem from the list
    {
        if (ds?.ListOrderItem.RemoveAll(o => o?.ID == id) == 0)
            throw new Exception("can't delete that does not exist");
    }


    public void Update(OrderItem o)//updating an exsisting orderItem
    {
        OrderItem? tempRemove = ds.ListOrderItem.Find(i => i?.ID == o.ID);
        if (tempRemove==null) //if orderItem doesnt exists throw
            throw new Exception("cannot update the order item witch not exists");
       Delete(o.ID);//deletes the old orderItem
        Add(o);//updating the order by creating a new one
    }


    public OrderItem GetById(int id)//rturns the orderItem with the id that matches the received one 
    {
        OrderItem? temp = ds.ListOrderItem.Find(x => x?.ID == id);
        if (temp == null)//if the orderItem wasnt found
            throw new Exception("order is not exists");
        return (OrderItem)temp;
    }

    public OrderItem GetProductByOrderAndID(int orderId, int productId)
    {
        OrderItem? temp = ds.ListOrderItem.Find(x => x?.OrderID == orderId);
        if (temp == null)
            throw new Exception("order not exist");
        if (temp?.ProductID == productId)
            return (OrderItem)temp;
        else throw new Exception(" product not exist");
    }
    public IEnumerable<OrderItem> GetAll()//prints all the orderItems in the list
    {
        return (from OrderItem o in ds.ListOrderItem select o).ToList<OrderItem>();

    }


}
