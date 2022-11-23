
namespace DO;

/// <summary>
/// Structure for the product entity. Contains the basic product fields
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
    ///  ID property receives and returns the products ID
    ///  Name property receives and returns the products Name
    ///  Category property receives and returns the products Category
    ///  Price property receives and returns the products price
    ///  InStock property receives and returns the products  
    /// 
    /// </summary>

    public int ID { get; set; }
    public string Name { get; set; } 
    public Category Category { get; set; }
    public double Price { get; set; }  
    public int InStock { get; set; }


    


}
