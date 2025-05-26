namespace Base.Domain;

public interface IDateTime
{
    DateTime UtcNow { get; }
    DateTime MinDate { get; }
    DateTime MaxDate { get; }
}