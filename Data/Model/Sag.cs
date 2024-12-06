using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Eksamensprojekt.Data.Model;

public class Sag
{
    private string overskrift;
    private string beskrivelse;
    private int sagsNummer;

    private List<Tidsregistrering> tidsregistrerings;
    
    public Sag(){}
    public Sag(int sagsNummer, string overskrift, string beskrivelse)
    {
        this.SagsNummer = sagsNummer;
        this.overskrift = overskrift;
        this.beskrivelse = beskrivelse;
    }
    [Key]
    public int SagsNummer
    {
        get => sagsNummer;
        set => sagsNummer = value;
    }

    public string Overskrift
    {
        get => overskrift;
        set => overskrift = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Beskrivelse
    {
        get => beskrivelse;
        set => beskrivelse = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int AfdelingsId { get; set; }

    public List<Tidsregistrering> Tidsregistrerings
    {
        get => tidsregistrerings;
        set => tidsregistrerings = value ?? throw new ArgumentNullException(nameof(value));
    }
}