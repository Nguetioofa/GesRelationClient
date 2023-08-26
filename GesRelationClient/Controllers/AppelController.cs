using GesRelationClient.Models;
using GesRelationClient.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace GesRelationClient.Controllers
{
    public class AppelController : Controller
    {
        IRepositorie<Appel> _repositorie;
        public AppelController(IRepositorie<Appel> repositorie)
        {
            _repositorie = repositorie;
        }


        // GET: AppelController
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var apples = await _repositorie.GetPaged(page, pageSize);
            var totalAppel = await _repositorie.GetTotal();
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AppelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: AppelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
