using System.ComponentModel.DataAnnotations;

namespace DTO.Model;

public class SagDTO
{
 public SagDTO() {}
 public SagDTO(int sagsNummer, string overskrift, string beskrivelse)
 {
  SagsNummer = sagsNummer;
  Overskrift = overskrift;
  Beskrivelse = beskrivelse;
 }
 [Key]
 public int SagsNummer { get; set; }
 public string Overskrift { get; set; }
 public string Beskrivelse { get; set; }
 
 public int AfdelingsId { get; set; }
 
 private List<TidsregistreringDTO> Tidsregistrerings { get; set; }
}