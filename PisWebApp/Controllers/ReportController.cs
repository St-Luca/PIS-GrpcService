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
    
        [Route("ReportGenerator/GenerateAppsPersentReport")]
        public async Task<IActionResult> GenerateAppsPersentReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int localityId)
        {
            var report = await _grpcClient.GenerateAppsPercentReportAsync(
                new ReportRequest { 
                    StartDate = Timestamp.FromDateTime(startDate.ToUniversalTime()), EndDate = Timestamp.FromDateTime(endDate.ToUniversalTime()), TypeId = localityId });

            return View("Details", report);
        }

        [Route("ReportGenerator/GenerateClosedAppsReport")]
        public async Task<IActionResult> GenerateClosedAppsReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int localityId)
        {
            var report = await _grpcClient.GenerateClosedAppsReportAsync(
                new ReportRequest { 
                    StartDate = Timestamp.FromDateTime(startDate.ToUniversalTime()), EndDate = Timestamp.FromDateTime(endDate.ToUniversalTime()), TypeId = localityId });

            return View("Details", report);
        }

        [Route("ReportGenerator/MakeClosedContractsReport")]
        public async Task<IActionResult> MakeClosedContractsReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string orgName)
        {
            var report = await _grpcClient.MakeClosedContractsReportAsync(
                new ReportRequest { 
                    StartDate = Timestamp.FromDateTime(startDate.ToUniversalTime()), EndDate = Timestamp.FromDateTime(endDate.ToUniversalTime()), OrganizationName = orgName });

            return View("Details", report);
        }
    }
}
