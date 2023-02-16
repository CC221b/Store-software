namespace Dal;
using DalApi;
using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

static class XMLTools
{
    const string s_dir = @"../xml/";
    static XMLTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);
    }
    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T?> list, string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer serializer = new(typeof(List<T?>));
            serializer.Serialize(file, list);
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception)
        { throw new Exception(); }
    }
    #endregion
}


internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem orderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        if (listOrderItems.Exists(oi => oi?.ID == orderItem.ID))
            throw new Exception();
        listOrderItems.Add(orderItem);
        XMLTools.SaveListToXMLSerializer(listOrderItems, "OrderItems");
        return orderItem.ID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        if (listOrderItems.RemoveAll(p => p?.ID == id) == 0)
            throw new Exception();
        XMLTools.SaveListToXMLSerializer(listOrderItems, "OrderItems");
    }

    public void Update(DO.OrderItem orderItem)
    {

    }
    public DO.OrderItem Get(int ID)
    {
        return new DO.OrderItem();
    }
    public DO.OrderItem Get(Predicate<DO.OrderItem> func)
    {
        return new DO.OrderItem();
    }
    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems")!;
        //var c = listOrderItems.Select(oi => new {});
        //return listOrderItems.OrderBy(oi => ((DO.OrderItem)oi!).ID);
        //listOrderItems.Where(func).
        //    OrderBy(oi => ((DO.OrderItem)oi!).ID);
        return new List<DO.OrderItem>();
    }
    public IEnumerable<DO.OrderItem> GetByOrderID(int id)
    {
        return new List<DO.OrderItem>();
    }
    public DO.OrderItem GetByProductIDAndOrderID(int productId, int orderId)
    {
        return new DO.OrderItem();
    }
}


