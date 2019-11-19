using App2.Models;
using App2.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInterestsRequest _request;

        public HomeController(IInterestsRequest request)
        {
            _request = request;
        }

        public async Task<IActionResult> Index(InterestsViewModel model)
        {
            if (model.ValorJuros == 0)
            {
                var response = await _request.GetInterestRateAsync();
                model.TaxaJuros = response * 100;
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetInterests(double valorInicial, int meses, double taxaJuros = 0)
        {
            var response = await _request.GetInterestAmountAsync(valorInicial, meses);
            var model = new InterestsViewModel
            {
                ValorInicial = valorInicial,
                Meses = meses,
                ValorJuros = response,
                TaxaJuros = taxaJuros
            };
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}