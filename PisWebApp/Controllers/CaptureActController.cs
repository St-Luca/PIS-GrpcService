using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

/*namespace PisWebApp.Controllers
{
    public class CaptureActController : Controller
    {
        private readonly GrpcCaptureActServiceClient _grpcClient;
        public CaptureActController(GrpcCaptureActServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        // GET: CaptureActController
        public async Task<IActionResult> Index()
        {
            var acts = await _grpcClient.GetAllAsync(new Empty());

            return View(acts);
        }

        // GET: CaptureActController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var act = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(act);
        }

        // GET: CaptureActController/Create
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // POST: CaptureActController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GrpcCaptureAct act)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Отправляем данные новой организации через gRPC клиент
                    var response = await _grpcClient.AddAsync(act);

                    // Редирект на страницу списка организаций после успешного создания
                    return RedirectToAction(nameof(Index));
                }
                catch (RpcException ex)
                {
                    // Обработка ошибки при вызове gRPC сервиса
                    // Логирование или другие действия по обработке ошибки
                    ModelState.AddModelError(string.Empty, "Ошибка создания акта через gRPC.");
                }
            }
            // Если что-то пошло не так, вернем пользователя на форму создания
            return RedirectToAction(nameof(Index));
        }

        // GET: CaptureActController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var act = await _grpcClient.GetAsync(new IdRequest { Id = id });

            return View(act);
        }

        // POST: CaptureActController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GrpcCaptureAct act)
        {
            await _grpcClient.EditAsync(act);
            await _grpcClient.GetAsync(new IdRequest { Id = act.Id });

            return RedirectToAction("Index", "CaptureAct");
        }

        // GET: CaptureActController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _grpcClient.DeleteAsync(new IdRequest { Id = id });

            return RedirectToAction("Index", "CaptureAct");
        }

        // POST: CaptureActController/Delete/5
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
}*/