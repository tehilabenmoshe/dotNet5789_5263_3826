using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
//using BO;
using DalApi;

namespace BlImplementation;

internal class BoProduct: IBoProduct
{
    private IDal? Dal = DalApi.Factory.Get();
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
    public ProductItem GetProductByIDAndCartForCostumer(int ID, Cart cart)
    {
        if ((ID <= 100000) || (ID >= 999999))
            throw new BO.InvalidInputExeption();
        BO.ProductItem pi = new BO.ProductItem();
        DO.Product temp = Dal?.Product.GetById(ID) ?? throw new BO.DoesntExistException();
        p.ID = temp.ID;
        p.Name = temp.Name;
        p.Price = temp.Price;
        p.Category = (BO.Category)temp.Category;
        p.InStock = temp.InStock;
        // p.path=temp.path;?????
        return p;

    }
}
