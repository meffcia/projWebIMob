# Sprawozdanie z realizacji projektów w technologii MVC
**Autorzy:**  Patrycja Falkowska 325462, Wiktoria Jeleniewicz 

---

## Zadanie 1: Stworzenie kalkulatora w technologii web MVC

### Opis zadania
Należało stworzyć aplikację webową działającą jako prosty kalkulator, który dodaje dwie liczby. Wymagane było zastosowanie architektury MVC. 

### Wymagania
- **Model**: reprezentuje dane (dwie liczby oraz wynik).
- **Widok**: zawiera formularz do wprowadzenia dwóch liczb.
- **Kontroler**: odbiera dane z formularza, sumuje liczby i zwraca wynik do widoku.
- **Formularz**: powinien zawierać pola do wprowadzania liczb oraz przycisk „Dodaj”.

### Implementacja
```c#
﻿namespace proj2.Models
{
    public class CalculatorModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public double? Result { get; set; } // Nullable in case result is not calculated yet
    }
}
```
```c#
﻿@model proj2.Models.CalculatorModel

<h2>Kalkulator</h2>

<form asp-action="Index" method="post">
    <div>
        <label for="number1">Liczba 1:</label>
        <input type="number" step="any" name="Number1" value="@Model?.Number1" />
    </div>
    <div>
        <label for="number2">Liczba 2:</label>
        <input type="number" step="any" name="Number2" value="@Model?.Number2" />
    </div>
    <div>
        <button type="submit">Dodaj</button>
    </div>
</form>

@if (Model?.Result != null)
{
    <div>
        <h3>Wynik: @Model.Result</h3>
    </div>
}
```

---

## Zadanie 2: Stworzenie aplikacji typu "TODO list" z wykorzystaniem sesji

### Opis zadania
Zadanie polegało na stworzeniu aplikacji typu „lista TODO” z użyciem technologii MVC. Lista zadań miała być przechowywana po stronie serwera w sesji.

### Wymagania
- **Przechowywanie danych**: lista zadań powinna być przechowywana w sesji po stronie serwera.
- **Funkcjonalności**:
  - Dodawanie nowych zadań do listy.
  - Usuwanie zadań z listy.
  - Oznaczanie zadań jako ukończone.
- **Widok**: powinien dynamicznie aktualizować listę zadań na podstawie danych z sesji.

### Implementacja
```c#
﻿namespace proj2.Models
{ 
    public class TodoItem
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
```
```c#
﻿@mode﻿l List<proj2.Models.TodoItem>

<h2>TODO Lista</h2>

<form asp-action="Add" method="post">
    <div>
        <input type="text" name="description" placeholder="Dodaj nowe zadanie" />
        <button type="submit">Dodaj</button>
    </div>
</form>

<ul>
    @for (int i = 0; i < Model.Count; i++)
    {
        <li>
            @if (Model[i].IsCompleted)
            {
                <s>@Model[i].Description</s>
           }
            else
            {
                @Model[i].Description
                <form asp-action="Complete" method="post" style="display:inline">
                    <input type="hidden" name="index" value="@i" />
                    <button type="submit">Ukończ</button>
                </form>
            }

            <form asp-action="Delete" method="post" style="display:inline">
                <input type="hidden" name="index" value="@i" />
                <button type="submit">Usuń</button>
            </form>
        </li>
    }
</ul>
```

---

## Zadanie 3: Stworzenie aplikacji używającej Local Storage

### Opis zadania
Stworzenie aplikacji webowej, która przechowuje dane użytkownika w Local Storage przeglądarki. Przykładowa aplikacja do zapisywania i zarządzania listą ulubionych cytatów.

### Wymagania
- **Dodawanie cytatów**: użytkownik może dodawać nowe cytaty, które są zapisywane w Local Storage.
- **Dostępność danych**: cytaty powinny być dostępne po odświeżeniu strony.
- **Zarządzanie cytatami**: użytkownik powinien mieć możliwość usuwania cytatów oraz edytowania istniejących.

### Implementacja
```c#
﻿@{
    ViewData["Title"] = "Ulubione Cytaty";
    Layout = "_Layout"; // Użycie wspólnego układu
}

<h2>Ulubione Cytaty</h2>

<div>
    <input type="text" id="quoteInput" placeholder="Wpisz cytat..." />
    <button id="addQuoteBtn">Dodaj Cytat</button>
</div>

<ul id="quotesList">
    <!-- Cytaty będą dodawane tutaj -->
</ul>

<script>
    // Funkcja do załadowania cytatów z Local Storage
    function loadQuotes() {
        const quotes = JSON.parse(localStorage.getItem('quotes')) || [];
        const quotesList = document.getElementById('quotesList');
        quotesList.innerHTML = '';

        quotes.forEach((quote, index) => {
            const li = document.createElement('li');
            li.textContent = quote;
            li.appendChild(createEditButton(index));
            li.appendChild(createDeleteButton(index));
            quotesList.appendChild(li);
        });
    }

    // Funkcja do tworzenia przycisku edytowania
    function createEditButton(index) {
        const button = document.createElement('button');
        button.textContent = 'Edytuj';
        button.onclick = function () {
            const newQuote = prompt('Edytuj cytat:', localStorage.getItem('quotes').split(',')[index]);
            if (newQuote) {
                const quotes = JSON.parse(localStorage.getItem('quotes')) || [];
                quotes[index] = newQuote;
                localStorage.setItem('quotes', JSON.stringify(quotes));
                loadQuotes();
            }
        };
        return button;
    }

    // Funkcja do tworzenia przycisku usuwania
    function createDeleteButton(index) {
        const button = document.createElement('button');
        button.textContent = 'Usuń';
        button.onclick = function () {
            const quotes = JSON.parse(localStorage.getItem('quotes')) || [];
            quotes.splice(index, 1);
            localStorage.setItem('quotes', JSON.stringify(quotes));
            loadQuotes();
        };
        return button;
    }

    // Funkcja do dodawania cytatu
    document.getElementById('addQuoteBtn').addEventListener('click', function () {
        const quoteInput = document.getElementById('quoteInput');
        const quote = quoteInput.value.trim();
        if (quote) {
            const quotes = JSON.parse(localStorage.getItem('quotes')) || [];
            quotes.push(quote);
            localStorage.setItem('quotes', JSON.stringify(quotes));
            quoteInput.value = '';
            loadQuotes();
        }
    });

    // Załaduj cytaty po załadowaniu strony
    window.onload = loadQuotes;
</script>
```

---

## Podsumowanie
W ramach sprawozdania zrealizowano trzy zadania wymagające użycia technologii MVC oraz Local Storage:
1. **Kalkulator**: aplikacja wykorzystująca model, widok oraz kontroler do realizacji prostej operacji matematycznej.
2. **Lista TODO**: aplikacja zarządzająca zadaniami z przechowywaniem danych w sesji po stronie serwera.
3. **Zarządzanie cytatami**: aplikacja używająca Local Storage do przechowywania danych po stronie przeglądarki, z możliwością dodawania, usuwania i edytowania elementów listy.
