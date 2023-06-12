namespace api.Controllers;

public abstract class EntityDto {
    public EntityIdDto Id { get; set; } = default!;
    public abstract int ModelVersion { get; set; }
}
