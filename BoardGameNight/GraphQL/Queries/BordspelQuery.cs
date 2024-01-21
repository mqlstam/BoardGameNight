using BoardGameNight.Repositories.Interfaces;
using GraphQL;
using GraphQL.Types;


public class BordspelQuery : ObjectGraphType
{
    public BordspelQuery(IBordspelRepository bordspelRepository, IBordspellenavondRepository bordspellenavondRepository)
    {
        FieldAsync<ListGraphType<BordspelType>>(
            "bordspellen",
            resolve: async context => await bordspelRepository.GetAllAsync());

        FieldAsync<BordspelType>(
            "bordspel",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: async context => await bordspelRepository.GetByIdAsync(context.GetArgument<int>("id")));

        FieldAsync<ListGraphType<BordspellenavondType>>(
            "bordspellenavonden",
            resolve: async context => await bordspellenavondRepository.GetAllAsync());

        FieldAsync<BordspellenavondType>(
            "bordspellenavond",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: async context => await bordspellenavondRepository.GetByIdAsync(context.GetArgument<int>("id")));

        // Voeg hier eventueel meer queries toe indien nodig.
    }
}
