using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using static PIS_GrpcService.PIS_GrpcService.GrpcAnimalService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PisWebApp.Controllers
{
    public class AnimalController : Controller
    {
        private readonly GrpcAnimalServiceClient _grpcClient;

        public AnimalController(GrpcAnimalServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        // GET: AnimalController
        public async Task<IActionResult> Index()
        {
            var animals = await _grpcClient.GetAllAsync(new Empty());

            return View(animals);
        }

        // GET: AnimalController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var animal = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(animal);
        }

        // GET: AnimalController/Create
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GrpcAnimal animal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Отправляем данные новой организации через gRPC клиент
                    var response = await _grpcClient.AddAsync(animal);

                    // Редирект на страницу списка организаций после успешного создания
                    return RedirectToAction(nameof(Index));
                }
                catch (RpcException ex)
                {
                    // Обработка ошибки при вызове gRPC сервиса
                    // Логирование или другие действия по обработке ошибки
                    ModelState.AddModelError(string.Empty, "Ошибка создания животного через gRPC.");
                }
            }
            // Если что-то пошло не так, вернем пользователя на форму создания
            return RedirectToAction(nameof(Index));
        }

        // GET: AnimalController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(animal);
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrpcAnimal animal)
        {
            await _grpcClient.EditAsync(animal);
            await _grpcClient.GetAsync(new IdRequest { Id = animal.Id });

            return RedirectToAction("Index", "Animal");
        }

        // GET: AnimalController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _grpcClient.DeleteAsync(new IdRequest { Id = id });

            return RedirectToAction("Index", "Animal");
        }

        // POST: AnimalController/Delete/5
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
