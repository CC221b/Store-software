
namespace DalApi;

public class ExceptionExists : Exception
{
    public override string Message => "Sorry, this item already exists in the system, Please enter a different item ID. Thanks!";
}
public class ExceptionNotExists : Exception
{
    public override string Message => "Sorry, this item does not exist. Please enter a valid item ID. Thanks!";
}
/// <summary>
/// Since there is a fixed size for the lists, we would like to throw an error if there is no more room to insert a new item.
/// </summary>
public class ExceptionNoRoom : Exception
{
    public override string Message => "Sorry, there is no more space available to insert a new item, please contact the relevant authorities. Thanks!";
}
/// <summary>
/// Since it is possible that all the data from the lists will be deleted even though they have been initialized,
/// we would like to print a message about this.
/// </summary>
public class ExceptionEmpty : Exception
{
    public override string Message => "Sorry, there is no data to display.";
}
/// <summary>
/// An error in case the item is null.
/// </summary>
public class ExceptionNull : Exception
{
    public override string Message => "Sorry, nullable error.";
}
