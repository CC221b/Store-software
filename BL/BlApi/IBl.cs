namespace BlApi;
/// <summary>
/// The purpose of the interfaces of the logical layer is to concentrate and declare the operations that the layer exposes. 
/// In the layer we will add a main interface that will centralize the access to the specific interfaces,
/// which will include the methods related to a certain main logical entity.
/// </summary>
public interface IBl
{
    public IProduct Product { get; }
    public ICart Cart { get; }
    public IOrder Order { get; }
}
