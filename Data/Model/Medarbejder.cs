using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eksamensprojekt.Data.Model;

public class Medarbejder
{
    private int medarbejderId;
    private string initial;
    private int cpr;
    private string navn;

    private List<Tidsregistrering> tidsregistrerings;
    public Medarbejder(){}

    public Medarbejder(int medarbejderId, string initial, int cpr, string navn)
    {
        this.MedarbejderId = medarbejderId;
        this.initial = initial;
        this.Cpr = cpr;
        this.navn = navn;
    }
    [Key]
    public int MedarbejderId { get; set; }

    public string Initial
    {
        get => initial;
        set => initial = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Cpr { get; set; } 
    
    public int AfdelingsId { get; set; }
    public Afdeling Afdeling { get; set; }
    public string Navn
    {
        get => navn;
        set => navn = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Tidsregistrering> Tidsregistrerings { get; set; } = new List<Tidsregistrering>();
}