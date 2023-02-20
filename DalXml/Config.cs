using System.Xml.Linq;

namespace Dal;

internal class Config
{
	public int LoadFromConfigXML(string type)
	{
        var elements = XDocument.Load(@"../xml/Config.xml")?.Root;
        var res = elements?.Element(type);
        int ID = Convert.ToInt32(res?.Value) + 1;
        if (elements != null)
        {
            res?.Remove();
            XElement xElement = new XElement(type, ID);
            elements.Add(xElement);
            elements.Save(@"../xml/Config.xml");
        }
        return ID;
    }

    private int orderID;

	public int OrderID
    {
		get { return LoadFromConfigXML("OrderID"); }
		set { orderID = value; }
	}

    private int orderItemID;

    public int OrderItemID
    {
        get { return LoadFromConfigXML("OrderItemID"); }
        set { orderItemID = value; }
    }

}
