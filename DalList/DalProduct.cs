
using DalApi;
using DO;

namespace Dal;

public class DalProduct : IProduct
{
    DataSource ds = DataSource.s_instance;
    public void Update(Product p)
    {
        Product? tempProduct = ds.ListProduct.Find(i => i?.ID == p.ID);
        if (tempProduct==null)
            throw new Exception("cannot update a product that not exsist");
       Delete(p.ID); //remove
        Add(p); //adds
    }

    public int Add(Product p)
    {
        Product? temp = ds.ListProduct.Find(i => i?.ID == p.ID);
        if (temp != null) //if the list empty- retruns value. else-return a value
            throw new Exception("allready exist");
        ds.ListProduct.Add(p);    //pushing the order to the list
        return p.ID; //return the id of the order
    }

    public void Delete(int id)
    {
        if (ds?.ListProduct.RemoveAll(o => o?.ID == id) == 0)
            throw new Exception("can't delete that does not exist");
    }

    public Product GetById(int id) //=> ds.ListProduct.FirstOrDefault() ?? throw new Exception("missing product id");
    {
        Product? temp = ds.ListProduct.Find(i => i?.ID == id);
        if (temp == null)
            throw new Exception("product does not exist");
        return(Product)temp;

    }

    public IEnumerable<Product> GetAll()
    {
        return(from Product p in ds.ListProduct select p).ToList<Product>();

    }
}

