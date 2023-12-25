using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using System.Diagnostics.Contracts;
using static PIS_GrpcService.PIS_GrpcService.GrpcContractService;
using static PIS_GrpcService.PIS_GrpcService.GrpcOrganizationService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityCostService;

namespace PisWebApp.Controllers
{
    public class ContractController : Controller
    {
        private readonly GrpcContractServiceClient _grpcClient;
        private readonly GrpcOrganizationServiceClient _organizationClient;
        private readonly GrpcLocalityServiceClient _localityCleint;
        private readonly GrpcLocalityServiceClient _localityCostCleint;

        public ContractController(GrpcContractServiceClient grpcClient, GrpcOrganizationServiceClient grpcOrganizationServiceClient, GrpcLocalityServiceClient grpcLocalityServiceClient, GrpcLocalityServiceClient localityCostCleint)
        {
            _grpcClient = grpcClient;
            _organizationClient = grpcOrganizationServiceClient;
            _localityCleint = grpcLocalityServiceClient;
            _localityCostCleint = localityCostCleint;
        }

        // GET: ContractController
        public async Task<IActionResult> Index()
        {
            var contracts = await _grpcClient.GetAllAsync(new Empty());

            return View(contracts);
        }

        // GET: ContractController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contract = await _grpcClient.GetAsync(new IdRequest { Id = id });
            //var organizations = await _organizationClient.GetAllAsync(new Empty());
            //var localities = await _localityCleint.GetAllAsync(new Empty());
            //var locCost = await _localityCostCleint.GetAllAsync(new Empty());

            return View(contract);
        }

        // GET: ContractController/Create
        public async Task<IActionResult> Add()
        {
            var organizations = await _organizationClient.GetAllAsync(new Empty());
            var locCost = await _localityCostCleint.GetAllAsync(new Empty());

            ViewBag.orgId = new SelectList(organizations.List,"Id", "Id");
            ViewBag.locCostId = new SelectList(locCost.List, "Id", "Id");
            
            
            return View();
        }

        // POST: ContractController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GrpcContract contract)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Отправляем данные новой организации через gRPC клиент
                    var response = await _grpcClient.AddAsync(contract);

                    // Редирект на страницу списка организаций после успешного создания
                    return RedirectToAction(nameof(Index));
                }
                catch (RpcException ex)
                {
                    // Обработка ошибки при вызове gRPC сервиса
                    // Логирование или другие действия по обработке ошибки
                    ModelState.AddModelError(string.Empty, "Ошибка создания контракта через gRPC.");
                }
            }
            // Если что-то пошло не так, вернем пользователя на форму создания
            return RedirectToAction(nameof(Index));
        }

        // GET: ContractController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var contract = await _grpcClient.GetAsync(new IdRequest { Id = id });
            var organizations = await _organizationClient.GetAllAsync(new Empty());
            var localities = await _localityCleint.GetAllAsync(new Empty());

            if (contract == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.orgId = new SelectList(organizations.List, "Id", "Id");
            ViewBag.locId = new SelectList(localities.List, "Id", "Id");

            return View(contract);
        }

        // POST: ContractController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrpcContract contract)
        {
            await _grpcClient.EditAsync(contract);
            await _grpcClient.GetAsync(new IdRequest { Id = contract.Id });

            return RedirectToAction("Index", "Contract");
        }

        // GET: ContractController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _grpcClient.DeleteAsync(new IdRequest { Id = id });

            return RedirectToAction("Index", "Contract");
        }

        // POST: ContractController/Delete/5
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