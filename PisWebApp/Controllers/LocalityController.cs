
using Microsoft.AspNetCore.Mvc;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityService;


namespace PisWebApp.Controllers
{
    public class LocalityController : Controller
    {
        private readonly GrpcLocalityServiceClient _grpcClient;
        public LocalityController(GrpcLocalityServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        // GET: LocalityController
        public async Task<IActionResult> Index()
        {
            var localities = await _grpcClient.GetAllAsync(new Empty());

            return View(localities);
        }

        // GET: LocalityController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var locality = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(locality);
        }
    }
}