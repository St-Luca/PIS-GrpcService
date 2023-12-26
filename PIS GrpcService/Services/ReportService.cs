using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using Google.Protobuf.WellKnownTypes;
using PIS_GrpcService.Services.Mappers;
using PIS_GrpcService.DataAccess.Repositories;

namespace PIS_GrpcService.Services;

public class ReportService : GrpcReportService.GrpcReportServiceBase
{

    private readonly CatchingApplicationsRepository applicationsRepository;
    private readonly CaptureActsRepository actsRepository;
    private readonly MunicipalContractsRepository contractsRepository;
    private readonly LocalityCostsRepository localityCostsRepository;


    private readonly ILogger<ReportService> _logger;
    public ReportService(ILogger<ReportService> logger, CatchingApplicationsRepository catchingApplicationsRepository, 
        CaptureActsRepository captureActsRepository, 
        MunicipalContractsRepository contractsRepository, 
        LocalityCostsRepository localityCostsRepository)
    {
        _logger = logger;
        applicationsRepository = catchingApplicationsRepository;
        actsRepository = captureActsRepository;
        this.contractsRepository = contractsRepository;
        this.localityCostsRepository = localityCostsRepository;
    }

    public override async Task<GrpcReport> GenerateAppsPercentReport(ReportRequest reportRequest, ServerCallContext context)
    {
        var reportGenerator = new ReportGenerator(actsRepository, applicationsRepository);

        return reportGenerator.GenerateAppsPercentReport(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.TypeId);
    }

    public override async Task<GrpcReport> GenerateClosedAppsReport(ReportRequest reportRequest, ServerCallContext context)
    {
        var allActs = actsRepository.GetAll();
        //var allActs = _dbContext.Acts
        //    .Include(a => a.Applications)
        //    .ThenInclude(app => app.Act.Contract.LocalityCosts);

        var allDoneActs = allActs
            .Where(a => a.ActDate >= reportRequest.StartDate.ToDateTime() &&
                        a.ActDate <= reportRequest.EndDate.ToDateTime() &&
                        a.Locality.Id == reportRequest.TypeId)
            .ToList();

        int totalCost = 0;

        foreach (var act in allDoneActs)
        {
            foreach (var application in act.Applications)
            {
                var contract = application.Act?.Contract;
                var locality = application.Act?.Locality;

                if (contract != null && locality != null)
                {
                    var localityCost = contract.LocalityCosts.FirstOrDefault(lc => lc.IdLocality == locality.Id);
                    if (localityCost != null)
                    {
                        // Добавляем стоимость заявки к общей сумме
                        totalCost += localityCost.Cost;
                    }
                }
            }
        }

        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по стоимости закрытых заявок в населенном пункте за период",
            Description = $"За период с {reportRequest.StartDate.ToDateTime().Date} по {reportRequest.EndDate.ToDateTime().Date} в нас. пункте {reportRequest.TypeId} " +
                $"было закрыто заявок на сумму: {totalCost}"
        };
    }


    public override async Task<GrpcReport> GenerateClosedContractsSumReport(ReportRequest reportRequest, ServerCallContext context)
    {
        //var sum = actsRepository.GetContractsSum(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.TypeId);

        var contracts = contractsRepository.GetAll();
        var acts = actsRepository.GetAll();
        var localityCosts = localityCostsRepository.GetAll();
        //var contracts = new ContractService.GetAllAsync();
        //var contracts = _dbContext.Contracts;      

        var allDoneContracts = contracts.Where(a => a.EffectiveDate >= reportRequest.StartDate.ToDateTime() &&
        a.EffectiveDate <= reportRequest.EndDate.ToDateTime() && a.Performer.Id == reportRequest.TypeId).ToList();

        var actsOfAllDoneContracts = acts.Where(a => allDoneContracts.Select(c => c.Id).Contains(a.IdContract)).ToList();

        var sum = 0;

        foreach (var act in actsOfAllDoneContracts)
        {
            var locality = act.Locality;
            var localityCost = localityCosts.FirstOrDefault(lc => lc.IdLocality == locality.Id && lc.IdContract == act.IdContract);

            if (localityCost != null)
            {
                sum += localityCost.Cost;
            }
        }


        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по стоимости закрытых контрактов за период по организации",
            Description = $"За период с {reportRequest.StartDate.ToDateTime().Date} по {reportRequest.EndDate.ToDateTime().Date} в нас. пункте {reportRequest.TypeId} " +
              $"было закрыто контрактов на сумму: {sum}"
        };
        //var allDoneApps = _dbContext.Applications.Select(o => o.Map()).ToList(); //apprep.GetAllDoneApps
        //var response = _dbContext.Organizations.Select(o => o.Map()).ToList();

        //var result = new OrganizationArray();
        //result.List.AddRange(response);

        //return Task.FromResult(result);
    }
}
