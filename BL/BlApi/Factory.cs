using BlImplementation;

namespace BlApi;

public static class Factory
{
    public static IBl Get()
    {
        IBl bl = new Bl();
        return bl;
    }
}
