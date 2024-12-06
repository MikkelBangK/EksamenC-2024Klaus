using System.Diagnostics;
using Eksamensprojekt.Data.Model;
using Microsoft.EntityFrameworkCore;


namespace Eksamensprojekt.Context;

public class VirksomhedContext : DbContext
{
    public VirksomhedContext()
    {
        bool created = Database.EnsureCreated();
        if (created)
        {
            Debug.WriteLine("Database created");
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=eksamen;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=true");
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Afdeling>().HasData(new Afdeling[]
        {
            new Afdeling { AfdelingsId = 1, Navn = "Salg", Nummer = 1 },
            new Afdeling { AfdelingsId = 2, Navn = "Support", Nummer = 2 },
            new Afdeling { AfdelingsId = 3, Navn = "Udvikling", Nummer = 3 }
        });
        modelBuilder.Entity<Medarbejder>()
            .HasOne(m => m.Afdeling)
            .WithMany()
            .HasForeignKey(m => m.AfdelingsId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Medarbejder>().HasData(new Medarbejder[]
        {
            new Medarbejder { MedarbejderId = 1, Initial = "NK", Cpr = 1234, Navn = "Nikolaj Kahr", AfdelingsId = 1},
            new Medarbejder { MedarbejderId = 2, Initial = "BG", Cpr = 5678, Navn = "Benjamin Gregersen", AfdelingsId = 1},
            new Medarbejder { MedarbejderId = 3, Initial = "EC", Cpr = 9101, Navn = "Emil Carlsen", AfdelingsId = 1},
        });

        modelBuilder.Entity<Sag>().HasData(new Sag[]
        {
            new Sag { SagsNummer = 1, Beskrivelse = "Øl", Overskrift = "Bajer", AfdelingsId = 1},
        });
        modelBuilder.Entity<Tidsregistrering>().HasData(new Tidsregistrering[]
        {
            new Tidsregistrering { 
                TidsregistreringId = 1, 
                MedarbejderId = 1,
                SagId = 1,
                StartTid = new DateTime(2024, 12, 4, 8, 30, 0),
                SlutTid = new DateTime(2024, 12, 4, 17, 0,0 ),
            },
            new Tidsregistrering { 
                TidsregistreringId = 2, 
                MedarbejderId = 1,
                SagId = 1,
                StartTid = new DateTime(2024, 12, 4, 8, 30, 0),
                SlutTid = new DateTime(2024, 12, 4, 17, 0,0 ),
            },
            new Tidsregistrering { 
                TidsregistreringId = 3, 
                MedarbejderId = 1,
                SagId = 1,
                StartTid = new DateTime(2024, 12, 4, 8, 30, 0),
                SlutTid = new DateTime(2024, 12, 4, 17, 0,0 ),
            }
            
        });
    }
    public DbSet<Afdeling> Afdelinger {get ; set;}
    public DbSet<Medarbejder> Medarbejdere {get ; set;}
    public DbSet<Sag> Sager {get ; set;}
    public DbSet<Tidsregistrering> Tidsregistreringer {get ; set;}
}