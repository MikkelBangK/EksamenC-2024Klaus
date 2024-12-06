using System.ComponentModel.DataAnnotations;

namespace Eksamensprojekt.Data.Model;

public class Afdeling
{
    private int afdelingsId;
    private string navn;
    private int nummer;

    private List<Medarbejder> medarbejders;
    private List<Sag> sags;
    
    public Afdeling(){}
    public Afdeling(int afdelingsId, string navn, int nummer)
    {
        this.afdelingsId = afdelingsId;
        this.navn = navn;
        this.nummer = nummer;
    }
    [Key]
    public int AfdelingsId
    {
        get => afdelingsId;
        set => afdelingsId = value;
    }
    public string Navn
    {
        get => navn;
        set => navn = value ?? throw new ArgumentNullException(nameof(value));
    }
    public int Nummer
    {
        get => nummer;
        set => nummer = value;
    }

    public List<Medarbejder> Medarbejders
    {
        get => medarbejders;
        set => medarbejders = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Sag> Sags
    {
        get => sags;
        set => sags = value ?? throw new ArgumentNullException(nameof(value));
    }
}