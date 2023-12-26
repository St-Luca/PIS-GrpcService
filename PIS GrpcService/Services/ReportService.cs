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
        var totalCostApp = actsRepository.GetAppsTotalCost(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.TypeId);

        return GenerateClosedAppsReport(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.OrganizationName, totalCostApp);
    }


    public override async Task<GrpcReport> MakeClosedContractsReport(ReportRequest reportRequest, ServerCallContext context)
    {
        var sum = actsRepository.GetContractsSum(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.OrganizationName);

        return MakeClosedContractsReport(reportRequest.StartDate.ToDateTime(), reportRequest.EndDate.ToDateTime(), reportRequest.OrganizationName, sum);
    }

    public GrpcReport MakeClosedContractsReport(DateTime startDate, DateTime endDate, string orgName, int sum)
    {
        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по стоимости закрытых контрактов за период по организации",
            Description = $"За период с {startDate.Date} по {endDate.Date} в организации {orgName} " +
              $"было закрыто контрактов на сумму: {sum}"
        };
    }

    public GrpcReport GenerateClosedAppsReport(DateTime startDate, DateTime endDate, string localityName, int totalCostApp)
    {
        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по стоимости закрытых заявок за период по населенному пункту",
            Description = $"За период с {startDate.Date} по {endDate.Date} в населенном пункте {localityName} " +
              $"было закрыто заявок на сумму: {totalCostApp}"
        };
    }
}
