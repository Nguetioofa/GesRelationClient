using GesRelationClient.Models;
using GesRelationClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;

namespace GesRelationClient.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        IRepositorie<Client> _clientService;

        public ClientController(IRepositorie<Client> repositorie)
        {
            _clientService = repositorie;
        }
        // GET: ClientController
        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var clients = await _clientService.GetPaged(page, pageSize);
            var totalClients = await _clientService.GetTotal();
            var totalPages = (int)Math.Ceiling((double)totalClients / pageSize);

            var model = new ClientListPagedViewModel
            {
                Clients = clients,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }


        // GET: ClientController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = await _clientService.GetById(id);

            return View(client);
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                var result = await _clientService.Create(client);
                if (result)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.ErrorMessage = "Echec";
                    return View(client);
                }
            }

            ViewBag.ErrorMessage = "Veuillez remplir tous les champs.";
            return View(client);

        }

        // GET: ClientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientService.GetById(id);

            return View(client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                var result = await _clientService.Update(client);
                if (result)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.ErrorMessage = "Echec";
                    return View(client);
                }
            }

            ViewBag.ErrorMessage = "Veuillez remplir tous les champs.";
            return View(client);
        }

        // GET: ClientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientService.GetById(id);

            return View(client);
        }

        // POST: ClientController/Delete/5
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
