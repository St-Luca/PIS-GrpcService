using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class CaptureActsRepository
{
    private ApplicationContext context;

    public CaptureActsRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }

    public void Add(CaptureAct organization)
    {
        context.Acts.Add(organization);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Acts.Remove(context.Acts.First(o => o.Id == id));
        context.SaveChanges();
    }

    public CaptureAct Get(int id)
    {
        return context.Acts.Single(o => o.Id == id);
    }

    public void Edit(CaptureAct organization)
    {
        context.Acts.Update(organization);
        context.SaveChanges();
    }

    public List<CaptureAct> GetAll()
    {
        return context.Acts.ToList();
    }
}
