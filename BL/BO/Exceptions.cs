namespace BO;

/// <summary>
/// Errors coming from the DAL by inheritance.
/// </summary>
public class ExceptionFromDal : Exception
{
    public ExceptionFromDal(Exception ex) : base("Exception in Dal.", ex) { }
    public override string Message => "Exception in Dal.";
}
/// <summary>
/// Invalid information errors.
/// </summary>
public class ExceptionInvalidData : Exception
{
    public override string Message => "Error: There is invalid data, please enter valid data.";
}
/// <summary>
/// Errors Item already exists.
/// </summary>
public class ExceptionExists : Exception
{
    public override string Message => "Error: Item already exists.";
}
/// <summary>
/// ID errors are incorrect (although there is an error of incorrect items,
/// I wanted something more specific and added this error)
/// </summary>
public class ExceptionInvalidID : Exception
{
    public override string Message => "Error: Please enter a valid ID number.";
}
/// <summary>
/// Errors Item does not exist.
/// </summary>
public class ExceptionNotExists : Exception
{
    public override string Message => "Error: Item does not exist.";
}
/// <summary>
/// Error out of stock.
/// </summary>
public class ExceptionOutOfStock : Exception
{
    public override string Message => "Error: The product is out of stock, sorry.";
}

/// <summary>
/// An order has been sent
/// </summary>
public class ExceptionOrderSent : Exception
{
    public override string Message => "Error: The order has already been sent and cannot be deleted at this time.";
}

/// <summary>
/// An error in case the item is null.
/// </summary>
public class ExceptionNull : Exception
{
    public override string Message => "Sorry, nullable error.";
}
