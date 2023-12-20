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
    //public DbSet<CaptureAct> Acts { get; set; } = default!;
    //public DbSet<Contract> Contracts { get; set; } = default!;
    public DbSet<Application> Applications { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocalityCost>()
                       .HasKey(p => p.IdLocality);
        modelBuilder.Entity<Application>()
                       .HasKey(p => p.Id);

        Organization o1 = new Organization { Id = 1, OrgName = "Smartway", INN = "111", KPP = "ss" };
        Organization o2 = new Organization { Id = 2, OrgName = "TumGU", INN = "222", KPP = "wtf" };

        modelBuilder.Entity<Organization>().HasData(o1, o2);

        Locality loc1 = new Locality { Id = 1, Name = "Тюмень"};
        modelBuilder.Entity<Locality>().HasData(loc1);

        LocalityCost locCost1 = new LocalityCost { IdCost = 1, IdContract = "1", IdLocality = "1", Cost = "15000" };
        modelBuilder.Entity<LocalityCost>().HasData(locCost1);

        Animal animal1 = new Animal { Id = 1, Category = "Собака", Sex = "Кобель", Breed = "Овчарка", Size = "Большая", Coat = "Густая", Color = "Коричневая", Ears = "Коричневая", Tail = "Короткий", CapturedAct = "1", Mark = "134", IdentChip = "192"};
        modelBuilder.Entity<Animal>().HasData(animal1);

        Application app1 = new Application { Id = 1, Date = DateTime.UtcNow, ApplicantCategory = "Категория заявителя", AnimalDescription = "Коричневая овцарка", Urgency = "14", Locality = "1", Organization = "1" };
        modelBuilder.Entity<Application>().HasData(app1);
        //здесь прописываем связи сущностей и первоначальные данные
        //подключеине к бд через файл эппсеттингс
    }

}
