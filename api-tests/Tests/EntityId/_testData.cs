using api.Domain;

namespace api_tests.entitiyId;

public abstract class _testData
{
    public static IEnumerable<object[]> GetDisallowedIds()
    {
        foreach (string disallowedId in GuidEntityIdSpecification.DisallowedIds)
            yield return new [] { disallowedId };
    }

    public static IEnumerable<object[]> GetInvalidIds()
    {
        yield return new [] { "invalid" };
        yield return new [] { "00000000-0000-0000-0000-00000000000G" };
        yield return new [] { "ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ" };
        yield return new [] { "(12345678-1234-1234-1234-123456789abc)" };
        yield return new [] { "{12345678-1234-1234-1234-123456789abc}" };
        yield return new [] { "12345678123412341234123456789abc" };
    }
}
