namespace BO;

/// <summary>
/// Errors coming from the DAL by inheritance.
/// </summary>
public class ExceptionFromDal : Exception
{
    public ExceptionFromDal(Exception ex) : base("Exception in Dal.", ex) { }
    public override string Message => "An error was caught in the data layer The error is:";
}
/// <summary>
/// Invalid information errors.
/// </summary>
public class ExceptionInvalidData : Exception
{
    public override string Message => "Sorry, incorrect information was entered, please enter correct information. Thanks!";
}
/// <summary>
/// Errors Item already exists.
/// </summary>
public class ExceptionExists : Exception
{
    public override string Message => "Sorry, this item already exists in the system, Please enter a different item ID. Thanks!";
}
/// <summary>
/// ID errors are incorrect (although there is an error of incorrect items,
/// I wanted something more specific and added this error)
/// </summary>
public class ExceptionInvalidID : Exception
{
    public override string Message => "Sorry, an invalid ID number was entered, please enter a valid ID number. Thanks!";
}
/// <summary>
/// Errors Item does not exist.
/// </summary>
public class ExceptionNotExists : Exception
{
    public override string Message => "Sorry, this item does not exist. Please enter a valid item ID. Thanks!";
}
/// <summary>
/// Error out of stock.
/// </summary>
public class ExceptionOutOfStock : Exception
{
    public override string Message => "Sorry, the product is out of stock.";
}

/// <summary>
/// An order has been sent
/// </summary>
public class ExceptionOrderSent : Exception
{
    public override string Message => "Sorry, the order has already been sent, it is not possible to delete it.";
}

/// <summary>
/// An error in case the item is null.
/// </summary>
public class ExceptionNull : Exception
{
    public override string Message => "Sorry, nullable error.";
}
