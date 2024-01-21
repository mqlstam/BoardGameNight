using GraphQL.Types;
using BoardGameNight.Repositories.Interfaces;
using BoardGameNight.Models;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameNight.GraphQL.Mutations
{
    public class BordspelMutation : ObjectGraphType
    {
        public BordspelMutation(
            IBordspelRepository bordspelRepository,
            IBordspellenavondRepository bordspellenavondRepository)
        {
            Name = "Mutation";

            FieldAsync<BordspelType>(
                "createBordspel",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BordspelInputType>> { Name = "bordspel" }
                ),
                resolve: async context =>
                {
                    var bordspel = context.GetArgument<Bordspel>("bordspel");
                    await bordspelRepository.CreateAsync(bordspel);
                    return bordspel; // Assuming the bordspel object has been updated with an ID upon creation
                });

            FieldAsync<BordspelType>(
                "updateBordspel",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<BordspelInputType>> { Name = "bordspel" }
                ),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var bordspel = context.GetArgument<Bordspel>("bordspel");
                    bordspel.Id = id; // Assuming the bordspel object has an Id property to be set before updating
                    await bordspelRepository.UpdateAsync(bordspel);
                    return bordspel; // Assuming the bordspel object represents the updated state
                });

            FieldAsync<BordspellenavondType>(
                "createBordspellenavond",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BordspellenavondInputType>> { Name = "bordspellenavond" }
                ),
                resolve: async context =>
                {
                    var bordspellenavond = context.GetArgument<Bordspellenavond>("bordspellenavond");
                    await bordspellenavondRepository.CreateAsync(bordspellenavond, context.RequestServices.GetRequiredService<IHttpContextAccessor>().HttpContext.User.Identity.Name);
                    return bordspellenavond; // Assuming the bordspellenavond object has been updated with an ID upon creation
                });

            FieldAsync<BordspellenavondType>(
                "updateBordspellenavond",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<BordspellenavondInputType>> { Name = "bordspellenavond" }
                ),
                resolve: async context =>
                {
                    int id = context.GetArgument<int>("id");
                    var bordspellenavond = context.GetArgument<Bordspellenavond>("bordspellenavond");
                    bordspellenavond.Id = id; // Ensuring the bordspellenavond object has an Id property to be set before updating
                    await bordspellenavondRepository.UpdateAsync(bordspellenavond);
                    return bordspellenavond; // Assuming the bordspellenavond object represents the updated state
                });


            // Add additional fields for other mutations you may have
        }
    }

    public class BordspelInputType : InputObjectGraphType<Bordspel>
    {
        public BordspelInputType()
        {
            Name = "BordspelInput";
            Field(x => x.Naam);
            Field(x => x.Beschrijving);
            Field(x => x.GenreId);
            Field(x => x.SoortSpelId);
            Field(x => x.Is18Plus);
            Field(x => x.FotoUrl, nullable: true);
            // Add fields for other Bordspel properties that can be set on creation or update
        }
    }

    public class BordspellenavondInputType : InputObjectGraphType<Bordspellenavond>
    {
        public BordspellenavondInputType()
        {
            Name = "BordspellenavondInput";
            Field(x => x.Adres);
            Field(x => x.MaxAantalSpelers);
            Field(x => x.DatumTijd);
            Field(x => x.Is18Plus);
            Field(x => x.Dieetwensen, nullable: true);
            Field(x => x.DrankVoorkeur, nullable: true);
            Field(x => x.IsPotluck);
            // Add fields for other Bordspellenavond properties that can be set on creation or update
        }
    }
}