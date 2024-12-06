using System.ComponentModel.DataAnnotations;

namespace Eksamensprojekt.Data.Model;

public class Tidsregistrering
{
    private int tidsregistreringId;
    private int medarbejderId;
    private int? sagId;
    private DateTime startTid;
    private DateTime slutTid;
    
    private Medarbejder medarbejder;
    private Sag sag;

    public Tidsregistrering(){}
    public Tidsregistrering(int tidsregistreringId, int medarbejderId, int? sagId, DateTime startTid, DateTime slutTid)
    {
        this.tidsregistreringId = tidsregistreringId;
        this.medarbejderId = medarbejderId;
        this.sagId = sagId;
        this.startTid = startTid;
        this.slutTid = slutTid;
    }
    public int MedarbejderId
    {
        get => medarbejderId;
        set => medarbejderId = value;
    }

    public int? SagId
    {
        get => sagId;
        set => sagId = value;
    }
    [Key]
    public int TidsregistreringId
    {
        get => tidsregistreringId;
        set => tidsregistreringId = value;
    }
    
    public DateTime StartTid
    {
        get => startTid;
        set => startTid = value;
    }
    
    public DateTime SlutTid
    {
        get => slutTid;
        set => slutTid = value;
    }
}