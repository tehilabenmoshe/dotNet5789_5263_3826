
using DalApi;
using DO;

namespace Dal;

public class DalProduct:IProduct
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
}
