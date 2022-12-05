using DalApi;
using DO;

namespace Dal;

public class DalOrder: IOrder
{
    DataSource ds = DataSource.s_instance;
    public readonly Random rand = new Random();

    public int Add(Order o)//adding a new order to the list
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID == o.ID);
        if (temp != null) //if order with the received id exists throw
            throw new AlreadyExistExeption("allready exists");
        o.OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
        o.ShipDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 50L));
        o.DeliveryDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 20L));
        ds.ListOrder.Add(o);    //pushing the order to the list
        return o.ID; //return the id of the order
    }

    public void Delete(int id)//deleting existing order from the list
    {
        if (ds?.ListOrder.RemoveAll(o => o?.ID == id) == 0)
            throw new DoesntExistExeption("can't delete that does not exist");
    }

    public void Update(Order o)//updating an exsisting order
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID == o.ID);
        if (temp == null) //if order doesnt exists throw
            throw new DoesntExistExeption("cannot update the order that doesnt exists");
       Delete(o.ID); //deletes the old order
        Add(o); //updating the order by creating a new one

    }

    public Order GetById(int id) //rturns the order with the id that matches the received one 
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID ==id);
        if (temp==null)//if the order wasnt found
            throw new DoesntExistExeption("missing order id");
        return (Order)temp;
    }

    public IEnumerable<Order> GetAll()//prints all the orders in the list
    {
        return (from Order o in ds.ListOrder select o).ToList<Order>();
    }

}
