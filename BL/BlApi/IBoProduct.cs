using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
public interface IBoProduct
{
    public IEnumerable<ProductForList> getProductForList();
    public Product GetProductbyId(int ID);
    public ProductItem GetProductByIDAndCart(int ID, Cart cart);
    public void AddProduct(Product product);
    public void DeledeProduct(int ID);
    public void UpdateDetailProduct(Product product);
}
