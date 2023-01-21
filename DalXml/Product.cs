using System;
using System.Collections.Generic;
using System.Linq;
namespace Dal;
using DalApi;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "products"; //Linq to XML

    static DO.Product? getProduct(XElement p) =>
        p.ToIntNullable("ID") is null ? null : new DO.Product()
        {
            ID = (int)p.Element("ID")!,
            Name = (string?)p.Element("Name"),
            Price = (int?)p.Element("Price"),
            Category = (DO.Category)p.ToEnumNullable<DO.Category>("Category"),
            InStock = (int?)p.Element("InStock"),

        };//Linq 
    static IEnumerable<XElement> createProductsElement(DO.Product product)
    {
        yield return new XElement("ID", product.ID);
        if (product.Name is not null)
            yield return new XElement("Name", product.Name);
        if (product.Price is not null)
            yield return new XElement("Price", product.Price);
        if (product.Category != null)
            yield return new XElement("Category", product.Category);
        if (product.InStock is not null)
            yield return new XElement("InStock", product.InStock);
    }//Linq 
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null)
    {
        if (filter == null)
            return XMLTools.LoadListFromXMLElement(s_products).Elements().Select(x => getProduct(x));
        else
            return XMLTools.LoadListFromXMLElement(s_products).Elements().Select(x => getProduct(x)).Where(filter);




        //    XElement root = XMLTools.LoadListFromXMLElement(s_products);
        //List<DO.Product> returnlist = (from p in root.Elements()
        //                                select
        //                                new DO.Product()
        //                                {
        //                                    ID = (int)p.Element("ID")!,
        //                                    Name = (string?)p.Element("Name"),
        //                                    Price = (double?)p.Element("Price"),
        //                                    Category = p.ToEnumNullable<DO.Category>("Category"),
        //                                    InStock = (int?)p.Element("InStock")
        //                                }).ToList();    // לא מעביר את הפריטים לרשימההה

        //return (IEnumerable<Product?>)returnlist;   

    }

    //public DO.Product GetByID(int id) =>
    //        (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
    //        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
    //        // fix to: throw new DalMissingIdException(id);
    //        ?? throw new Exception("missing id"))!;


    //לבדוק למה המימוש של המוצר בלינק לא עובד
    public DO.Product GetById(int id)
    {
        var listProduct = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);

        if (listProduct.Exists(o => o?.ID == id))
            return (DO.Product)(listProduct.FirstOrDefault(o => o?.ID == id));
        else
            throw new Exception("missing id");//DalMissingIdException(id, "Lecturer");
    }
    public int Add(DO.Product p)
    {
        XElement ProductRootElem = XMLTools.LoadListFromXMLElement(s_products);

        if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(pro => pro.ToIntNullable("ID") == p.ID) is not null)
            // fix to: throw new DalMissingIdException(id);;
            throw new Exception("id already exist");

        ProductRootElem.Add(new XElement("Product", createProductsElement(p)));
        XMLTools.SaveListToXMLElement(ProductRootElem, s_products);

        return p.ID; ;
    }
    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        (productsRootElem.Elements()
            // fix to: throw new DalMissingIdException(id);
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new Exception("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }
    public void Update(DO.Product product)
    {
        Delete(product.ID);
        Add(product);
    }


    //public DO.Product GetById(int id)
    //{
    //    var listProduct = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);

    //    if (listProduct.Exists(p => p?.ID == id))
    //        return (DO.Product)(listProduct.FirstOrDefault(p => p?.ID == id));
    //    else
    //        throw new Exception("missing id");//DalMissingIdException(id, "Lecturer");

    //}


}
