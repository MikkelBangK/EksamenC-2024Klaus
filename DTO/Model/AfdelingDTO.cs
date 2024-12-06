using System.ComponentModel.DataAnnotations;

namespace DTO.Model;

public class AfdelingDTO
{
    public AfdelingDTO() {}
    public AfdelingDTO(int afdelingsId, string navn, int nummer)
    {
        AfdelingsId = afdelingsId;
        Navn = navn;
        Nummer = nummer;
    }
    [Key]
    public int AfdelingsId { get; set; }
    public string Navn { get; set; }
    public int Nummer { get; set; }
    private List<MedarbejderDTO> MedarbejdersDto { get; set; }
    private List<SagDTO> SagsDto { get; set; }
}