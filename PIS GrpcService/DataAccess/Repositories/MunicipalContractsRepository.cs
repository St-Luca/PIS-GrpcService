using Microsoft.EntityFrameworkCore;
using Contract = PIS_GrpcService.Models.Contract;

namespace PIS_GrpcService.DataAccess.Repositories;

public class MunicipalContractsRepository
{
    private ApplicationContext context;

    public MunicipalContractsRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }

    public void Add(Contract contract)
    {
        context.Contracts.Add(contract);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Contracts.Remove(context.Contracts.First(o => o.Id == id));
        context.SaveChanges();
    }

    public Contract Get(int id)
    {
        return context.Contracts.Include(a => a.Performer).Include(a => a.LocalityCosts).Single(o => o.Id == id);
    }

    public void Edit(Contract contract)
    {
        context.Contracts.Update(contract);
        context.SaveChanges();
    }

    public List<Contract> GetAll()
    {
        return context.Contracts.Include(a => a.Performer).Include(a => a.LocalityCosts).ThenInclude(lc => lc.Locality).ToList();
    }
}
