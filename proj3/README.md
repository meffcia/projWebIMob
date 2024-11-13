# Księgarnia Zaplecze System

### Patrycja Falkowska 325462 
### Wiktoria Jeleniewicz 325477
---

System zarządzania sprzedażą książek, stworzony przy użyciu ASP.NET Core MVC, umożliwiający dodawanie, edytowanie i rejestrowanie sprzedaży książek oraz automatyczne generowanie statystyk na podstawie szczegółowych danych sprzedaży.

## Spis treści
1. [Technologie](#technologie)
2. [Funkcjonalności](#funkcjonalności)
3. [Struktura Plików](#struktura-plików)
4. [Dalszy rozwój](#dalszy-rozwój)

## Technologie

Projekt został stworzony przy użyciu:
- **ASP.NET Core MVC** - framework do budowania aplikacji webowych w .NET
- **Now UI Dashboard** - szablonu interfejsu użytkownika dostarczonego z repozytorium [now-ui-dashboard](https://github.com/tomasz-trener/now-ui-dashboard-master)
- **JSON** - do przechowywania danych książek i sprzedaży w formacie JSON

## Funkcjonalności

1. **Dodawanie i edytowanie książek** - Możliwość rejestracji książek i ich cen w systemie.
2. **Rejestracja sprzedaży** - Funkcja dodawania transakcji sprzedaży, wybierania książek z listy, oraz wprowadzania ilości sprzedanych sztuk z podziałem na daty.
3.  **Historia sprzedaży** - Wyświetlenie listy dokonanych transakcji z opcją edycji lub usunięcia.

## Struktura Plików

```
proj3

├── Properties
│   ├── LaunchSettings.json
├── wwwroot
│	├── assets                   
│	│   ├── css
│	│   ├── js
│	│   ├── fonts
│	│   ├── img
│	│   └── scss
├── Controllers
│   ├── BookController.cs
│   ├── HomeController.cs
│   ├── StatsController.cs
│   └── SaleController.cs
├── Data
│   ├── books.json
│   └── sales.json
├── Models
│   ├── BookModel.cs
│   ├── ErrorViewModel.cs
│   ├── StatsModel.cs
│   └── SaleModel.cs
├── Views
│   ├── Home
│   │   └── Index.cshtml
│   ├── Book
│   │   ├── Index.cshtml
│   │   ├── Edit.cshtml
│   │   └── Create.cshtml
│   ├── Sales
│   │   ├── Index.cshtml
│   │   ├── Edit.cshtml
│   │   └── Create.cshtml
│   ├── Stats
│   │   └── Index.cshtml
│   └── Shared
│   │   ├── Error.cshtml
│       └── _Layout.cshtml
├── appsettings.json
├── Program.cs
```

## Dalszy rozwój

1. **Rozbudowa modelu książek** - Dodanie innych właściwości, takich jak ISBN, gatunek, czy wydawnictwo.
2. **Weryfikacja poprawności danych** - Dodanie walidacji na poziomie front-endu i back-endu.
3. **Zarządzanie danymi przez bazę danych** - Zamiana plików JSON na bazę danych SQLite lub inną obsługiwaną przez Entity Framework Core.
