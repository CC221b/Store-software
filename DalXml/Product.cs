namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class Product : IProduct
{
    XElement? productsRoot;
    string productsPath = @"../xml/Products.xml";

    public Product()
    {
        if (!File.Exists(productsPath))
            CreateFiles();
        else
            LoadData();
    }
    private void CreateFiles()
    {
        productsRoot = new XElement("Products");
        productsRoot.Save(productsPath);
    }
    private void LoadData()
    {
        try
        {
            productsRoot = XElement.Load(productsPath);
        }
        catch
        {
            Console.WriteLine("File upload problem");
        }
    }

    public int Add(DO.Product product)
    {
        try
        {
            Get(product.ID);
        }
        catch (Exception)
        {
            XElement id = new XElement("ID", product.ID);
            XElement name = new XElement("Name", product.Name);
            XElement price = new XElement("Price", product.Price);
            XElement category = new XElement("Category", product.Category.ToString());
            XElement inStock = new XElement("InStock", product.InStock);
            productsRoot?.Add(new XElement("Product", id, name, price, category, inStock));
            productsRoot?.Save(productsPath);
            return product.ID;
        }
        throw new ExceptionExists();
    }

    public void Delete(int id)
    {
        XElement? productElement;
        try
        {
            productElement = (from p in productsRoot?.Elements()
                              where Convert.ToInt32(p.Element("ID")?.Value) == id
                              select p).FirstOrDefault();
            productElement?.Remove();
            productsRoot?.Save(productsPath);
        }
        catch
        {
            throw new Exception();
        }

    }

    public void Update(DO.Product product)
    {
        Delete(product.ID);
        Add(product);
    }

    public DO.Product Get(int ID)
    {
        LoadData();
        XElement? product = productsRoot?.Elements()
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
        LoadData();
        if (productsRoot?.Elements().Count() == 0)
        {
            throw new ExceptionEmpty();
        }
        IEnumerable<DO.Product> products = from product in productsRoot?.Elements()
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
