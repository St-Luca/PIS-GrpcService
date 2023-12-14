using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcOrganizationService;
//using PIS_GrpcService.PisWebApp;
//using static PIS_GrpcService.Organizationer;
//using static PIS_GrpcService.PisWebApp.Organizationer;

namespace PisWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly GrpcOrganizationServiceClient _grpcClient;
        public OrganizationController(GrpcOrganizationServiceClient grpcClient)
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
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // POST: OrganizationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GrpcOrganization organization)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Отправляем данные новой организации через gRPC клиент
                    var response = await _grpcClient.AddAsync(organization);

                    // Редирект на страницу списка организаций после успешного создания
                    return RedirectToAction(nameof(Index));
                }
                catch (RpcException ex)
                {
                    // Обработка ошибки при вызове gRPC сервиса
                    // Логирование или другие действия по обработке ошибки
                    ModelState.AddModelError(string.Empty, "Ошибка создания организации через gRPC.");
                }
            }
            // Если что-то пошло не так, вернем пользователя на форму создания
            return RedirectToAction(nameof(Index));
        }

        // GET: OrganizationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var organization = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(organization);
        }

        // POST: OrganizationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrpcOrganization organization)
        {
            await _grpcClient.EditAsync(organization);
            await _grpcClient.GetAsync(new IdRequest { Id = organization.Id });

            return RedirectToAction("Index", "Organization");
        }

        // GET: OrganizationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _grpcClient.DeleteAsync(new IdRequest { Id = id });

            return RedirectToAction("Index", "Organization");
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
