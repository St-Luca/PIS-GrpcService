using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcReportService;

namespace PisWebApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly GrpcReportServiceClient _grpcClient;
        public ReportController(GrpcReportServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }
    
        [Route("Report/AppsPersentReport")]
        public async Task<IActionResult> GenerateAppsPersentReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string localityName)
        {
            var report = await _grpcClient.GenerateAppsPercentReportAsync(new ReportRequest { StartDate = Timestamp.FromDateTime(startDate), EndDate = Timestamp.FromDateTime(endDate), TypeName = localityName });

            return View("Details", report);
        }

        [Route("Report/ClosedAppsReport")]
        public async Task<IActionResult> GenerateClosedAppsReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string localityName)
        {
            var report = await _grpcClient.GenerateClosedAppsReportAsync(new ReportRequest { StartDate = Timestamp.FromDateTime(startDate), EndDate = Timestamp.FromDateTime(endDate), TypeName = localityName });

            return View("Details", report);
        }

        [Route("Report/ClosedContractsSumReport")]
        public async Task<IActionResult> GenerateClosedContractsSumReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string orgName)
        {
            var report = await _grpcClient.GenerateClosedContractsSumReportAsync(new ReportRequest { StartDate = Timestamp.FromDateTime(startDate), EndDate = Timestamp.FromDateTime(endDate), TypeName = orgName });

            return View("Details", report);
        }
    }
}
