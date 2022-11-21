

namespace Dal;
using DalApi;
using DO;

public class DalOrderItem:IOrderItem
{

    DataSource ds = DataSource.s_instance;

    public int Add(OrderItem o)
    {
        if (ds.ListOrderItem.FirstOrDefault() != null) //if the list empty- retruns value. else-return a value
            throw new NotImplementedException();
        o.ID = DataSource.Config.NextOrderNbumber;
        ds.ListOrderItem.Add(o);    //pushing the order to the list
        return o.ID; //return the id of the order item
    }

    public void Delete(int id)
    {
        if (ds?.ListOrderItem.RemoveAll(o => o?.ID == id) == 0)
            throw new Exception("can't delete that does not exist");
    }


    public void Update(OrderItem o)
    {
        if (!ds.ListOrderItem.Exists(i => i?.ID == o.ID)) //chek if exsist
            throw new Exception("cannot update the order item witch not exists");

        OrderItem? tempRemove = ds.ListOrderItem.Find(i => i?.ID == o.ID); 
        ds.ListOrderItem.Remove(tempRemove); //remove
        ds.ListOrderItem.Add(o); //adds

    }


    public OrderItem GetById(int id) => ds.ListOrderItem.FirstOrDefault() ?? throw new Exception("missing order id");


    //public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter) =>
    //   (filter == null ?
    //   ds?.ListOrderItem.Select(item => item) :
    //   ds?.ListOrderItem.Where(filter))

    //   ?? throw new Exception("Missing order");










}
