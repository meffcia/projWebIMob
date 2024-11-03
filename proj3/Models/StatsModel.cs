using System;
using System.Collections.Generic;

namespace proj3.Models
{
    public class StatsEntry
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
    }

    public class StatsModel
    {
        public List<StatsEntry> Entries { get; set; }

        public StatsModel()
        {
            Entries = LoadData();
        }

        private List<StatsEntry> LoadData()
        {
            var json = System.IO.File.ReadAllText("data/sales.json");
            return System.Text.Json.JsonSerializer.Deserialize<List<StatsEntry>>(json);
        }

        public int GetTotalQuantity()
        {
            int total = 0;
            foreach (var entry in Entries)
            {
                total += entry.Quantity;
            }
            return total;
        }

        public Dictionary<string, int> GetQuantityByBook()
        {
            var quantities = new Dictionary<string, int>();

            foreach (var entry in Entries)
            {
                if (quantities.ContainsKey(entry.BookTitle))
                {
                    quantities[entry.BookTitle] += entry.Quantity;
                }
                else
                {
                    quantities[entry.BookTitle] = entry.Quantity;
                }
            }

            return quantities;
        }

        public Dictionary<string, int> GetMonthlyStats()
        {
            var monthlyStats = new Dictionary<string, int>();

            foreach (var entry in Entries)
            {
                string key = $"{entry.Month}-{entry.Year}"; // Zmiana formatu na "Miesiąc-Rok"
                if (monthlyStats.ContainsKey(key))
                {
                    monthlyStats[key] += entry.Quantity;
                }
                else
                {
                    monthlyStats[key] = entry.Quantity;
                }
            }

            return monthlyStats;
        }

        public Dictionary<string, Dictionary<int, int>> GetSalesSummary()
        {
            var salesSummary = new Dictionary<string, Dictionary<int, int>>();

            foreach (var entry in Entries)
            {
                if (!salesSummary.ContainsKey(entry.BookTitle))
                {
                    salesSummary[entry.BookTitle] = new Dictionary<int, int>();
                }

                if (salesSummary[entry.BookTitle].ContainsKey(entry.Year))
                {
                    salesSummary[entry.BookTitle][entry.Year] += entry.Quantity;
                }
                else
                {
                    salesSummary[entry.BookTitle][entry.Year] = entry.Quantity;
                }
            }

            return salesSummary;
        }


    }
}
