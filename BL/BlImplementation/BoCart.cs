using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using DO;

//using DO;
//using BO;


namespace BlImplementation;

/// <summary>
/// The BoCart class is responsible for implementing the business logic for adding and updating products in a cart.
/// </summary>
internal class BoCart : IBoCart
{
    private IDal? Dal = DalApi.Factory.Get();

    public readonly Random rand = new Random();

    /// <summary>
    /// The AddProductToCart method:
//    takes in a BO.Cart object and an integer id as input and returns a BO.Cart object.
//first checks if the input cart object is null, and if so, creates a new cart object.
//then checks if the input id is valid(between 100000 and 999999) and retrieves the product with the corresponding id from the Data Access Layer(DAL) using the DalApi.Factory.Get() method.
//If the product is not already in the cart, a new BO.OrderItem is created and added to the cart's Items list, and the UpdateProductInCart method is called to update the cart's total price.
//If the product is already in the cart, the method increases the quantity of the product and updates the cart's total price.
//If the product is out of stock, the method throws a BO.DoesntExistException exception.
     /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    /// <exception cref="BO.DoesntExistException"></exception>
    public BO.Cart AddProductToCart(BO.Cart? cart, int id)
    {
        if (cart == null) //if cart-null
            cart = new BO.Cart(); //create new cart

        if ((id < 100000) || (id > 999999)) //if the id is wrong
            throw new BO.InvalidInputExeption();

        DO.Product tmp = new DO.Product();
        tmp = Dal!.Product.GetById(id);//get the product from DO

        BO.OrderItem orderToAdd = new BO.OrderItem(); //createan orderitem in order to add him to the cart

        if (!(cart.Items!.Exists(crt => crt.ProductID == id))) //if the cart dosent exsist in DO
        {
            //if(!(cart.Items!.Exists(crt =>crt.ProductID==P))
            orderToAdd.ProductID = tmp.ID;
            orderToAdd.Price = (double)tmp.Price;
            orderToAdd.Amount = 1;
            orderToAdd.TotalPrice = orderToAdd.Price * orderToAdd.Amount;
            orderToAdd.Name=tmp.Name;
            cart.Items.Add(orderToAdd);//add the orderitem to the cart


            UpdateProductInCart(cart, id, orderToAdd.Amount);
        }


        else
        {
            if (tmp.InStock > 0)
            {
                BO.OrderItem? o = cart.Items.FirstOrDefault(o => o.ProductID == id);
                o!.Amount++; //update the amount
                o.TotalPrice += o.Price;
                cart.TotalPrice += o.Price; //update the total price of the cart

                UpdateProductInCart(cart, id, o.Amount);

            }
            else
                throw new BO.DoesntExistException("Product out of stock");
        }
        

        return cart; //return the cart
    }


    /// <summary>
    /// The UpdateProductInCart method:

//    takes in a BO.Cart object, an integer id, and an integer newAmount as input and returns a BO.Cart object.
//first retrieves the product from the DAL using the DalApi.Factory.Get() method and checks if the product is already in the cart.
//If the new amount is 0, the product is removed from the cart.
//If the new amount is greater than the current amount, the method checks if there is enough stock and increases the quantity of the product in the cart.
//If the new amount is less than the current amount, the method decreases the quantity of the product in the cart.
//The method updates the cart's total price accordingly. 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public BO.Cart UpdateProductInCart(BO.Cart cart, int id, int newAmount)
    {
        bool productInCart = false;
        try
        {
            DO.Product? p = Dal.Product.GetById(id);
            if (cart?.Items == null)
                throw new BO.DoesntExistException("cart is empty");

            foreach (BO.OrderItem? item in cart.Items)
            {
                if (item != null && item.ProductID == id)
                {
                    productInCart = true;
                    if (newAmount == 0)
                    {
                        cart.Items.Remove(item);
                        cart.TotalPrice = cart.TotalPrice ?? 0 - (item.Price * item.Amount);
                        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        return cart;
                    }
                    int? difference = newAmount - item.Amount;
                    if (item.Amount < newAmount)
                    {
                        if (!(p?.InStock >= difference))
                            throw new BO.DoesntExistException("cant add - p out of stock");
                        item.Amount = newAmount;
                       // cart.TotalPrice = (cart.TotalPrice ?? 0) + (item.Price * difference);
                       // cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        
                        return cart;
                    }
                    if (item.Amount > newAmount)
                    {

                        item.Amount = newAmount;
                        item.TotalPrice= newAmount * item.Price;
                        //  cart.TotalPrice = (cart.TotalPrice ?? 0) + (item.Price * difference);
                        //cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        // cart.TotalPrice = newAmount * item.Price;
                        cart.TotalPrice += difference*item.Price;
                        return cart;
                    }
                }
                
            }

            if(productInCart==false) //if the product is not in the cart
                throw new BO.DoesntExistException("product is not in cart");

            return cart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    /// <summary>
    /// The MakeCart method is responsible for creating an order from an input BO.Cart object and adding it to the Data Access Layer (DAL). The method performs several checks on the input cart object to ensure that it has valid customer information (name, address, and email) before proceeding.

//    The method first creates a new DO.Order object and copies the customer information from the input BO.Cart object to the new order object. It also sets the order's ship and delivery dates to the minimum value and sets the order date to the current date.

//It then uses the DalApi.Factory.Get() method to retrieve the orderId from the DAL.

//The method then iterates through the BO.Cart object's Items list and for each item, it retrieves the corresponding DO.Product object from the DAL using the DalApi.Factory.Get() method.

//Then it creates a new DO.OrderItem object, copies the relevant information from the BO.OrderItem object, and sets the orderId to the orderId retrieved earlier.

//Finally, it adds the new DO.OrderItem object to the DAL using the DalApi.OrderItem.Add() method and updates the product's stock in the DAL using the DalApi.Product.Update() method.
//    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    public void MakeCart(BO.Cart? cart)
    {
        //check input:
        if (cart == null) throw new BO.InvalidInputExeption();
        if (cart.CustomerName == "") throw new BO.InvalidInputExeption();
        if (cart.CustomerAddress == "") throw new BO.InvalidInputExeption();
        if (cart.CustomerEmail == "" || !(cart.CustomerEmail!.Contains("@")))
            throw new BO.InvalidInputExeption();

        //copy data:
        DO.Order order = new DO.Order();
        order.CustomerName = cart.CustomerName;
        order.CustomerEmail = cart.CustomerEmail;
        order.CustomerAddress = cart.CustomerAddress;
        //set dates:
        order.ShipDate = DateTime.MinValue;
        order.DeliveryDate = DateTime.MinValue;
        order.OrderDate = DateTime.Now;

        //order.ID = rand.Next(100) * 10;

        int orderId =Dal!.Order.Add(order);

        foreach (BO.OrderItem? oInBo in cart.Items)
        {
            DO.Product productInDo = Dal.Product.GetById(oInBo.ProductID); //reciving the product in do
            DO.OrderItem o = new DO.OrderItem(); //creating new orderItem in do
            
            o.ID = oInBo.ID;
            o.ProductID = oInBo.ProductID;
            o.Price = oInBo.Price;
            o.Amount = oInBo.Amount;
            o.OrderID = orderId; //מספר ההזמנה הנ"ל
            o.ID=(int) (Dal?.OrderItem.Add(o));
            productInDo.InStock -= oInBo.Amount; //update the amount
            Dal.Product.Update(productInDo);

        }

    }

}








//BO.OrderItem? tmp = new BO.OrderItem();
//tmp = cart.Items.Find(o => o.ProductID == id); //find the item in the cart
////if (tmp == null) //if the product dosent exsist in the cart
////{
////   // BO.OrderItem? tmp2 = new BO.OrderItem();
////    AddProductToCart(cart, id); //insert the product to the cart
////    UpdateProductInCart(cart,id, newAmount); //update the product
////   // cart.TotalPrice +=

////}
//    // throw new BO.DoesntExistException();

//if (cart == null)
//    throw new BO.DoesntExistException();

//if (newAmount > tmp.Amount)
//{
//    DO.Product p = new DO.Product();
//    if (p.InStock > 0) //if there is product
//    {
//        cart.TotalPrice += tmp.Price * (newAmount - tmp.Amount); //update the total price of the cart-add the extra price from the new amount
//        tmp.Amount = newAmount;
//    }
//    else
//        throw new BO.DoesntExistException();
//}

//if (newAmount < tmp.Amount)
//{
//    cart.TotalPrice -= tmp.Price * (tmp.Amount - newAmount); //update the total price of the cart-add the extra price from the new amount
//    tmp.Amount = newAmount;
//}

//if (newAmount == 0)
//{
//    cart.TotalPrice -= tmp.Price * tmp.Amount;
//    Dal!.Product.Delete(id);

//}
//return cart;
