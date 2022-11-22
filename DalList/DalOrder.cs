using DalApi;
using DO;

namespace Dal;

public class DalOrder: IOrder
{
    DataSource ds = DataSource.s_instance;
    
    public int Add(Order o)
    {
        Order? temp = ds.ListOrder.Find(i => i?.ID == o.ID);
        if (temp != null) //if the list empty- retruns value. else-return a value
            throw new Exception("allready exist");
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

        Order? tempRemove = ds.ListOrder.Find(i => i?.ID == o.ID);
        ds.ListOrder.Remove(tempRemove); //remove
        ds.ListOrder.Add(o); //adds

    }

    public Order GetById(int id) 
    {

        if(ds==null)
            throw new Exception("missing order id");

        foreach(Order temp in ds.ListOrder)
        {
            if (temp.ID == id)
                return temp;
        }
        throw new Exception("missing order id");

    }

    public IEnumerable<Order> GetAll()
    {
        return (from Order o in ds.ListOrder select o).ToList<Order>();
    }




}
