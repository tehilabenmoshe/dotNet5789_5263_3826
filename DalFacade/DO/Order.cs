﻿
namespace DO;


public struct Order
{
    public override string ToString() => $@"
    Product ID={ID}: {Name}, 
    category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
    ";

    /// <summary>
    /// Unique ID of ...
    /// </summary>

    public int ID { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public int Price { get; set; }  
    public int InStock { get; set; }


}
