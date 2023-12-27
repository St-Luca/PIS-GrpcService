﻿using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.Models;

namespace PIS_GrpcService.DataAccess.Repositories;

public class CatchingApplicationsRepository
{
    private ApplicationContext context;

    public CatchingApplicationsRepository(ApplicationContext dbContext)
    {
        context = dbContext;
    }

    public void Add(Application organization)
    {
        context.Applications.Add(organization);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        context.Applications.Remove(context.Applications.First(o => o.Id == id));
        context.SaveChanges();
    }

    public Application Get(int id)
    {
        return context.Applications.Include(l => l.Act)
            .Include(l => l.Locality).ThenInclude(a => a.LocalityCosts)
            .Include(l => l.Organization).Single(o => o.Id == id);
    }

    public void Edit(Application organization)
    {
        context.Applications.Update(organization);
        context.SaveChanges();
    }

    public List<Application> GetAll()
    {
        return context.Applications.Include(l => l.Act)
            .Include(l => l.Locality).ThenInclude(a => a.LocalityCosts)
            .Include(l => l.Organization).ToList();
    }

    public int GetAllAppsInPeriodCount(DateTime startDate, DateTime endDate, int localityName)
    {
        var allApps = GetAppsInPeriod(startDate, endDate, localityName);

        return allApps.Count;
    }

    public List<Application> GetAppsInPeriod(DateTime startDate, DateTime endDate, int localityName)
    {
        var apps = context.Applications.Include(a => a.Locality).ToList();

        return apps.Where(app => app.IsInPeriodAndLocality(startDate, endDate, localityName)).ToList();
    }
}
