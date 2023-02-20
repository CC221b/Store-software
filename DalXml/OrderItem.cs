namespace Dal;
using DalApi;
using DO;
using System;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem orderItem)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        if (listOrderItems.Exists(oi => oi.ID == orderItem.ID))
            throw new ExceptionExists();
        var element = XDocument.Load(@"../xml/Config.xml")?.Root;
        var res = element?.Element("OrderItemID");
        orderItem.ID = Convert.ToInt32(res?.Value) + 1;
        if (element != null)
        {
            res?.Remove();
            XElement orderItemID = new XElement("OrderItemID", orderItem.ID);
            element.Add(orderItemID);
            element.Save(@"../xml/Config.xml");
        }
        listOrderItems.Add(orderItem);
        XMLTools.SaveListToXMLSerializer(listOrderItems, "OrderItems");
        return orderItem.ID;
    }

    public void Delete(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        if (listOrderItems.RemoveAll(p => p.ID == id) == 0)
            throw new ExceptionNotExists();
        XMLTools.SaveListToXMLSerializer(listOrderItems, "OrderItems");
    }

    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.ID);
        Add(orderItem);
    }

    public DO.OrderItem Get(int ID)
    {
        DO.OrderItem orderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems")
            .FirstOrDefault(p => p.ID == ID);
        if (orderItem.ID != 0)
        {
            return orderItem;
        }
        throw new ExceptionNotExists();
    }

    public DO.OrderItem Get(Predicate<DO.OrderItem> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems").Find(func);
    }

    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        return func == null ? listOrderItems.OrderBy(oi => ((DO.OrderItem)oi).ID) :
        listOrderItems.Where(func).
            OrderBy(oi => ((DO.OrderItem)oi).ID);
    }

    public IEnumerable<DO.OrderItem> GetByOrderID(int id)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        return listOrderItems.Where(oi => oi.OrderId == id);
    }

    public DO.OrderItem GetByProductIDAndOrderID(int productId, int orderId)
    {
        var listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>("OrderItems");
        return listOrderItems.Where(oi => (oi.ProductId == productId && oi.OrderId == orderId)).FirstOrDefault();
    }
}


