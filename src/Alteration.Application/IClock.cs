namespace Alteration.Application
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}
