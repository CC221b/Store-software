

namespace BO;

public class ExceptionFromDal : Exception
{
    public override string Message => "Error: Item already exists.";
}
