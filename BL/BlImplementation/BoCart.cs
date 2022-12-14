using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;


namespace BlImplementation;

internal class BoCart: IBoCart
{
    private IDal? Dal = DalApi.Factory.Get();

    public BO.Cart AddProductToCart(BO.Cart? cart, int id)
    {
        if (cart==null) //if cart-null
            cart=new BO.Cart(); //create new cart

        if ((id < 100000) || (id > 999999)) //if the id is wrong
            throw new BO.InvalidInputExeption();

        DO.Product tmp = new DO.Product();
        tmp = Dal!.Product.GetById(id);//get the product from DO

        BO.OrderItem orderToAdd=new BO.OrderItem(); //createan orderitem in order to add him to the cart

        if (!(cart.Items!.Exists(crt => crt.ID == id))) //if the cart dosent exsist in DO
        {
            
                orderToAdd.ProductID = tmp.ID;
                orderToAdd.Price=tmp.Price;
                orderToAdd.Amount = 1;
                cart.Items.Add(orderToAdd);//add the orderitem to the cart
            
        }
        else
        {
            if (tmp.InStock > 0)
            {
                BO.OrderItem? o = cart.Items.FirstOrDefault(o => o.ProductID == id);
                o!.Amount++; //update the amount
                cart.TotalPrice += o.Price; //update the total price of the cart
            }
            else
                throw new BO.DoesntExistException();
        }
        return cart; //return the cart
    }
}

public BO.Cart UpdateProductInCart(BO.Cart cart, int id, int newAmount)
{
    BO.OrderItem? tmp = new BO.OrderItem();
    tmp= cart.Items.FirstOrDefault(o => o.ProductID == id); //find the item in the cart
   if(tmp==null)
         throw new BO.DoesntExistException();

   if(cart==null)
        throw new BO.DoesntExistException();

    if (newAmount > tmp.Amount)
    {
        DO.Product p = new DO.Product();
        if (p.InStock > 0) //if there is product
        {
            cart.TotalPrice += tmp.Price*(newAmount - tmp.Amount); //update the total price of the cart-add the extra price from the new amount
            tmp.Amount = newAmount;
        }
        else
            throw new BO.DoesntExistException();
    }

    if (newAmount < tmp.Amount)
    {
        cart.TotalPrice -= tmp.Price * ( tmp.Amount- newAmount); //update the total price of the cart-add the extra price from the new amount
        tmp.Amount = newAmount;
    }

    if (newAmount == 0)
    {
        cart.TotalPrice -= tmp.Price * tmp.Amount;
        Dal?.Product.Delete(id);
    }
    return cart;
}


public void MakeCart(BO.Cart? cart)
{
    //check input:
    if(cart==null) throw new BO.InvalidInputExeption();
    if (cart.CustomerName=="") throw new BO.InvalidInputExeption();
    if (cart.CustomerAddress == "") throw new BO.InvalidInputExeption();
    if (cart.CustomerEmail == ""|| cart.CustomerEmail!.Contains("@"))
        throw new BO.InvalidInputExeption();

    //copy data:
    DO.Order order = new DO.Order();
    order.CustomerName = cart.CustomerName;
    order.CustomerEmail = cart.CustomerEmail;
    order.CustomerAddress = cart.CustomerAddress;
    //set dates:
    order.ShipDate = DateTime.MinValue;
    order.DeliveryDate = DateTime.MinValue;
    order.OrderDate= DateTime.Now;

    int orderId = 0;
   
    Dal?.Order.Add(order);
    DO.OrderItem o = new DO.OrderItem();
    //להמשיך -ליצור אובייקטים של הזמנה לפי הסל ולבקש אישור להוספת הפריטים





}