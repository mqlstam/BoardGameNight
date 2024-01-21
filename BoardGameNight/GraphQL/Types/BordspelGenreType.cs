using GraphQL.Types;
using BoardGameNight.Models;

public class BordspelGenreType : ObjectGraphType<BordspelGenre>
{
    public BordspelGenreType()
    {
        Name = "BordspelGenre";
        Field(x => x.Id, type: typeof(IdGraphType)).Description("Het ID van het genre.");
        Field(x => x.Naam).Description("De naam van het genre.");
        Field<ListGraphType<BordspelType>>(
            "bordspellen",
            resolve: context => context.Source.Bordspellen
        );
    }
}