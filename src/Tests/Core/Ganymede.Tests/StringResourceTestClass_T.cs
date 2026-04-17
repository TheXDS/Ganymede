namespace TheXDS.Ganymede;

public abstract class StringResourceTestClass<T> : StringResourceTestClass where T : notnull
{
    protected StringResourceTestClass() : base(typeof(T))
    {
    }
}
