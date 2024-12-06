using DTO.Model;
using Eksamensprojekt.Context;
using Eksamensprojekt.Data.Model;
using Eksamensprojekt.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Eksamensprojekt.Repositories;

public class VirksomhedRepository
{
    
    //Adders
    public static void AddAfdeling(AfdelingDTO afdelingDto)
    {
        using var db = new VirksomhedContext();
        afdelingDto.AfdelingsId = 0;
        var afdelingEntity = Mapper.MapTilAfdelingEntity(afdelingDto);  // Bruger mapperen
        db.Afdelinger.Add(afdelingEntity);
        db.SaveChanges();
    }

    public static void AddMedarbejder(MedarbejderDTO medarbejderDto, AfdelingDTO afdelingDto)
    {
        using var db = new VirksomhedContext();
        medarbejderDto.MedarbejderId = 0;

        var afdelingEntity = db.Afdelinger
            .FirstOrDefault(a => a.AfdelingsId == afdelingDto.AfdelingsId);

        if (afdelingEntity == null)
        {
            afdelingEntity = new Afdeling
            {
                AfdelingsId = afdelingDto.AfdelingsId,
                Navn = afdelingDto.Navn,
                Nummer = afdelingDto.Nummer
            };

            db.Afdelinger.Add(afdelingEntity);
            db.SaveChanges();
        }

        var medarbejderEntity = new Medarbejder
        {
            MedarbejderId = medarbejderDto.MedarbejderId,
            Initial = medarbejderDto.Initial,
            Cpr = medarbejderDto.Cpr,
            Navn = medarbejderDto.Navn,
            AfdelingsId = afdelingEntity.AfdelingsId
        };

        db.Medarbejdere.Add(medarbejderEntity);
        db.SaveChanges();
    }

    public static void AddSag(SagDTO sagDto)
    {
        using var db = new VirksomhedContext();
        sagDto.SagsNummer = 0;
        var sagEntity = Mapper.MapTilSagEntity(sagDto);
        db.Sager.Add(sagEntity);
        db.SaveChanges();
    }

    public static void AddTidsregistrering(TidsregistreringDTO tidsregistreringDto)
    {
        using var db = new VirksomhedContext();
        tidsregistreringDto.TidsregistreringId = 0;
        var tidsregistreringEntity = Mapper.MapTilTidsregistreringEntity(tidsregistreringDto);
        db.Tidsregistreringer.Add(tidsregistreringEntity);
        db.SaveChanges();
    }
    
    //Logik
    public static void RegistrerTidsregistrering(TidsregistreringDTO tidsregistreringDto)
    {
        using var db = new VirksomhedContext();
        var tidsregistreringEntity = new Tidsregistrering(
            tidsregistreringDto.TidsregistreringId,
            tidsregistreringDto.MedarbejderId,
            tidsregistreringDto.SagId,
            tidsregistreringDto.StartTid, 
            tidsregistreringDto.SlutTid);
        db.Tidsregistreringer.Add(tidsregistreringEntity);
        db.SaveChanges();
    }

    public static void RegistrerTidUdenSag(MedarbejderDTO medarbejderDto, TidsregistreringDTO tidsregistreringDto)
    {
        using var db = new VirksomhedContext();
        var tidsregistreringEntity = new Tidsregistrering(
            tidsregistreringDto.TidsregistreringId,
            tidsregistreringDto.MedarbejderId,
            tidsregistreringDto.SagId,
            tidsregistreringDto.StartTid,
            tidsregistreringDto.SlutTid);
        db.Tidsregistreringer.Add(tidsregistreringEntity);
        db.SaveChanges();
    }
    
    public static void AddTidsregistreringToSag(SagDTO sag, TidsregistreringDTO tidsregistrering)
    {
        using var db = new VirksomhedContext();
        var sagEntity = db.Sager.FirstOrDefault(s => s.SagsNummer == sag.SagsNummer);
        var tidsregistreringEntity = new Tidsregistrering(
            tidsregistrering.TidsregistreringId,
            tidsregistrering.MedarbejderId,
            tidsregistrering.SagId,
            tidsregistrering.StartTid, 
            tidsregistrering.SlutTid);
        db.Tidsregistreringer.Add(tidsregistreringEntity);
        db.SaveChanges();
    }

    //Getters
    public static List<TidsregistreringDTO> GetTidsregistreringer()
    {
        using var db = new VirksomhedContext();
        return db.Tidsregistreringer
            .Select(t => Mapper.MapTilTidsregistreringDTO(t))
            .ToList();
    }
    public static MedarbejderDTO GetMedarbejder(int medarbejderId)
    {
        using var db = new VirksomhedContext();
        var medarbejderEntity = db.Medarbejdere.FirstOrDefault(m => m.MedarbejderId == medarbejderId);
        return Mapper.MapTilMedarbejderDTO(medarbejderEntity);
    }

    public static List<MedarbejderDTO> GetMedarbejdere()
    {
        using var db = new VirksomhedContext();
        List<Medarbejder> medarbejders = db.Medarbejdere
            .Include((t => t.Tidsregistrerings))
            .ToList();
        List<MedarbejderDTO> medarbejderDtos = medarbejders.Select(m => Mapper.MapTilMedarbejderDTO(m)).ToList();
        return medarbejderDtos;
    }

    public static List<SagDTO> GetSager()
    {
        using var db = new VirksomhedContext();
        return db.Sager
            .Select(s => Mapper.MapTilSagDTO(s))
            .ToList();

    }

    public static List<AfdelingDTO> GetAfdelinger()
    {
        using var db = new VirksomhedContext();
        return db.Afdelinger
            .Select(a => Mapper.MapTilAfdelingDTO(a))
            .ToList();
    }
    
    public static AfdelingDTO GetAfdeling(int afdelingId)
    {
        using var db = new VirksomhedContext();
        var afdelingEntity = db.Afdelinger.FirstOrDefault(a => a.AfdelingsId == afdelingId);
        return Mapper.MapTilAfdelingDTO(afdelingEntity);
    }

    public static TidsregistreringDTO OpretTidsregistrering(TidsregistreringDTO tidsregistreringDto)
    {
        using var db = new VirksomhedContext();

        var tidsregistreringsEntity = new Tidsregistrering
        {
            MedarbejderId = tidsregistreringDto.MedarbejderId,
            StartTid = tidsregistreringDto.StartTid,
            SlutTid = tidsregistreringDto.SlutTid,
            SagId = tidsregistreringDto.SagId
        };

        db.Tidsregistreringer.Add(tidsregistreringsEntity);
        db.SaveChanges();

        return Mapper.MapTilTidsregistreringDTO(tidsregistreringsEntity);
    }
}