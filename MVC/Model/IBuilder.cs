namespace Shared.Interfaces
{
    public interface IBuilder<out T, TParam>
    {
        TParam Params { get; set; }
        T Build();
    }
}