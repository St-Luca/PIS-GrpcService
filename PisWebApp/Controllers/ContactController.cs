﻿using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcContractService;

namespace PisWebApp.Controllers
{
    public class ContractController : Controller
    {
        private readonly GrpcContractServiceClient _grpcClient;
        public ContractController(GrpcContractServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
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

            return View(contract);
        }

        // GET: ContractController/Create
        public async Task<IActionResult> Add()
        {
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
            var contract = await _grpcClient.GetAsync(new IdRequest { Id = id });

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