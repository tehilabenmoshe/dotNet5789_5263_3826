using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
public interface IBoCart
{
    public BO.Cart AddProductToCart(BO.Cart cart, int ID);
    public BO.Cart UpdateProductInCart(BO.Cart cart, int id, int newAmount);
    public void MakeCart(BO.Cart cart);
}
