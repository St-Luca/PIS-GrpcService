using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class LocalitiesRepository
{
    private ApplicationContext context;

    public LocalitiesRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }

    public void Add(Locality organization)
    {
        context.Localities.Add(organization);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Localities.Remove(context.Localities.First(o => o.Id == id));
        context.SaveChanges();
    }

    public Locality Get(int id)
    {
        return context.Localities.Single(o => o.Id == id);
    }

    public void Edit(Locality organization)
    {
        context.Localities.Update(organization);
        context.SaveChanges();
    }

    public List<Locality> GetAll()
    {
        return context.Localities.ToList();
    }

    public LocalityCost GetLocalityCostByLocality(int localityId)
    {
        return context.LocalityCosts.Single(o => o.IdLocality == localityId);
    }

    public LocalityCost GetLocalityCostByContract(int contractId)
    {
        return context.LocalityCosts.Single(o => o.IdContract == contractId);
    }
}
