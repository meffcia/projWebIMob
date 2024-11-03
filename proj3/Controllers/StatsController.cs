using Microsoft.AspNetCore.Mvc;
using proj3.Models;

namespace proj3.Controllers
{
    public class StatsController : Controller
    {
        private readonly StatsModel _model;

        public StatsController()
        {
            _model = new StatsModel();
        }

        public IActionResult Index()
        {
            var totalQuantity = _model.GetTotalQuantity();
            var quantityByBook = _model.GetQuantityByBook();
            var monthlyStats = _model.GetMonthlyStats();

            // Zorganizujemy dane miesięczne według roku
            var monthlyStatsByYear = new Dictionary<int, Dictionary<int, int>>(); // Rok -> Miesiąc -> Ilość

            foreach (var entry in _model.Entries)
            {
                if (!monthlyStatsByYear.ContainsKey(entry.Year))
                {
                    monthlyStatsByYear[entry.Year] = new Dictionary<int, int>();
                }

                if (monthlyStatsByYear[entry.Year].ContainsKey(entry.Month))
                {
                    monthlyStatsByYear[entry.Year][entry.Month] += entry.Quantity;
                }
                else
                {
                    monthlyStatsByYear[entry.Year][entry.Month] = entry.Quantity;
                }
            }

            // Dodajemy obliczenia dla tabeli
            var salesSummary = _model.GetSalesSummary();

            ViewData["QuantityByBook"] = quantityByBook;
            ViewData["MonthlyStatsByYear"] = monthlyStatsByYear;
            ViewData["SalesSummary"] = salesSummary;

            var uniqueYears = salesSummary.SelectMany(b => b.Value.Keys).Distinct().ToList();
            ViewData["UniqueYears"] = uniqueYears;


            return View(new { TotalQuantity = totalQuantity, QuantityByBook = quantityByBook });
        }



    }
}
