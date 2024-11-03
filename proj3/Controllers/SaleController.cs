using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using proj3.Models; // Upewnij się, że ta przestrzeń nazw jest poprawna
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class SalesController : Controller
{
    private const string SalesFilePath = "data/sales.json"; // Ścieżka do pliku sprzedaży
    private const string BooksFilePath = "data/books.json"; // Ścieżka do pliku książek

    public IActionResult Index()
    {
        var sales = GetSales();
        return View(sales);
    }

    public IActionResult Create()
    {
        var books = GetBooks(); // Pobierz książki
        ViewBag.Books = books;
        ViewBag.Months = Enumerable.Range(1, 12).Select(m => new SelectListItem
        {
            Value = m.ToString(),
            Text = m.ToString()
        });
        ViewBag.Years = Enumerable.Range(2020, 5).Select(y => new SelectListItem
        {
            Value = y.ToString(),
            Text = y.ToString()
        });
        return View();
    }

    [HttpPost]
    public IActionResult Create(SaleModel sale)
    {
        if (ModelState.IsValid)
        {
            var sales = GetSales();

            // Sprawdź, czy sprzedaż już istnieje dla danego tytułu, miesiąca i roku
            var existingSale = sales.FirstOrDefault(s =>
                s.BookTitle == sale.BookTitle &&
                s.Month == sale.Month &&
                s.Year == sale.Year);

            if (existingSale != null)
            {
                // Jeśli sprzedaż istnieje, zaktualizuj ilość
                existingSale.Quantity += sale.Quantity;
            }
            else
            {
                // Jeśli sprzedaż nie istnieje, dodaj nowy wpis
                sale.Id = sales.Count > 0 ? sales.Max(s => s.Id) + 1 : 1; // Generowanie unikalnego Id
                sales.Add(sale);
            }

            SaveSales(sales);
            return RedirectToAction("Index");
        }

        return View(sale);
    }

    public IActionResult Edit(int id)
    {
        var sales = GetSales();
        var sale = sales.FirstOrDefault(s => s.Id == id);
        if (sale == null)
        {
            return NotFound();
        }

        var books = GetBooks(); // Pobierz książki
        ViewBag.Books = books;
        ViewBag.Months = Enumerable.Range(1, 12).Select(m => new SelectListItem
        {
            Value = m.ToString(),
            Text = m.ToString()
        });
        ViewBag.Years = Enumerable.Range(2020, 5).Select(y => new SelectListItem
        {
            Value = y.ToString(),
            Text = y.ToString()
        });

        return View(sale);
    }

    [HttpPost]
    public IActionResult Edit(SaleModel sale)
    {
        if (ModelState.IsValid)
        {
            var sales = GetSales();
            var existingSale = sales.FirstOrDefault(s => s.Id == sale.Id);
            if (existingSale != null)
            {
                existingSale.BookTitle = sale.BookTitle;
                existingSale.Month = sale.Month;
                existingSale.Year = sale.Year;
                existingSale.Quantity = sale.Quantity;

                SaveSales(sales);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        return View(sale);
    }

    public IActionResult Delete(int id)
    {
        var sales = GetSales();
        var sale = sales.FirstOrDefault(s => s.Id == id);
        if (sale == null)
        {
            return NotFound();
        }
        return View(sale);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var sales = GetSales();
        var saleToRemove = sales.FirstOrDefault(s => s.Id == id);
        if (saleToRemove != null)
        {
            sales.Remove(saleToRemove);
            SaveSales(sales);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    private List<SaleModel> GetSales()
    {
        if (!System.IO.File.Exists(SalesFilePath))
        {
            return new List<SaleModel>();
        }

        var jsonData = System.IO.File.ReadAllText(SalesFilePath);
        return JsonConvert.DeserializeObject<List<SaleModel>>(jsonData) ?? new List<SaleModel>();
    }

    private void SaveSales(List<SaleModel> sales)
    {
        var jsonData = JsonConvert.SerializeObject(sales, Formatting.Indented);
        System.IO.File.WriteAllText(SalesFilePath, jsonData);
    }

    private List<BookModel> GetBooks()
    {
        if (!System.IO.File.Exists(BooksFilePath))
        {
            return new List<BookModel>();
        }

        var jsonData = System.IO.File.ReadAllText(BooksFilePath);
        return JsonConvert.DeserializeObject<List<BookModel>>(jsonData) ?? new List<BookModel>();
    }
}
