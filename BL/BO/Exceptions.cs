

namespace BO;

public class ExceptionFromDal : Exception
{
    public ExceptionFromDal(Exception ex) : base("Exception in Dal.", ex) { }
    public override string Message => "Exception in Dal.";
}
