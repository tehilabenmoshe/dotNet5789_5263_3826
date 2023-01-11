using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
//using DO;
//using BO;


namespace BlImplementation;


internal class BoCart : IBoCart
{
    private IDal? Dal = DalApi.Factory.Get();
   
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

        }
        else
        {
            if (tmp.InStock > 0)
            {
                BO.OrderItem? o = cart.Items.FirstOrDefault(o => o.ProductID == id);
                o!.Amount++; //update the amount
                o.TotalPrice += o.Price;
                cart.TotalPrice += o.Price; //update the total price of the cart
             
            }
            else
                throw new BO.DoesntExistException();
        }
        return cart; //return the cart
    }

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
                        cart.TotalPrice = (cart.TotalPrice ?? 0) + (item.Price * difference);
                        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
                        return cart;
                    }
                    if (item.Amount > newAmount)
                    {
                        item.Amount = newAmount;
                        cart.TotalPrice = (cart.TotalPrice ?? 0) + (item.Price * difference);
                        cart.TotalPrice = Math.Round(cart.TotalPrice ?? 0, 2);
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



    }

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

        int orderId = Dal!.Order.Add(order);

        foreach (BO.OrderItem? oInBo in cart.Items)
        {
            DO.Product productInDo = Dal.Product.GetById(oInBo.ProductID); //reciving the product in do
            DO.OrderItem o = new DO.OrderItem(); //creating new orderItem in do
            
            o.ID = oInBo.ID;
            o.ProductID = oInBo.ProductID;
            o.Price = oInBo.Price;
            o.Amount = oInBo.Amount;
            o.OrderID = orderId; //מספר ההזמנה הנ"ל
            Dal?.OrderItem.Add(o);
            productInDo.InStock -= oInBo.Amount; //update the amount
            Dal.Product.Update(productInDo);

        }

        
        //להמשיך -להוריד פריטים מהסל
        //לדחוף לdo לרשימה

    }

}