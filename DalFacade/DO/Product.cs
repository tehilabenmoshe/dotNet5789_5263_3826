
namespace DO;

/// <summary>
/// Structure for ...
/// </summary>
public struct Product
{
    public override string ToString() => $@"
    Product ID:{ID} 
    Name: {Name}
    category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
    ";

    /// <summary>
    /// Unique ID of ...
    /// </summary>

    public int ID { get; set; } 
    public string Name { get; set; } 
    public Category Category { get; set; }
    public double Price { get; set; }  
    public int InStock { get; set; }


    


}
