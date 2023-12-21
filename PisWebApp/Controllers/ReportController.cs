﻿using Google.Protobuf.WellKnownTypes;
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
    
        [Route("Report/GenerateAppsPersentReport")]
        public async Task<IActionResult> GenerateAppsPersentReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string localityName)
        {
            var report = await _grpcClient.GenerateAppsPercentReportAsync(new ReportRequest { StartDate = Timestamp.FromDateTime(startDate.ToUniversalTime()), EndDate = Timestamp.FromDateTime(endDate.ToUniversalTime()), TypeName = localityName });

            return View("Details", report);
        }

        [Route("Report/GenerateClosedAppsReport")]
        public async Task<IActionResult> GenerateClosedAppsReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string localityName)
        {
            var report = await _grpcClient.GenerateClosedAppsReportAsync(new ReportRequest { StartDate = Timestamp.FromDateTime(startDate.ToUniversalTime()), EndDate = Timestamp.FromDateTime(endDate.ToUniversalTime()), TypeName = localityName });

            return View("Details", report);
        }

        [Route("Report/GenerateClosedContractsSumReport")]
        public async Task<IActionResult> GenerateClosedContractsSumReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string orgName)
        {
            var report = await _grpcClient.GenerateClosedContractsSumReportAsync(new ReportRequest { StartDate = Timestamp.FromDateTime(startDate.ToUniversalTime()), EndDate = Timestamp.FromDateTime(endDate.ToUniversalTime()), TypeName = orgName });

            return View("Details", report);
        }
    }
}
