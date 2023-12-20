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
        //var report =  GrpcReport().GenerateAppsPercentReport(startDate, endDate, localityName);
        return new GrpcReport();
        //var allDoneApps = _dbContext.Applications.Select(o => o.Map()).ToList(); //apprep.GetAllDoneApps
        //var response = _dbContext.Organizations.Select(o => o.Map()).ToList();

        //var result = new OrganizationArray();
        //result.List.AddRange(response);

        //return Task.FromResult(result);
    }

    public override async Task<GrpcReport> GenerateClosedAppsReport(ReportRequest reportRequest, ServerCallContext context)
    {
        //var report =  GrpcReport().GenerateAppsPercentReport(startDate, endDate, localityName);
        return new GrpcReport();
        //var allDoneApps = _dbContext.Applications.Select(o => o.Map()).ToList(); //apprep.GetAllDoneApps
        //var response = _dbContext.Organizations.Select(o => o.Map()).ToList();

        //var result = new OrganizationArray();
        //result.List.AddRange(response);

        //return Task.FromResult(result);
    }

    public override async Task<GrpcReport> GenerateClosedContractsSumReport(ReportRequest reportRequest, ServerCallContext context)
    {
        //var report =  GrpcReport().GenerateAppsPercentReport(startDate, endDate, localityName);
        return new GrpcReport();
        //var allDoneApps = _dbContext.Applications.Select(o => o.Map()).ToList(); //apprep.GetAllDoneApps
        //var response = _dbContext.Organizations.Select(o => o.Map()).ToList();

        //var result = new OrganizationArray();
        //result.List.AddRange(response);

        //return Task.FromResult(result);
    }
}
