using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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
        //здесь прописываем связи сущностей и первоначальные данные
        //подключеине к бд через файл эппсеттингс
    }

}
