using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class LocalitiesRepository
{
    private ApplicationContext context;

    public LocalitiesRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }

    public Locality Get(int id)
    {
        return context.Localities.Include(l => l.LocalityCosts).Single(o => o.Id == id);
    }

    public List<Locality> GetAll()
    {
        return context.Localities.Include(l => l.LocalityCosts).ToList();
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
