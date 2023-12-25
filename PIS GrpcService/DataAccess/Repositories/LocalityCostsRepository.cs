using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class LocalityCostsRepository
{
    private ApplicationContext context;

    public LocalityCostsRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }
    public LocalityCost Get(int id) /////////////
    {
        return context.LocalityCosts.Single(o => o.IdContract == id);
    }

    public List<LocalityCost> GetAll()
    {
        return context.LocalityCosts.ToList();
    }
}
