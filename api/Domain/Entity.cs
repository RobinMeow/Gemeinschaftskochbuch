namespace api.Domain;

public abstract class Entity
{
    public int ModelVersion { get; set; } = 0; // start at zero, so the version is also the amount of times, it was changed :)
}
