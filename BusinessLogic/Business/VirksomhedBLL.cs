using DTO.Model;
using Eksamensprojekt.Repositories;

namespace BusinessLogic.Business;

public class VirksomhedBLL
{
    
    public void AddTidsregistrering(TidsregistreringDTO tidsregistreringdto)
    {
        VirksomhedRepository.RegistrerTidsregistrering(tidsregistreringdto);
    }
    public void AddTidUdenSag(MedarbejderDTO medarbejderdto, TidsregistreringDTO tidsregistreringdto)
    {
        VirksomhedRepository.RegistrerTidUdenSag(medarbejderdto, tidsregistreringdto);
    }

    public void AddAfdeling(AfdelingDTO afdelingDto)
    {
        VirksomhedRepository.AddAfdeling(afdelingDto);
    }
    
    public void AddMedarbejder(MedarbejderDTO medarbejderDto, AfdelingDTO afdelingDto)
    {
        VirksomhedRepository.AddMedarbejder(medarbejderDto, afdelingDto);
    }

    public void AddSag(SagDTO sagDto)
    {
        VirksomhedRepository.AddSag(sagDto);
    }
    
    //Getters
    public List<AfdelingDTO> GetAfdelinger()
    {
        return VirksomhedRepository.GetAfdelinger();
    }
    public List<MedarbejderDTO> GetMedarbejdere()
    {
        return VirksomhedRepository.GetMedarbejdere();
    }
    public List<SagDTO> GetSager()
    {
        return VirksomhedRepository.GetSager();
    }
    public List<TidsregistreringDTO> GetTidsregistreringer()
    {
        return VirksomhedRepository.GetTidsregistreringer();
    }

    public TidsregistreringDTO OpretTidsregistrering(TidsregistreringDTO tidsregistreringDto)
    {
        return VirksomhedRepository.OpretTidsregistrering(tidsregistreringDto);
    }
    
    public MedarbejderDTO GetMedarbejder(int medarbejderId)
    {
        return VirksomhedRepository.GetMedarbejder(medarbejderId);
    }
    
    public AfdelingDTO GetAfdeling(int afdelingId)
    {
        return VirksomhedRepository.GetAfdeling(afdelingId);
    }
  
}