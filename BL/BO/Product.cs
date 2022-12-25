using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
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
    /// the amount of product in the stock
    /// </summary>
    public int? InStock { get; set; }

    /// <summary>
    /// function- print Product class
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        ID:{ID}
        Name:{Name}
    	Price:{Price}
    	Category:{Category}
        In Stock:{InStock}
";

}
