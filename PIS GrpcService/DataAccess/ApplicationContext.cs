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
    //public DbSet<Locality> Localities { get; set; } = default!;
    //public DbSet<LocalityCost> LocalityCosts { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    //public DbSet<Animal> Animals { get; set; } = default!;
    //public DbSet<CaptureAct> Acts { get; set; } = default!;
    //public DbSet<CatchingApplication> CatchingApplications { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Organization o1 = new Organization { Id = 1, OrgName = "Smartway", INN = "111", KPP = "ss" };
        Organization o2 = new Organization { Id = 2, OrgName = "TumGU", INN = "222", KPP = "wtf" };

        modelBuilder.Entity<Organization>().HasData(o1, o2);

        //здесь прописываем связи сущностей и первоначальные данные
        //подключеине к бд через файл эппсеттингс
    }

}
