namespace api.Controllers;

public abstract class EntityDto {
    public string Id { get; set; } = default!;
    public abstract int ModelVersion { get; set; }
}
