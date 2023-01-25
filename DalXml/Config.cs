using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Dal;
public class ConfigOrder
{

    public static int getNumOrder()   
    {
        XElement root = XMLTools.LoadListFromXMLElement("config");
        int nextNum = Convert.ToInt32(root!.Element("orderNum")!.Value);
        root!.Element("orderNum")!.Value = (nextNum + 1).ToString();
        XMLTools.SaveListToXMLElement(root, "config");
        return nextNum + 1;
    }
} 
public class ConfigOrderItem
{
    public static int getNumOrder()
    {
        XElement root = XMLTools.LoadListFromXMLElement("config");
        int nextNum = Convert.ToInt32(root!.Element("orderItemNum")!.Value);
        root!.Element("orderItemNum")!.Value = (nextNum + 1).ToString();
        XMLTools.SaveListToXMLElement(root, "config");
        return nextNum + 1;
    }
}
