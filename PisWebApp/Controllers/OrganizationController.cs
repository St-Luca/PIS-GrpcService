using Microsoft.AspNetCore.Mvc;
using PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.Organizationer;
//using PIS_GrpcService.PisWebApp;
//using static PIS_GrpcService.Organizationer;
//using static PIS_GrpcService.PisWebApp.Organizationer;

namespace PisWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly OrganizationerClient _grpcClient;
        public OrganizationController(OrganizationerClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        // GET: OrganizationController
        public async Task<IActionResult> Index()
        {
            var organizations = await _grpcClient.GetAllAsync(new Empty());

            return View(organizations);
        }

        // GET: OrganizationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var organization = await _grpcClient.GetAsync(new IdRequest { Id = id});

            return View(organization);
        }

        // GET: OrganizationController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: OrganizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: OrganizationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrganizationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: OrganizationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
