namespace Dal;
using DalApi;
using DO;
using System.Xml;
using System.Xml.Linq;

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
        return new DO.Product();
    }
    public DO.Product Get(Predicate<DO.Product> func)
    {
        return new DO.Product();
    }
    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        IEnumerable<DO.Product> p = new List<DO.Product>();
        return p;
    }
}
