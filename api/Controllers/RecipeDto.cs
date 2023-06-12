namespace api.Controllers;

public sealed class RecipeDto : EntityDto
{
    public override int ModelVersion { get; set; } = Domain.Recipe.MODEL_VERSION;
    public required string Name { get; set; }
}
