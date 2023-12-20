//using Microsoft.AspNetCore.Mvc;
//using static PIS_GrpcService.PIS_GrpcService.GrpcOrganizationService;

//namespace PisWebApp.Controllers;

//public class ApplicationController : Controller
//{
//    private readonly GrpcApplicationServiceClient _grpcClient;
//    public ApplicationController(GrpcApplicationServiceClient grpcClient)
//    {
//        _grpcClient = grpcClient;
//    }

//    // GET: ApplicationController
//    public async Task<IActionResult> Index()
//    {
//        var apps = await _grpcClient.GetAllAsync(new Empty());

//        return View(apps);
//    }
//}
