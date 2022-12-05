using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductItem
{
    /// <summary>
    /// product id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// product category
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// amount of this product int the customer cart
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// if the product exist in stock
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// function- print ProductItem class
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        ID={ID}
        Name={Name}
    	Price: {Price}
        Category:{Category}
    	Amount:{Amount}
        In stock:{InStock}
";

}
