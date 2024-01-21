using GraphQL.Types;
using BoardGameNight.Models;

public class BordspellenavondType : ObjectGraphType<Bordspellenavond>
{
    public BordspellenavondType()
    {
        Name = "Bordspellenavond";
        Field(x => x.Id).Description("Het ID van de bordspellenavond.");
        Field(x => x.Adres).Description("Het adres van de bordspellenavond.");
        Field(x => x.MaxAantalSpelers).Description("Het maximale aantal spelers voor de bordspellenavond.");
        Field(x => x.DatumTijd).Description("De datum en tijd van de bordspellenavond.");
        Field(x => x.Is18Plus).Description("Geeft aan of de bordspellenavond 18+ is.");
        Field(x => x.Dieetwensen).Description("De dieetwensen voor de bordspellenavond.");
        Field(x => x.DrankVoorkeur).Description("De drankvoorkeur voor de bordspellenavond.");
        Field(x => x.IsPotluck).Description("Geeft aan of het een potluck bordspellenavond is.");
        Field<ListGraphType<BordspelType>>("bordspellen", resolve: context => context.Source.Bordspellen);
    }
}