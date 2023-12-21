using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using Google.Protobuf.WellKnownTypes;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class ReportService : GrpcReportService.GrpcReportServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<ReportService> _logger;
    public ReportService(ILogger<ReportService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override async Task<GrpcReport> GenerateAppsPercentReport(ReportRequest reportRequest, ServerCallContext context)
    {
        var allApps = _dbContext.Applications;
        var allActs = _dbContext.Acts;

        var allDoneApps = allApps.Where(a => a.Date >= reportRequest.StartDate.ToDateTime() &&
        a.Date <= reportRequest.EndDate.ToDateTime() && a.Locality.Name == reportRequest.TypeName).ToList();

        var allDoneAppsOfActs = allActs.Where(a => a.ActDate >= reportRequest.StartDate.ToDateTime() &&
        a.ActDate <= reportRequest.EndDate.ToDateTime() && a.Locality.Name == reportRequest.TypeName).SelectMany(c => c.Applications)
        .Where(a => a.Date >= reportRequest.StartDate.ToDateTime() &&
        a.Date <= reportRequest.EndDate.ToDateTime() && a.Locality.Name == reportRequest.TypeName).ToList();

        //var report =  GrpcReport().GenerateAppsPercentReport(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.TypeName);
        return new GrpcReport{ 
              Number = 1,
              Name = "Отчет по проценту выполненных заявок в населенном пункте",
              Description = $"За период с {reportRequest.StartDate.ToDateTime().Date} по {reportRequest.EndDate.ToDateTime().Date} в нас. пункте {reportRequest.TypeName} " +
              $"было выполнено {allDoneAppsOfActs.Count/ allDoneApps.Count} заявок"
            };
    }

    public override async Task<GrpcReport> GenerateClosedAppsReport(ReportRequest reportRequest, ServerCallContext context)
    { 
        var allActs = _dbContext.Acts;

        var allDoneActs = allActs.Where(a => a.ActDate >= reportRequest.StartDate.ToDateTime() &&
        a.ActDate <= reportRequest.EndDate.ToDateTime() && a.Locality.Name == reportRequest.TypeName).ToList();

        var allDoneApplications = allDoneActs
            .SelectMany(act => act.Applications)
            .ToList();

        int totalCost = 0;

        foreach (var application in allDoneApplications)
        {
            var contract = application.Act?.Contract; 
            if (contract != null)
            {
                // Если контракт есть, добавляем стоимость 1 отлова к общей сумме
                totalCost += contract.LocalityCosts.FirstOrDefault(lc => lc.IdContract == contract.Id && lc.Locality == application.Act.Locality).Cost;
            }
        }

        //var report =  GrpcReport().GenerateAppsPercentReport(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.TypeName);
        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по стоимости закрытых заявок в населенном пункте за период",
            Description = $"За период с {reportRequest.StartDate.ToDateTime().Date} по {reportRequest.EndDate.ToDateTime().Date} в нас. пункте {reportRequest.TypeName} " +
              $"было закрыто заявок на сумму: {totalCost}"
        };
    }

    public override async Task<GrpcReport> GenerateClosedContractsSumReport(ReportRequest reportRequest, ServerCallContext context)
    {
        var contracts = _dbContext.Contracts;

        var allDoneContracts = contracts.Where(a => a.EffectiveDate >= reportRequest.StartDate.ToDateTime() &&
        a.EffectiveDate <= reportRequest.EndDate.ToDateTime() && a.Performer.OrgName == reportRequest.TypeName).ToList();

        var sum = 0;

        foreach (var contract in allDoneContracts)
        {
            foreach (var act in contract.CaptureActs)
            {
                var locality = act.Locality;
                var localityCost = _dbContext.LocalityCosts.FirstOrDefault(lc => lc.IdLocality == locality.Id && lc.IdContract == contract.Id);

                if (localityCost != null)
                {
                    sum += localityCost.Cost;
                }
            }
        }

        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по стоимости закрытых контрактов за период по организации",
            Description = $"За период с {reportRequest.StartDate.ToDateTime().Date} по {reportRequest.EndDate.ToDateTime().Date} в нас. пункте {reportRequest.TypeName} " +
              $"было закрыто контрактов на сумму: {sum}"
        };
        //var allDoneApps = _dbContext.Applications.Select(o => o.Map()).ToList(); //apprep.GetAllDoneApps
        //var response = _dbContext.Organizations.Select(o => o.Map()).ToList();

        //var result = new OrganizationArray();
        //result.List.AddRange(response);

        //return Task.FromResult(result);
    }
}
