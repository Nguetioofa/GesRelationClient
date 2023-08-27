using FastReport;
using FastReport.Export.PdfSimple;
using GesRelationClient.Models;
using GesRelationClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GesRelationClient.Controllers
{
    [Authorize("ManagerOnly")]
    public class RapportController : Controller
    {
        private readonly IRepositorie<Client> _clientSetvice;
        private readonly IRepositorie<Appel> _appelService;
        private readonly IWebHostEnvironment _environment;


        public RapportController(IRepositorie<Client> clientSetvice, IRepositorie<Appel> appelService, IWebHostEnvironment environment)
        {
            _clientSetvice = clientSetvice;
            _appelService = appelService;
            _environment = environment; 
        }
        // GET: RapportController
        public ActionResult Index()
        {
            return View();
        }

        public async  Task<FileResult> Generate()
        {
            FastReport.Utils.Config.WebMode = true;
            Report report = new Report();
            string path = _environment.WebRootPath + "/reports/Rapport.frx";
            report.Load(path);

            var AppelList = await _appelService.GetAll();
            var ClientList = await _clientSetvice.GetAll();
            report.RegisterData(AppelList, "AppelListRef");
            report.RegisterData(ClientList, "ClientListRef");
            if (report.Report.Prepare())
            {
                PDFSimpleExport pDFSimpleExport = new PDFSimpleExport();
                pDFSimpleExport.ShowProgress = false;
                pDFSimpleExport.Subject = "Rapport";
                pDFSimpleExport.Title = "rapport";
                var ms = new MemoryStream();
                report.Report.Export(pDFSimpleExport, ms);
                report.Dispose();
                pDFSimpleExport.Dispose();
                ms.Position = 0;
                return File(ms.ToArray(),"application/pdf", $"rapport{DateTime.Now.ToString("u")}.pdf");
            }
            else
            {
                return null;
            }
        }

    }
}
