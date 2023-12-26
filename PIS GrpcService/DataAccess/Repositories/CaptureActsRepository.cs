using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

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
        return context.Acts.Include(a => a.Animals).Include(a => a.Applications).Include(a => a.Contract).Include(a => a.Performer).ToList();
    }

    public int GetDoneAppsInPeriodCount(DateTime startDate, DateTime endDate, int localityName)
    {
        var doneAppsCount = GetDoneAppsInPeriod(startDate, endDate, localityName).Count;
        
        return doneAppsCount;
    }

    public List<Application> GetDoneAppsInPeriod(DateTime startDate, DateTime endDate, int localityName)
    {
        var allDoneApps = new List<Application>();

        var allActs = context.Acts.Include(c => c.Applications).Include(c => c.Locality).ToList();

        var doneActs = allActs.Where(act => act.IsInPeriodAndLocality(startDate, endDate, localityName)).ToList();

        foreach (var act in doneActs)
        {
            var doneAppsOfAct = GetDoneAppsInPeriod(startDate, endDate, localityName, act.Id);

            allDoneApps = allDoneApps.Concat(doneAppsOfAct).ToList();
        }

        return allDoneApps;
    }

    public List<Application> GetDoneAppsInPeriod(DateTime startDate, DateTime endDate, int localityName, int actId)
    {
        var apps = context.Applications.Include(a => a.Locality).ToList();

        return apps.Where(app => app.IsInPeriodAndLocality(startDate, endDate, localityName) && app.IdAct == actId).ToList();
    }

    public int GetContractsSum(DateTime startDate, DateTime endDate, string orgName)
    {
        var allActs = context.Acts.Include(c => c.Applications).Include(c => c.Locality).Include(c => c.Performer).Include(c => c.Contract).ToList();

        var actsByOrg = allActs.Where(act => act.IsInOrganization(orgName)).ToList();

        var totalSum = 0;

        foreach (var act in actsByOrg)
        {
            if(act.Contract.EffectiveDate >= startDate && act.Contract.EffectiveDate <= endDate)
            {
                var costInCity = act.Contract.GetCostContract(act.IdLocality);
                totalSum += costInCity.Cost;
            }

        }
        return totalSum;
    }

    public int GetAppsTotalCost(DateTime startDate, DateTime endDate, int localityId)
    {
        var allActs = context.Acts.Include(c => c.Applications).Include(c => c.Locality).Include(c => c.Performer).Include(c => c.Contract).ToList();

        var closedActs = allActs.Where(act => act.IsInPeriodAndLocality(startDate, endDate, localityId)).ToList();

        var totalCost = 0;

        foreach (var act in closedActs)
        {
            foreach (var app in act.Applications.Where(app => app.IsInPeriodAndLocality(startDate, endDate, act.Locality.Id)))
            {
                var costInCity = act.GetCostClosedApp(act.IdLocality);
                totalCost += costInCity.Cost;
            } 
        }

        return totalCost;
    }
}
