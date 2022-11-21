using DalApi;
using DO;

namespace Dal;

internal class DalOrder: IOrder
{
    DataSource ds = DataSource.s_instance;
    
    public int Add(Order o)
    {
        if(ds.ListOrder.FirstOrDefault() != null) //if the list empty- retruns value. else-return a value
            throw new NotImplementedException();
        o.ID = DataSource.Config.NextOrderNbumber;
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
        if (!ds.ListOrder.Exists(i => i?.ID == o.ID)) //chek if exsist
            throw new Exception("cannot update the product witch not exists");

        Order? tempRemove = ds.ListOrder.Find(i => i?.ID == o.ID); //מוזר שלא עובד
        ds.ListOrder.Remove(tempRemove); //remove
        ds.ListOrder.Add(o); //adds

    }


    public Order GetById(int id) => ds.ListOrder.FirstOrDefault() ?? throw new Exception("missing order id");
    //{
    //    //Order temp = ds.ListOrder.Find(i => i.ID == id);

    //    //if (ds.ListOrder.FirstOrDefault() != null)
    //    //    throw new Exception("missing order id");
    //    //else
    //    //    return temp; 

    //}

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter) =>
        (filter == null ?
        ds?.ListOrder.Select(item => item) :
        ds?.ListOrder.Where(filter))

        ?? throw new Exception("Missing order");
  




}
