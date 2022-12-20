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
        BO.ProductForList temp = new BO.ProductForList();
        List<DO.Product> products = Dal.Product.GetAll().ToList();
        foreach (DO.Product p in products)
        {
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
                throw new BO.DoesntExistException();
            BO.Product p = new BO.Product();
            DO.Product temp=Dal?.Product.GetById(ID)?? throw new BO.DoesntExistException();
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
            throw new BO.InvalidInputExeption();
        BO.ProductItem p = new BO.ProductItem();
        DO.Product temp = Dal?.Product.GetById(ID) ?? throw new BO.DoesntExistException();
        p.ID = temp.ID; //לראות מה עושים עם cart
        p.Name = temp.Name;
        p.Price = temp.Price;
        p.Category = (BO.Category)temp.Category;
        if (temp.InStock > 0)
            p.InStock = true;
        else
            p.InStock = false;
        return p;
        // p.path=temp.path;?????

    }
    public void AddProduct(BO.Product product)
    {
        if(product.Name=="")
            throw new BO.InvalidInputExeption();
        if ((product.ID < 100000) || (product.ID > 999999))
            throw new BO.InvalidInputExeption(); 
        if(product.Price<0)
            throw new BO.InvalidInputExeption();
        if(product.InStock<0)
            throw new BO.InvalidInputExeption();
       
        if(Dal?.Product.GetById(product.ID) != null) //if the product already exsist in DO
            throw new BO.AlreadyExistExeption();

        DO.Product temp = new DO.Product();
        temp = ProductFromBOToDO(product);
        Dal?.Product.Add(temp);
    }
    public void DeledeProduct(int id) 
    {
        Dal?.Product.Delete(id);
    }
    public void UpdateDetailProduct(BO.Product? p)
    {
        if (p.Name == "")
            throw new BO.InvalidInputExeption();
        if (p.Price < 0)
            throw new BO.InvalidInputExeption();
        if (p.InStock < 0)
            throw new BO.InvalidInputExeption();
        if(p.ID<100000||p.ID>999999) 
            throw new BO.InvalidInputExeption();    
        DO.Product temp = new DO.Product();
        temp=Dal?.Product.GetById(p.ID)??throw new BO.DoesntExistException();//if the product was found put it in temp
        temp= ProductFromBOToDO(p);//update temp with the new data of p
    }



}
