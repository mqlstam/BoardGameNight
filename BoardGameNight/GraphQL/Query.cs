using BoardGameNight.Repositories.Implementations;
using GraphQL.Types;
using BoardGameNight.Repositories.Interfaces;
using GraphQL;

namespace BoardGameNight.GraphQL.Queries
{
    public class BordspelQuery : ObjectGraphType
    {
        public BordspelQuery(
            IBordspelRepository bordspelRepository,
            IBordspellenavondRepository bordspellenavondRepository,
            IBordspelGenreRepository bordspelGenreRepository,
            ISoortBordspelRepository soortBordspelRepository)
        {
            Name = "Query";

            FieldAsync<ListGraphType<BordspelType>>(
                "bordspellen",
                resolve: async context => await bordspelRepository.GetAllAsync());

            FieldAsync<BordspelType>(
                "bordspel",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context => await bordspelRepository.GetByIdAsync(context.GetArgument<int>("id")));

            FieldAsync<ListGraphType<BordspellenavondType>>(
                "bordspellenavonden",
                resolve: async context => await bordspellenavondRepository.GetAllAsync());

            FieldAsync<BordspellenavondType>(
                "bordspellenavond",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context => await bordspellenavondRepository.GetByIdAsync(context.GetArgument<int>("id")));

            FieldAsync<ListGraphType<BordspelGenreType>>(
                "bordspelGenres",
                resolve: async context => await bordspelGenreRepository.GetAllAsync());

            FieldAsync<BordspelGenreType>(
                "bordspelGenre",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context => await bordspelGenreRepository.GetByIdAsync(context.GetArgument<int>("id")));

            FieldAsync<ListGraphType<SoortBordspelType>>(
                "soortenBordspellen",
                resolve: async context => await soortBordspelRepository.GetAllAsync());

            FieldAsync<SoortBordspelType>(
                "soortBordspel",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: async context => await soortBordspelRepository.GetByIdAsync(context.GetArgument<int>("id")));

            // Add additional fields for other queries you may have
        }
    }
}