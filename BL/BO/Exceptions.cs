

namespace BO;

public class ExceptionFromDal : Exception
{
    public ExceptionFromDal(Exception ex) : base("Exception in Dal.", ex) { }
    public override string Message => "Exception in Dal.";
}

public class ExceptionInvalidData : Exception
{
    public override string Message => "Error: There is invalid data, please enter valid data.";
}

public class ExceptionExists : Exception
{
    public override string Message => "Error: Item already exists.";
}

public class ExceptionInvalidID : Exception
{
    public override string Message => "Error: Please enter a valid ID number.";
}

public class ExceptionNotExists : Exception
{
    public override string Message => "Error: Item does not exist.";
}

public class ExceptionOutOfStock : Exception
{
    public override string Message => "Error: The product is out of stock, sorry.";
}
