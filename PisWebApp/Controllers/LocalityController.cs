using Microsoft.AspNetCore.Mvc;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityService;

namespace PisWebApp.Controllers;

public class LocalityController : Controller
{
    private readonly GrpcLocalityServiceClient _grpcClient;
    public LocalityController(GrpcLocalityServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    //GET: LocalityController
    public async Task<IActionResult> Index()
    {
        var animals = await _grpcClient.GetAllAsync(new Empty());

        return View(animals);
    }
}

