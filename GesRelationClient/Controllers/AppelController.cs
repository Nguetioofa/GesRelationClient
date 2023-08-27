using GesRelationClient.Models;
using GesRelationClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GesRelationClient.Controllers
{
    [Authorize]
    public class AppelController : Controller
    {
        IRepositorie<Appel> _appelService;
        public AppelController(IRepositorie<Appel> appelService)
        {
            _appelService = appelService;
        }


        // GET: AppelController
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var apples = await _appelService.GetPaged(page, pageSize);
            var totalAppel = await _appelService.GetTotal();
            var totalPages = (int)Math.Ceiling((double)totalAppel / pageSize);

            var model = new AppelListPagedViewModel
            {
                apples = apples,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }


        // GET: AppelController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var appel = await _appelService.GetById(id);

            return View(appel);
        }
        // GET: AppelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appel appel)
        {
            if (ModelState.IsValid)
            {
                var result = await _appelService.Create(appel);
                if (result)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.ErrorMessage = "Echec";
                    return View(appel);
                }
            }

            ViewBag.ErrorMessage = "Veuillez remplir tous les champs.";
            return View(appel);
        }

        // GET: AppelController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appel = await _appelService.GetById(id);

            return View(appel);
        }

        // POST: AppelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appel appel)
        {
            if (ModelState.IsValid)
            {
                var result = await _appelService.Update(appel);
                if (result)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.ErrorMessage = "Echec";
                    return View(appel);
                }
            }

            ViewBag.ErrorMessage = "Veuillez remplir tous les champs.";
            return View(appel);
        }

        // GET: AppelController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appel = await _appelService.GetById(id);

            return View(appel);
        }

        // POST: AppelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
