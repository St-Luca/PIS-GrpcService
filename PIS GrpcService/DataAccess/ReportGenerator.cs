using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using System.Data;

namespace PIS_GrpcService.DataAccess;

public class ReportGenerator
{
    private readonly CaptureActsRepository actsRepository;
    private readonly CatchingApplicationsRepository applicationsRepository;

    public ReportGenerator(
        CaptureActsRepository actRepository,
        CatchingApplicationsRepository applicationsRepository)
    {
        actsRepository = actRepository;
        this.applicationsRepository = applicationsRepository;
    }

    public GrpcReport GenerateAppsPercentReport(DateTime startDate, DateTime endDate, int localityName)
    {
        var allAppsCount = applicationsRepository.GetAllAppsInPeriodCount(startDate, endDate, localityName);
        var doneAppsCount = actsRepository.GetDoneAppsInPeriodCount(startDate, endDate, localityName);

        return GenerateAppsPercentReport(startDate, endDate, allAppsCount, doneAppsCount, localityName);
    }

    public GrpcReport GenerateAppsPercentReport(DateTime startDate, DateTime endDate, int allAppsCount, int doneAppsCount, int localityName)
    {
        decimal percentage = 0;

        if (allAppsCount != 0)
        {
            percentage = ((decimal)doneAppsCount / allAppsCount) * 100;
        }

        return new GrpcReport
        {
            Number = 1,
            Name = "Отчет по проценту выполненных заявок в населенном пункте",
            Description = $"За период с {startDate.Date} по {endDate.Date} в нас. пункте {localityName} " +
              $"было зарегистрировано {allAppsCount} заявок, выполнено - {doneAppsCount}. Процент выполнения составил: {percentage}"
        };
    }

}
