using DalApi;
using DO;

namespace Dal;

public class DalOrder: IOrder
{
    DataSource ds = DataSource.s_instance;
    public readonly Random rand = new Random();

    public int Add(Order o)
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID == o.ID);
        if (temp != null) //if the list empty- retruns value. else-return a value
            throw new Exception("allready exists");
        o.OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
        o.ShipDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 50L));
        o.DeliveryDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 20L));
        ds.ListOrder.Add(o);    //pushing the order to the list
        return o.ID; //return the id of the order
    }

    public void Delete(int id)
    {
        if (ds?.ListOrder.RemoveAll(o => o?.ID == id) == 0)
            throw new Exception("can't delete that does not exist");
    }

    public void Update(Order o)
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID == o.ID);
        if (temp == null) 
            throw new Exception("cannot update the product witch not exists");
       Delete(o.ID); //remove
       Add(o); //adds

    }

    public Order GetById(int id) 
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID ==id);
        if (temp==null)
            throw new Exception("missing order id");
        return (Order)temp;
    }

    public IEnumerable<Order> GetAll()
    {
        return (from Order o in ds.ListOrder select o).ToList<Order>();
    }

}
