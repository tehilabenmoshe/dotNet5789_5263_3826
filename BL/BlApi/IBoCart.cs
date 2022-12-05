using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
public interface IBoCart
{
    public Cart AddProductToCart(Cart cart, int ID);
    public Cart UpdateProductInCart(Cart cart, int ID, int amount);
    public void MakeCart(Cart cart);
}
