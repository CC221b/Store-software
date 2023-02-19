﻿namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Order : IOrder
{
    public int Add(DO.Order order)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders");
        if (listOrders.Exists(oi => oi.ID == order.ID))
            throw new ExceptionExists();
        XElement? element = XElement.Load(@"../xml/ConfigOrderID.xml")?.Element("OrderID");
        order.ID = Convert.ToInt32(element?.Value) + 1;
        if (element != null)
        {
            element.Value = order.ID.ToString();
            element.Save(@"../xml/ConfigOrderID.xml");
        }
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, "Orders");
        return order.ID;
    }

    public void Delete(int id)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders");
        if (listOrders.RemoveAll(p => p.ID == id) == 0)
            throw new ExceptionNotExists();
        XMLTools.SaveListToXMLSerializer(listOrders, "Orders");
    }

    public void Update(DO.Order order)
    {
        Delete(order.ID);
        Add(order);
    }

    public DO.Order Get(int ID)
    {
        DO.Order order = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders")
            .FirstOrDefault(p => p.ID == ID);
        if (order.ID != 0)
        {
            return order;
        }
        throw new ExceptionNotExists();
    }

    public DO.Order Get(Predicate<DO.Order> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders").Find(func);
    }

    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>("Orders");
        return func == null ? listOrders.OrderBy(oi => ((DO.Order)oi).ID) :
        listOrders.Where(func).
            OrderBy(oi => ((DO.Order)oi).ID);
    }
}

