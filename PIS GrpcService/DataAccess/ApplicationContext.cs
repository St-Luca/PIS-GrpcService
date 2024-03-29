﻿using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;
using Contract = PIS_GrpcService.Models.Contract;

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

        modelBuilder.Entity<Contract>()
            .HasMany(lc => lc.LocalityCosts)
            .WithOne(c => c.Contract)
            .HasForeignKey(lc => lc.IdContract);

        modelBuilder.Entity<Locality>()
            .HasMany(lc => lc.LocalityCosts)
            .WithOne(c => c.Locality)
            .HasForeignKey(lc => lc.IdLocality);

        modelBuilder.Entity<Organization>()
                    .HasMany(p => p.Applications)
                    .WithOne(d => d.Organization)
                    .HasForeignKey(d => d.IdOrganization);

        modelBuilder.Entity<Organization>()
                    .HasMany(p => p.Acts)
                    .WithOne(d => d.Performer)
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


        Organization o1 = new Organization { Id = 1, OrgName = "Smartway", INN = "111", KPP = "ss" };
        Organization o2 = new Organization { Id = 2, OrgName = "TumGU", INN = "222", KPP = "kpp" };
        Organization o3 = new Organization { Id = 3, OrgName = "TIU", INN = "21231", KPP = "12313131" };
        modelBuilder.Entity<Organization>().HasData(o1, o2, o3);


        Locality loc1 = new Locality { Id = 1, Name = "Тюмень"};
        Locality loc2 = new Locality { Id = 2, Name = "Челябинск" };
        Locality loc3 = new Locality { Id = 3, Name = "Омск" };
        Locality loc4 = new Locality { Id = 4, Name = "Сургут" };
        Locality loc5 = new Locality { Id = 5, Name = "Екатеринбург" };
        modelBuilder.Entity<Locality>().HasData(loc1, loc2, loc3, loc4, loc5);


        Contract contract = new Contract { Id = 1, ConclusionDate = new DateTime(2023, 12, 01, 5, 0, 0).ToUniversalTime(), EffectiveDate = new DateTime(2024, 12, 31, 5, 0, 0).ToUniversalTime(), Amount = 12000, IdOrganization = o1.Id };
        Contract contract2 = new Contract { Id = 2, ConclusionDate = new DateTime(2023, 01, 01, 5, 0, 0).ToUniversalTime(), EffectiveDate = new DateTime(2023, 12, 31, 5, 0, 0).ToUniversalTime(), Amount = 10000, IdOrganization = o1.Id };
        modelBuilder.Entity<Contract>().HasData(contract, contract2);


        CaptureAct act1 = new CaptureAct { Id = 1, Date = new DateTime(2023, 01, 01, 5, 0, 0).ToUniversalTime(), IdOrganization = o1.Id, Amount = 10, IdContract = contract.Id, IdLocality = loc1.Id };
        CaptureAct act2 = new CaptureAct { Id = 2, Date = new DateTime(2023, 01, 10, 5, 0, 0).ToUniversalTime(), IdOrganization = o2.Id, Amount = 15, IdContract = contract.Id, IdLocality = loc3.Id };
        CaptureAct act3 = new CaptureAct { Id = 3, Date = new DateTime(2023, 10, 03, 5, 0, 0).ToUniversalTime(), IdOrganization = o3.Id, Amount = 20, IdContract = contract2.Id, IdLocality = loc4.Id };
        CaptureAct act4 = new CaptureAct { Id = 1000, Date = new DateTime(2025, 10, 03, 5, 0, 0).ToUniversalTime(), IdOrganization = o3.Id, Amount = 20, IdContract = contract2.Id, IdLocality = loc4.Id };

        modelBuilder.Entity<CaptureAct>().HasData(act1, act2, act3, act4);


        Animal animal1 = new Animal { Id = 1, Category = "Собака", Sex = "Кобель", Breed = "Овчарка", Size = "Большая", Coat = "Густая", Color = "Коричневая", Ears = "Коричневая", Tail = "Короткий", IdCaptureAct = act1.Id, Mark = "134", IdentChip = "192" };
        Animal animal2 = new Animal { Id = 2, Category = "Кошка", Sex = "Самка", Breed = "Сиамская", Size = "Большая", Coat = "Густая", Color = "Коричневая", Ears = "Коричневая", Tail = "Короткий", IdCaptureAct = act1.Id, Mark = "132", IdentChip = "222" };
        modelBuilder.Entity<Animal>().HasData(animal1, animal2);


        LocalityCost locCost1 = new LocalityCost { IdContract = contract.Id, IdLocality = loc1.Id, Cost = 15000 };
        LocalityCost locCost2 = new LocalityCost { IdContract = contract.Id, IdLocality = loc2.Id, Cost = 11000 };
        LocalityCost locCost3 = new LocalityCost { IdContract = contract.Id, IdLocality = loc3.Id, Cost = 10000 };
        LocalityCost locCost4 = new LocalityCost { IdContract = contract2.Id, IdLocality = loc2.Id, Cost = 10000 };
        LocalityCost locCost5 = new LocalityCost { IdContract = contract2.Id, IdLocality = loc3.Id, Cost = 12000 };
        LocalityCost locCost6 = new LocalityCost { IdContract = contract2.Id, IdLocality = loc4.Id, Cost = 13000 };
        modelBuilder.Entity<LocalityCost>().HasData(locCost1, locCost2, locCost3, locCost4, locCost5, locCost6);


        Application app1 = new Application
        {
            Id = 1,
            AnimalDescription = "Злой кот",
            Date = new DateTime(2023,01,01,5,0,0).ToUniversalTime(),
            ApplicantCategory = "Физ. лицо",
            IdLocality = loc1.Id,
            IdOrganization = o1.Id,
            IdAct = act1.Id,
            Urgency = "Срочно"
        };

        Application app4 = new Application
        {
            Id = 4,
            AnimalDescription = "Тут могло быть ваше животное",
            Date = new DateTime(2023, 01, 01, 5, 0, 0).ToUniversalTime(),
            ApplicantCategory = "Физ. лицо",
            IdLocality = loc1.Id,
            IdOrganization = o3.Id,
            Urgency = "Срочно"
        };

        Application app2 = new Application
        {
            Id = 2,
            AnimalDescription = "Злая собака",
            Date = new DateTime(2023, 10, 01, 5, 0, 0).ToUniversalTime(),
            ApplicantCategory = "Физ. лицо",
            IdLocality = loc2.Id,
            IdOrganization = o1.Id,
            IdAct = act3.Id,
            Urgency = "Чуть менее срочно"
        };

        Application app3 = new Application
        {
            Id = 3,
            AnimalDescription = "Добрая собака :(",
            Date = new DateTime(2023, 12, 01, 5, 0, 0).ToUniversalTime(),
            ApplicantCategory = "Физ. лицо",
            IdLocality = loc3.Id,
            IdOrganization = o2.Id,
            IdAct = act2.Id,
            Urgency = "Совсем не срочно"
        };

        modelBuilder.Entity<Application>().HasData(app1, app2, app3, app4);
    }

}
