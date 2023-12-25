using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
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

        // GET: LocalityController/Create
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // POST: LocalityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GrpcLocality locality)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Отправляем данные новой организации через gRPC клиент
                    var response = await _grpcClient.AddAsync(locality);

                    // Редирект на страницу списка организаций после успешного создания
                    return RedirectToAction(nameof(Index));
                }
                catch (RpcException ex)
                {
                    // Обработка ошибки при вызове gRPC сервиса
                    // Логирование или другие действия по обработке ошибки
                    ModelState.AddModelError(string.Empty, "Ошибка создания населенного пункта через gRPC.");
                }
            }
            // Если что-то пошло не так, вернем пользователя на форму создания
            return RedirectToAction(nameof(Index));
        }

        // GET: LocalityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var locality = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(locality);
        }

        // POST: LocalityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrpcLocality locality)
        {
            await _grpcClient.EditAsync(locality);
            await _grpcClient.GetAsync(new IdRequest { Id = locality.Id });

            return RedirectToAction("Index", "Locality");
        }

        // GET: LocalityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _grpcClient.DeleteAsync(new IdRequest { Id = id });

            return RedirectToAction("Index", "Locality");
        }

        // POST: LocalityController/Delete/5
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