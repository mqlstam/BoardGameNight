using GraphQL.Types;
using BoardGameNight.Models;

public class BordspelType : ObjectGraphType<Bordspel>
{
    public BordspelType()
    {
        Name = "Bordspel";
        Field(x => x.Id).Description("Het ID van het bordspel.");
        Field(x => x.Naam).Description("De naam van het bordspel.");
        Field(x => x.Beschrijving).Description("De beschrijving van het bordspel.");
        Field(x => x.GenreId).Description("De genre ID van het bordspel.");
        Field(x => x.SoortSpelId).Description("De soort spel ID van het bordspel.");
        Field(x => x.Is18Plus).Description("Geeft aan of het bordspel 18+ is.");
        Field(x => x.FotoUrl, nullable: true).Description("De URL van de foto van het bordspel.");
        Field<BordspelGenreType>(
            "genre",
            resolve: context => context.Source.Genre
        );
        Field<SoortBordspelType>(
            "soortSpel",
            resolve: context => context.Source.SoortSpel
        );
    }
}