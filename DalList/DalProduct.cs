
using DalApi;
using DO;
namespace Dal;

public class DalProduct : IProduct
{
    DataSource ds = DataSource.s_instance;
    public void Update(Product p)//updating an exsisting product
    {
        Product? tempProduct = ds.ListProduct.Find(i => i?.ID == p.ID);
        if (tempProduct==null)//if product doesnt exists throw
            throw new DoesntExistExeption("cannot update a product that not exsist");
       Delete(p.ID); //deletes the old product
        Add(p); //updating the product by creating a new one
    }
    public int Add(Product p)//adding a new product to the list
    {
        Product? temp = ds.ListProduct.Find(i => i?.ID == p.ID);
        if (temp != null) //if the product with the received id exists throw
            throw new AlreadyExistExeption("allready exist");
        ds.ListProduct.Add(p);//pushing the product to the list
        return p.ID; //return the id of the product
    }
    public void Delete(int id)//deleting existing product from the list
    {
        if (ds?.ListProduct.RemoveAll(o => o?.ID == id) == 0)
            throw new DoesntExistExeption("can't delete that does not exist");
    }
    public Product GetById(int id)//rturns the product with the id that matches the received one  
    {
        Product? temp = ds.ListProduct.Find(i => i?.ID == id);
        if (temp == null)//if the product wasnt found
            throw new DoesntExistExeption("product does not exist");
        return(Product)temp;

    }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        return(from Product? p in ds.ListProduct select p).ToList<Product?>();//returns all the products in the list

    }
}

