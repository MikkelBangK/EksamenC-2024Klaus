using DTO.Model;
using Eksamensprojekt.Context;
using Eksamensprojekt.Repositories;

namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        VirksomhedContext context = new VirksomhedContext();
    }

    [Test]
    public void Test1()
    {
     AfdelingDTO afdelingDto = new AfdelingDTO(1, "Navn", 1);
     MedarbejderDTO medarbejderDto = new MedarbejderDTO("EC",9101, "Emil Carlsen");
     VirksomhedRepository.AddMedarbejder(medarbejderDto,afdelingDto);
    }
    
}