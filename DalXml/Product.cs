﻿namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class Product : IProduct
{
    public int Add(DO.Product product)
    {
        XElement xElement = XElement.Load(@"../xml/Products.xml");
        xElement.Add(product);
        return product.ID;
    }

    public void Delete(int id)
    {

    }

    public void Update(DO.Product product)
    {

    }

    public DO.Product Get(int ID)
    {
        XElement? product = XElement.Load(@"../xml/Products.xml").Descendants("Product")
            .Where(product => Convert.ToInt32(product?.Element("ID")?.Value) == ID).FirstOrDefault();
        if (product != null)
        {
            return new DO.Product()
            {
                ID = Convert.ToInt32(product?.Element("ID")?.Value),
                Name = product?.Element("Name")?.Value,
                Price = Convert.ToInt32(product?.Element("Price")?.Value),
                Category = DO.Categories.TryParse(product?.Element("Category")?.Value, out DO.Categories category) ? (DO.Categories)category : 0,
                InStock = Convert.ToInt32(product?.Element("InStock")?.Value)
            };
        }
        throw new ExceptionNotExists();
    }

    public DO.Product Get(Predicate<DO.Product> func)
    {
        DO.Product product = GetAll().ToList().Find(func);
        return product.ID != 0 ? product : throw new ExceptionNotExists();
    }

    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        if (XElement.Load(@"../xml/Products.xml").Descendants("Product").Count() == 0)
        {
            throw new ExceptionEmpty();
        }
        IEnumerable<DO.Product> products = from product in XElement.Load(@"../xml/Products.xml").Descendants("Product")
                                           let category = DO.Categories.TryParse(product?.Element("Category")?.Value, out DO.Categories category) ? (DO.Categories)category : 0
                                           select new DO.Product()
                                           {
                                               ID = Convert.ToInt32(product?.Element("ID")?.Value),
                                               Name = product?.Element("Name")?.Value,
                                               Price = Convert.ToInt32(product?.Element("Price")?.Value),
                                               Category = category,
                                               InStock = Convert.ToInt32(product?.Element("InStock")?.Value)
                                           };
        return (func == null) ? products : products.Where(func);
    }
}
