using System.ComponentModel.DataAnnotations;

namespace DTO.Model;

public class TidsregistreringDTO
{
    public TidsregistreringDTO() {}
    public TidsregistreringDTO(int tidsregistreringId, int medarbejderId, int? sagId, DateTime startTid, DateTime slutTid)
    {
        TidsregistreringId = tidsregistreringId;
        MedarbejderId = medarbejderId;
        SagId = sagId;
        StartTid = startTid;
        SlutTid = slutTid;
    }
    [Key]
    public int TidsregistreringId { get; set; }
    public DateTime StartTid { get; set; }
    public DateTime SlutTid { get; set; }
    public int MedarbejderId { get; set; }
    public int? SagId { get; set; }

}