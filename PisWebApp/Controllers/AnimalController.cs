using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcAnimalService;

namespace PisWebApp.Controllers
{
    public class AnimalController : Controller
    {
        private readonly GrpcAnimalServiceClient _grpcClient;
        public AnimalController(GrpcAnimalServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        //GET: AnimalController
        public async Task<IActionResult> Index()
        {
            var animals = await _grpcClient.GetAllAsync(new Empty());

            return View(animals);
        }
    }
}
