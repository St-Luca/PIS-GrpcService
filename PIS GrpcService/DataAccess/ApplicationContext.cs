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
    //public DbSet<LocalityCost> LocalityCosts { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    //public DbSet<Animal> Animals { get; set; } = default!;
    //public DbSet<CaptureAct> Acts { get; set; } = default!;
    public DbSet<Application> Applications { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Organization o1 = new Organization { Id = 1, OrgName = "Smartway", INN = "111", KPP = "ss" };
        Organization o2 = new Organization { Id = 2, OrgName = "TumGU", INN = "222", KPP = "wtf" };

        modelBuilder.Entity<Organization>().HasData(o1, o2);

        Application App1 = new Application { Id = 1, Date = DateTime.Now, ApplicantCategory = "Категория заявителя", AnimalDescription = "Коричневая овцарка", Urgency = "14", Locality = "1", Organization = "1" };
        modelBuilder.Entity<Application>().HasData(App1);

        Locality loc1 = new Locality { Id=1, Name = "Тюмень" };
        Locality loc2 = new Locality { Id=2, Name = "Ялуторовск" };
        modelBuilder.Entity<Application>().HasData(loc1, loc2);

        //здесь прописываем связи сущностей и первоначальные данные
        //подключеине к бд через файл эппсеттингс
    }

}
