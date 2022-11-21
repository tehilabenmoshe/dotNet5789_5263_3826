
using DalApi;
using DO;

namespace Dal;

public class DalProduct : IProduct
{


    DataSource ds = DataSource.s_instance;

    public void Update(Product p)
    {
        if (!ds.ListProduct.Exists(i => i?.ID == p.ID))
            throw new Exception("cannot update a product that not exsist");
        Product? tempProduct = ds.ListProduct.Find(i => i?.ID == p.ID);
        ds.ListProduct.Remove(tempProduct); //remove
        ds.ListProduct.Add(p); //adds
    }

    public int Add(Product p)
    {
        if (ds.ListProduct.FirstOrDefault() != null) //if the list empty- retruns value. else-return a value
            throw new NotImplementedException();
        p.ID = DataSource.Config.NextOrderNbumber;
        ds.ListProduct.Add(p);    //pushing the order to the list
        return p.ID; //return the id of the order
    }

    public void Delete(int id)
    {
        if (ds?.ListProduct.RemoveAll(o => o?.ID == id) == 0)
            throw new Exception("can't delete that does not exist");
    }

    public Product GetById(int id) => ds.ListProduct.FirstOrDefault() ?? throw new Exception("missing product id");


    //public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter) =>
    //(filter == null ?
    //ds?.ListProduct.Select(item => item) :
    //ds?.ListProduct.Where(filter))

    //?? throw new Exception("Missing Product");

}

