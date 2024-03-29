namespace api.Domain;

public sealed class Chef : Entity
{
    public const int MODEL_VERSION = 0;

    public override int ModelVersion { get; init; } = MODEL_VERSION;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Chef(string name, EntityId chefId, string email)
    : base(chefId)
    {
        Name = name;
        Email = email;
    }
}
