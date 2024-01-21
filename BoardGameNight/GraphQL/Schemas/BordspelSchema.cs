using GraphQL.Types;

namespace BoardGameNight.GraphQL.Schemas;

public class BordspelSchema : Schema
{
    public BordspelSchema(IServiceProvider provider) : base(provider)
    {
        Query = provider.GetRequiredService<BordspelQuery>();
    }
}
