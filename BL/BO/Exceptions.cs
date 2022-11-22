

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

public class ExceptionExistsInOrder : Exception
{
    public override string Message => "Error: Item already exists in the order.";
}
