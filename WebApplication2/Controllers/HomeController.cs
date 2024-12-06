using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using BusinessLogic.Business;
using DTO.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private VirksomhedBLL _virksomhedBll = new VirksomhedBLL();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(IFormCollection formData)
    {
        string afdeling = formData["Afdelinger"];
        int afdelingId = int.TryParse(afdeling, out afdelingId) ? afdelingId : 0;
        FyldAfdelinger();
        FyldSager(afdelingId);
        PopulateDropdownLists();
        MedarbejderUgeIndex();
        if (formData != null && formData.ContainsKey("AfdelingValg"))
        {
            string valgtAfdeling = formData["Afdelinger"];
            ViewBag.SelectedAfdeling = valgtAfdeling;
        }

        return View();
    }

    public IActionResult Oversigt()
    {
        
        return RedirectToAction("MedarbejderUgeIndex");
        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public void FyldAfdelinger()
    {
        ViewBag.Afdelingerne = new List<SelectListItem>
        {
            new SelectListItem { Text = "Afdeling Salg", Value = "1" },
            new SelectListItem { Text = "Afdeling Support", Value="2"},
            new SelectListItem { Text = "Afdeling Udvikling", Value = "3"}

        };
    }

    public void FyldMedarbejder(int afdelingsId)
    {
        ViewBag.Medarbejder = _virksomhedBll.GetMedarbejdere()
            .Where(m => m.AfdelingsId == afdelingsId)
            .Select(m => new SelectListItem
            {
                Value = m.AfdelingsId.ToString(),
                Text = m.Navn
            });
    }

    public void FyldSager(int afdelingId)
    {
        ViewBag.Sager = _virksomhedBll.GetSager()
            .Where(s => s.AfdelingsId == afdelingId)
            .Select(s => new SelectListItem
            {
                Value = s.AfdelingsId.ToString(),
                Text = s.Overskrift
            });
    }
    private void PopulateDropdownLists()
    {
        var afdelinger = _virksomhedBll.GetAfdelinger()
            .Select(a => new SelectListItem { Value = a.AfdelingsId.ToString(), Text = a.Navn })
            .ToList();
        var medarbejdere = _virksomhedBll.GetMedarbejdere()
            .Select(m => new SelectListItem() { Value = m.MedarbejderId.ToString(), Text = m.Navn })
            .ToList();

        var sager = _virksomhedBll.GetSager()
            .Select(s => new SelectListItem(){ Value = s.SagsNummer.ToString(), Text = s.Overskrift })
            .ToList();

        if (!afdelinger.Any())
            _logger.LogWarning("Afdelings listen er tom");
        if (!medarbejdere.Any())
            _logger.LogWarning("Medarbejder listen er tom");
        if (!sager.Any())
            _logger.LogWarning("Sags listen er tom");

        ViewBag.Afdelinger = afdelinger;
        ViewBag.Medarbejdere = medarbejdere;
        ViewBag.Sager = sager;
    }
    [HttpPost]
    public IActionResult OpretTid(IFormCollection formData)
    {
        PopulateDropdownLists();

        if (!int.TryParse(formData["SelectedAfdeling"], out int afdelingId) ||
            !int.TryParse(formData["Medarbejdere"], out int medarbejderId) ||
            !int.TryParse(formData["Sager"], out int sagId))
        {
            ModelState.AddModelError("", "Du skal vælge både afdeling, medarbejder og sag.");
            return View();
        }

        if (!DateTime.TryParse(formData["startDate"], out DateTime startDato) ||
            !DateTime.TryParse(formData["endDate"], out DateTime slutDato))
        {
            ModelState.AddModelError("", "Ugyldige datoer.");
            return View();
        }

        if (slutDato <= startDato)
        {
            ModelState.AddModelError("", "Slutdato skal være efter startdato.");
            return View();
        }

        var tidsregistrering = new TidsregistreringDTO
        {
            MedarbejderId = medarbejderId,
            StartTid = startDato,
            SlutTid = slutDato,
            SagId = sagId
        };

        _virksomhedBll.OpretTidsregistrering(tidsregistrering);

        TempData["SuccessMessage"] = "Tidsregistrering oprettet!";
        return RedirectToAction("Index");
    }
    public class MedarbejderUgeTimerViewModel
    {
        public string Navn { get; set; }
        public List<UgeTimerViewModel> UgeTimer { get; set; }
    }

    public class UgeTimerViewModel
    {
        public int Uge { get; set; }
        public double Timer { get; set; }
    }
    public IActionResult MedarbejderUgeIndex()
    {
        var medarbejder = _virksomhedBll.GetMedarbejdere();
        var tidsregistrering = _virksomhedBll.GetTidsregistreringer();

        var medarbejderUgeTimer = medarbejder.Select(m => new MedarbejderUgeTimerViewModel
        {
            Navn = m.Navn,
            UgeTimer = tidsregistrering.Where(t => t.MedarbejderId == m.MedarbejderId)
                .GroupBy(t => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    t.StartTid, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                .Select(g => new UgeTimerViewModel
                {
                    Uge = g.Key,
                    Timer = g.Sum(t => (t.SlutTid - t.StartTid).TotalHours)
                }).ToList()
        }).ToList();

        _logger.LogInformation("MedarbejderUgeTimer: {@medarbejderUgeTimer}", medarbejderUgeTimer);

        return View("Oversigt", medarbejderUgeTimer);

    }
}