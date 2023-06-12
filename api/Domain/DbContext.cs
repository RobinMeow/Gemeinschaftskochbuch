namespace api.Domain;

public abstract class DbContext
{
    public abstract IRezeptRepository RezeptRepository { get; init; }
}