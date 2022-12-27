using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;
//using BO;
using DalApi;
//using DO;

namespace BlImplementation;

internal class BoProduct: IBoProduct
{
    private IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.ProductForList> getProductForList()
    {
        List<BO.ProductForList?> listOfProducts = new List<BO.ProductForList?>();
        List<DO.Product> products = Dal.Product.GetAll().ToList();
        foreach (DO.Product p in products)
        {
            BO.ProductForList temp = new BO.ProductForList();
            temp.ID = p.ID;
            temp.Name = p.Name; 
            temp.Price= p.Price;
            temp.Category = (BO.Category)p.Category;
            listOfProducts.Add(temp);   
        }
        return listOfProducts;  
    } 
    public DO.Product ProductFromBOToDO(BO.Product p)
    {
        DO.Product temp=new DO.Product();
        temp.ID = p.ID;
        temp.Name = p.Name;
        temp.Price = p.Price;
        temp.InStock = p.InStock;
        temp.Category=(DO.Category)p.Category;
        return temp;
    }
    public BO.Product GetProductbyIdForManager(int ID)
    {
            if((ID<=100000)||(ID>=999999))
                throw new BO.InvalidInputExeption("ID is out of range"); ;
            BO.Product p = new BO.Product();
            DO.Product temp=Dal?.Product.GetById(ID)?? throw new BO.DoesntExistException("the product doesnt exists");
            p.ID = temp.ID;
            p.Name = temp.Name;
            p.Price=temp.Price;
            p.Category = (BO.Category)temp.Category;
            p.InStock= temp.InStock;
            // p.path=temp.path;?????
            return p;

    }
    public BO.ProductItem GetProductByIDAndCartForCostumer(int ID, BO.Cart cart)
    {
        if ((ID <= 100000) || (ID >= 999999))
            throw new BO.InvalidInputExeption("ID is out of range");
        BO.ProductItem p = new BO.ProductItem();
        DO.Product temp = Dal?.Product.GetById(ID) ?? throw new BO.DoesntExistException("Product doesnt exists");
        p.ID = temp.ID; //לראות מה עושים עם cart
        p.Name = temp.Name;
        p.Price = temp.Price;
        p.Category = (BO.Category)temp.Category;
        if (temp.InStock > 0)
        {
            p.InStock = true;
        }
        else   
            p.InStock = false;

        BO.OrderItem orderItem = new BO.OrderItem();
        orderItem=cart.Items.Find((x => x?.ProductID == ID));
        p.Amount = orderItem.Amount;

        return p;
    }
    public void AddProduct(BO.Product product)
    {
        if(product.Name=="")
            throw new BO.InvalidInputExeption("the Name must contain at least letter");
        if ((product.ID < 100000) || (product.ID > 999999))
            throw new BO.InvalidInputExeption("ID is out of range"); 
        if(product.Price<0)
            throw new BO.InvalidInputExeption("Price is out of range");
        if(product.InStock<0)
            throw new BO.InvalidInputExeption("Product is out of stock");
        try
        {
            if (Dal?.Product.GetById(product.ID) != null) //if the product already exsist in DO
            {

                throw new BO.AlreadyExistExeption("Product already exists");
            }
        }
        catch (DO.DoesntExistExeption ex)// if product doesnt exists
        {
            DO.Product temp = new DO.Product();
            temp = ProductFromBOToDO(product);
            Dal?.Product.Add(temp);
        }
       
    }
    public void DeledeProduct(int IDProduct)
    {
        //List<DO.Order?> tempList = (List<DO.Order?>)Dal.Order.GetAll();// create temp list to get all orders from DAL
        //foreach (DO.Order? o in tempList)// go over the list of orders
        //{
        //    List<DO.OrderItem?> itemsInO = new List<DO.OrderItem?>();// create orderItem list for testing
        //    try
        //    {

        //        itemsInO = (List<DO.OrderItem?>)Dal.OrderItem.GetItemsList((int)(o?.ID!));
        //        if (itemsInO.Find((x => x?.ProductID == IDProduct)) != null)// product was found in order
        //            throw new BO.CantDeleteItem("Product exists in an order, cannot be deleted");

        //    }
        //    catch (BO.CantDeleteItem ex)// exeption for product in order case
        //    {
        //        throw new BO.CantDeleteItem(ex.Message, ex);
        //    }
        //}
        try
        {

            Dal.Product.Delete(IDProduct); // deleting from DAL
        }
        catch (DO.DoesntExistExeption ex)// if doesnt work catch exeption
        {
            throw new BO.DoesntExistException(ex.Message, ex);
        }
    }
    public void UpdateDetailProduct(BO.Product? product)
    {
        if (!(product?.ID >= 100000 && product?.ID < 1000000))// id test
            throw new BO.InvalidInputExeption("ID is out of range");

        if (product?.Name == "")// name test 
            throw new BO.InvalidInputExeption("Name is not correct");

        if (product?.Price <= 0)// price test
            throw new BO.InvalidInputExeption("Price is out of range");

        if (product?.InStock < 0)// stock test
            throw new BO.InvalidInputExeption("Product is out of stock");

        try
        {
            DO.Product productTempDO = new DO.Product()// create DO product to update in DAL
            {
                ID = product!.ID,
                Name = product?.Name,
                Price = product?.Price,
                Category = (DO.Category)product?.Category,
                InStock = product?.InStock
            };

            Dal.Product.Update(productTempDO); // updating
        }
        catch (DO.DoesntExistExeption ex)// if doesnt work catch exeption
        {
            throw new BO.DoesntExistException(ex.Message, ex);
        }
    }


}
