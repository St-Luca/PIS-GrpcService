﻿using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcApplicationService;
using static PIS_GrpcService.PIS_GrpcService.GrpcOrganizationService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PisWebApp.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly GrpcApplicationServiceClient _grpcClient;
        private readonly GrpcOrganizationServiceClient _grpcOrganizationClient;
        private readonly GrpcLocalityServiceClient _grpcLocalityClient;

        public ApplicationController(GrpcApplicationServiceClient grpcClient, GrpcOrganizationServiceClient grpcOrganizationServiceClient, GrpcLocalityServiceClient grpcLocalityServiceClient)
        {
            _grpcClient = grpcClient;
            _grpcOrganizationClient = grpcOrganizationServiceClient;
            _grpcLocalityClient = grpcLocalityServiceClient;
        }

        // GET: ApplicationController
        public async Task<IActionResult> Index()
        {
            var applications = await _grpcClient.GetAllAsync(new Empty());

            return View(applications);
        }

        // GET: ApplicationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var application = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(application);
        }

        // GET: ApplicationController/Create
        public async Task<IActionResult> Add()
        {
            var organizations = await _grpcOrganizationClient.GetAllAsync(new Empty());
            var localities = await _grpcLocalityClient.GetAllAsync(new Empty());

            ViewBag.orgId = new SelectList(organizations.List, "Id", "Id");
            ViewBag.locId = new SelectList(localities.List, "Id", "Id");

            return View();
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GrpcApplication application)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Отправляем данные новой организации через gRPC клиент
                    var response = await _grpcClient.AddAsync(application);

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

        // GET: ApplicationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var application = await _grpcClient.GetAsync(new IdRequest { Id = id });
            var organizations = await _grpcOrganizationClient.GetAllAsync(new Empty());
            var localities = await _grpcLocalityClient.GetAllAsync(new Empty());

            ViewBag.orgId = new SelectList(organizations.List, "Id", "Id");
            ViewBag.locId = new SelectList(localities.List, "Id", "Id");

            return View(application);
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrpcApplication application)
        {
            await _grpcClient.EditAsync(application);
            await _grpcClient.GetAsync(new IdRequest { Id = application.Id });

            return RedirectToAction("Index", "Application");
        }

        // GET: ApplicationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _grpcClient.DeleteAsync(new IdRequest { Id = id });

            return RedirectToAction("Index", "Application");
        }

        // POST: ApplicationController/Delete/5
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
