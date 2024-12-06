using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Model;

public class MedarbejderDTO
{
    public MedarbejderDTO() {}
    public MedarbejderDTO(string initial, int cpr, string navn)
    {
        Initial = initial;
        Cpr = cpr;
        Navn = navn;
    }
    [Key]
    public int MedarbejderId { get; set; }
    public string Initial { get; set; }
    public int Cpr { get; set; }
    public string Navn { get; set; }
    public List<TidsregistreringDTO> Tidsregistrerings { get; set; }
    public int AfdelingsId { get; set; }
   
}