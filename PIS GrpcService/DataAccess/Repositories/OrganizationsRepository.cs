using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class OrganizationsRepository
{
    private ApplicationContext context;

    public OrganizationsRepository(ApplicationContext dbContext) 
    {
        context = dbContext;
    }

    public void Add(Organization organization)
    {
        context.Organizations.Add(organization);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Organizations.Remove(context.Organizations.First(o => o.Id == id));
        context.SaveChanges();
    }

    public Organization Get(int id)
    {
        return context.Organizations.Include(o => o.Acts).Include(o => o.Applications).Include(o => o.Contracts).Single(o => o.Id == id);
    }

    public void Edit(Organization organization) 
    { 
        context.Organizations.Update(organization);
        context.SaveChanges();
    }

    public List<Organization> GetAll()
    {
        return context.Organizations.Include(o => o.Acts).Include(o => o.Applications).Include(o => o.Contracts).ToList();
    }
}
