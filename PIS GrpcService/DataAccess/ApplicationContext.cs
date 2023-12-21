using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Locality> Localities { get; set; } = default!;
    public DbSet<LocalityCost> LocalityCosts { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<Animal> Animals { get; set; } = default!;
    public DbSet<CaptureAct> Acts { get; set; } = default!;
    public DbSet<Contract> Contracts { get; set; } = default!;
    public DbSet<Application> Applications { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<LocalityCost>()
        .HasKey(lc => new { lc.IdContract, lc.IdLocality });

        modelBuilder.Entity<LocalityCost>()
            .HasOne(lc => lc.Contract)
            .WithMany(c => c.LocalityCosts)
            .HasForeignKey(lc => lc.IdContract);

        modelBuilder.Entity<LocalityCost>()
            .HasOne(lc => lc.Locality)
            .WithMany(l => l.LocalityCosts)
            .HasForeignKey(lc => lc.IdLocality);

        modelBuilder.Entity<Organization>()
                    .HasMany(p => p.Applications)
                    .WithOne(d => d.Organization)
                    .HasForeignKey(d => d.IdOrganization);

        modelBuilder.Entity<Organization>()
                    .HasMany(p => p.Contracts)
                    .WithOne(d => d.Performer)
                    .HasForeignKey(d => d.IdOrganization);

        modelBuilder.Entity<CaptureAct>()
                   .HasMany(p => p.Applications)
                   .WithOne(d => d.Act)
                   .HasForeignKey(d => d.IdAct);

        modelBuilder.Entity<CaptureAct>()
                   .HasMany(p => p.Animals)
                   .WithOne(d => d.Act)
                   .HasForeignKey(d => d.IdCaptureAct);

        modelBuilder.Entity<Contract>()
                   .HasMany(p => p.CaptureActs)
                   .WithOne(d => d.Contract)
                   .HasForeignKey(d => d.IdContract);


        Organization o1 = new Organization { Id = 1, OrgName = "Smartway", INN = "111", KPP = "ss" };
        Organization o2 = new Organization { Id = 2, OrgName = "TumGU", INN = "222", KPP = "wtf" };

        modelBuilder.Entity<Organization>().HasData(o1, o2);

        Locality loc1 = new Locality { Id = 1, Name = "Тюмень"};
        Locality loc2 = new Locality { Id = 2, Name = "Челябинск" };
        Locality loc3 = new Locality { Id = 3, Name = "Омск" };
        Locality loc4 = new Locality { Id = 4, Name = "Сургут" };
        Locality loc5 = new Locality { Id = 5, Name = "Екатеринбург" };
        modelBuilder.Entity<Locality>().HasData(loc1, loc2, loc3, loc4, loc5);

        LocalityCost locCost1 = new LocalityCost { IdContract = 1, IdLocality = 1, Cost = 15000 };
        modelBuilder.Entity<LocalityCost>().HasData(locCost1);

        Animal animal1 = new Animal { Id = 1, Category = "Собака", Sex = "Кобель", Breed = "Овчарка", Size = "Большая", Coat = "Густая", Color = "Коричневая", Ears = "Коричневая", Tail = "Короткий", IdCaptureAct = 1, Mark = "134", IdentChip = "192"};
        modelBuilder.Entity<Animal>().HasData(animal1);

        Contract contract = new Contract { Id = 1, ConclusionDate = DateTime.UtcNow, EffectiveDate = DateTime.UtcNow, Amount = 12000, IdOrganization = o1.Id };
        CaptureAct act1 = new CaptureAct { Id = 1, ActDate = DateTime.UtcNow, IdOrganization = o1.Id, Amount = 10, IdCapturedAnimal = animal1.Id, IdContract = contract.Id, IdLocality = loc1.Id};

        modelBuilder.Entity<CaptureAct>().HasData(act1);
        modelBuilder.Entity<Contract>().HasData(contract);
        CaptureAct act2 = new CaptureAct { Id = 2, ActDate = new DateTime(2023, 10, 01).ToUniversalTime(), IdOrganization = o2.Id, Amount = 15, IdCapturedAnimal = animal1.Id, IdContract = contract.Id, IdLocality = 3 };


        Application app1 = new Application
        {
            Id = 1,
            AnimalDescription = "Gtc",
            Date = new DateTime(2023,01,01).ToUniversalTime(),
            ApplicantCategory = "app cat",
            IdLocality = loc1.Id,
            IdOrganization = o1.Id,
            IdAct = act1.Id,
            Urgency = "urg"
        };

        Application app2 = new Application
        {
            Id = 2,
            AnimalDescription = "Animal2",
            Date = new DateTime(2023, 10, 01).ToUniversalTime(),
            ApplicantCategory = "app cat",
            IdLocality = loc2.Id,
            IdOrganization = o1.Id,
            IdAct = 10,
            Urgency = "urg"
        };

        Application app3 = new Application
        {
            Id = 3,
            AnimalDescription = "Animal3",
            Date = new DateTime(2023, 12, 01).ToUniversalTime(),
            ApplicantCategory = "app cat",
            IdLocality = loc3.Id,
            IdOrganization = o1.Id,
            IdAct = act2.Id,
            Urgency = "urg"
        };
        modelBuilder.Entity<Application>().HasData(app1, app2, app3);

        Organization o3 = new Organization { Id = 3, OrgName = "TIU", INN = "21231", KPP = "12313131" };
        modelBuilder.Entity<Organization>().HasData(o3);

        LocalityCost locCost2 = new LocalityCost { IdContract = 1, IdLocality = 2, Cost = 11000 };
        LocalityCost locCost3 = new LocalityCost { IdContract = 2, IdLocality = 3, Cost = 10000 };
        modelBuilder.Entity<LocalityCost>().HasData(locCost2, locCost3);

        Animal animal2 = new Animal { Id = 2, Category = "Кошка", Sex = "Самка", Breed = "Сиамская", Size = "Большая", Coat = "Густая", Color = "Коричневая", Ears = "Коричневая", Tail = "Короткий", IdCaptureAct = 1, Mark = "132", IdentChip = "222" };
        modelBuilder.Entity<Animal>().HasData(animal2);

        Contract contract2 = new Contract { Id = 2, ConclusionDate = new DateTime(2023, 01, 01).ToUniversalTime(), EffectiveDate = DateTime.UtcNow, Amount = 10000, IdOrganization = o1.Id };

        modelBuilder.Entity<CaptureAct>().HasData(act2);
        modelBuilder.Entity<Contract>().HasData(contract2);
    }

}
