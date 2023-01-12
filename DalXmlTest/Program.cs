using System.Data.SqlTypes;

internal class Class1
{
    public static void Main(string[] args)
    {
        DO.Product p = new();
        DalXml d = new DalXml();
        d.Product.Add(p);
    }
}
