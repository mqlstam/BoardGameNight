using GraphQL.Types;
using BoardGameNight.Models;

public class SoortBordspelType : ObjectGraphType<SoortBordspel>
{
    public SoortBordspelType()
    {
        Name = "SoortBordspel";
        Field(x => x.Id, type: typeof(IdGraphType)).Description("Het ID van het soort bordspel.");
        Field(x => x.Naam).Description("De naam van het soort bordspel.");
        Field<ListGraphType<BordspelType>>(
            "bordspellen",
            resolve: context => context.Source.Bordspellen
        );
    }
}