using DTO.Model;
using Eksamensprojekt.Data.Model;

namespace Eksamensprojekt.Mappers;

public class Mapper
{
    
    //Afdeling mappers
    public static AfdelingDTO MapTilAfdelingDTO(Afdeling afdeling)
    {
        return new AfdelingDTO(
            afdeling.AfdelingsId,
            afdeling.Navn,
            afdeling.Nummer
        );
    }
    public static Afdeling MapTilAfdelingEntity(AfdelingDTO afdelingDTO)
    {
        return new Afdeling(
            afdelingDTO.AfdelingsId,
            afdelingDTO.Navn,
            afdelingDTO.Nummer
            );
    }
    
    //Medarbejder mappers
    public static MedarbejderDTO MapTilMedarbejderDTO(Medarbejder medarbejder)
    {
        MedarbejderDTO medarbejderDto = new MedarbejderDTO(
            medarbejder.Initial, 
            medarbejder.Cpr, 
            medarbejder.Navn
            
            );
        medarbejderDto.MedarbejderId = medarbejder.MedarbejderId;
        medarbejderDto.Tidsregistrerings = MapTilTidsregistreringDtos(medarbejder.Tidsregistrerings);
        return medarbejderDto;
    }
    public static Medarbejder MapTilMedarbejderEntity(MedarbejderDTO medarbejderDTO, AfdelingDTO afdelingDTO)
    {
        var afdelingEntity = new Afdeling
        {
            AfdelingsId = afdelingDTO.AfdelingsId,
            Navn = afdelingDTO.Navn,
            Nummer = afdelingDTO.Nummer
        };
        var medarbejderEntity = new Medarbejder(
            medarbejderDTO.MedarbejderId,
            medarbejderDTO.Initial,
            medarbejderDTO.Cpr,
            medarbejderDTO.Navn
        )
        {
            Afdeling = afdelingEntity,
            AfdelingsId = afdelingEntity.AfdelingsId
        };
        return medarbejderEntity;
    }
    
    //Sag mappers
    public static SagDTO MapTilSagDTO(Sag sag)
    {
        return new SagDTO(
            sag.SagsNummer,
            sag.Overskrift, 
            sag.Beskrivelse
            );
    }
    public static Sag MapTilSagEntity(SagDTO sagDTO)
    {
        return new Sag(
            sagDTO.SagsNummer,
            sagDTO.Beskrivelse, 
            sagDTO.Overskrift
            );
    }
    
    //Tidsregistrering mappers
    public static TidsregistreringDTO MapTilTidsregistreringDTO(Tidsregistrering tidsregistrering)
    {
        return new TidsregistreringDTO(
            tidsregistrering.TidsregistreringId,
            tidsregistrering.MedarbejderId,
            tidsregistrering.SagId,
            tidsregistrering.StartTid,
            tidsregistrering.SlutTid
            );
    }
    public static Tidsregistrering MapTilTidsregistreringEntity(TidsregistreringDTO tidsregistreringDTO)
    {
        return new Tidsregistrering(
            tidsregistreringDTO.TidsregistreringId,
            tidsregistreringDTO.MedarbejderId,
            tidsregistreringDTO.SagId,
            tidsregistreringDTO.StartTid,
            tidsregistreringDTO.SlutTid
            );
    }

    public static List<TidsregistreringDTO> MapTilTidsregistreringDtos(List<Tidsregistrering> tidsregistrerings)
    {
        return tidsregistrerings.Select(tr => new TidsregistreringDTO()
        {
            MedarbejderId = tr.MedarbejderId,
            SagId = tr.SagId,
            StartTid = tr.StartTid,
            SlutTid = tr.SlutTid,
            TidsregistreringId = tr.TidsregistreringId
        }
        ).ToList();
    }
}