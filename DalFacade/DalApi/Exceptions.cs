
namespace DalApi;

public class ExceptionExists : Exception
{
    public override string Message => "Error: Item already exists.";
}
public class ExceptionNotExists : Exception
{
    public override string Message => "Error: Item does not exist.";
}
/// <summary>
/// Since there is a fixed size for the lists, we would like to throw an error if there is no more room to insert a new item.
/// </summary>
public class ExceptionNoRoom : Exception
{
    public override string Message => "Error: There is no free space to insert a new Item";
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
/// An error in case an exception is caught without a source, i.e. the EX is NULL.
/// </summary>
public class ExceptionNullEx : Exception
{
    public override string Message => "Sorry, an error popped up, we don't know why.";
}

/// <summary>
/// An error in case the item is null.
/// </summary>
public class ExceptionNull : Exception
{
    public override string Message => "Sorry, nullable error.";
}
