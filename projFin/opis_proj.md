# **Opis Biznesowy Projektu**

Patrycja Falkowska, Wiktoria Jeleniewicz

## **Opis Biznesowy Projektu**

Projekt zakłada stworzenie dwóch aplikacji: **aplikacji webowej** opartej na architekturze **MVC (Model-View-Controller)** oraz **aplikacji mobilnej** stworzonej z użyciem **MVVM (Model-View-ViewModel)**, w oparciu o wspólne **API**. Celem aplikacji jest stworzenie prostego modelu sklepu internetowego, który umożliwia użytkownikom zarządzanie produktami, dodawanie ich do koszyka oraz realizowanie zakupów.

Obie aplikacje będą wykorzystywać **ASP.NET Core Web API** do komunikacji z serwerem, co umożliwi ich integrację na obu platformach. API będzie centralnym punktem dla logowania, przeglądania produktów, zarządzania koszykiem i zakupami. Obie aplikacje będą obsługiwać najważniejsze funkcjonalności sklepu internetowego, takie jak rejestracja użytkowników, zarządzanie danymi produktów, koszykiem, a także realizacja transakcji.

## **Zakres Aplikacji**

1.  **Aplikacja webowa (MVC):**  
    Aplikacja oparta na architekturze **MVC (Model-View-Controller)**, zaprojektowana w taki sposób, by prezentacja (widok) była oddzielona od logiki aplikacji (model) oraz sterowania przepływem danych (kontroler). Będzie to klasyczna aplikacja internetowa, działająca na serwerze i dostępna przez przeglądarkę.
    
2.  **Aplikacja mobilna (MVVM):**  
    Aplikacja mobilna, stworzona w ramach projektu **.NET MAUI** z wykorzystaniem wzorca **MVVM (Model-View-ViewModel)**, umożliwiająca użytkownikom korzystanie ze sklepu internetowego na urządzeniach mobilnych (Android/iOS). Architektura MVVM pomoże w utrzymaniu czystości kodu, rozdzielając logikę aplikacji od prezentacji.
    
3.  **API (.NET Core Web API):**  
    Wspólna część dla obu aplikacji, która obsługuje logikę biznesową, połączenie z bazą danych, zarządzanie użytkownikami, produktami, płatnościami i koszykiem. 
    

## **Wymagania Funkcjonalne**

### 1. **Logowanie i rejestracja użytkownika**

-   **MVC:**
    -   Użytkownik może się zalogować za pomocą e-maila i hasła.
    -   Użytkownik ma możliwość rejestracji konta (formularz rejestracyjny).
-   **MAUI:**
    -   Synchronizacja konta z aplikacją mobilną (logowanie za pomocą danych użytkownika).
    -   Funkcjonalność rejestracji użytkownika na urządzeniu mobilnym.

### 2. **Zarządzanie produktami**

-   **MVC:**
    -   Lista produktów jest wyświetlana w formie kafelków.
    -   Detale produktu obejmujące nazwę, cenę, opis oraz dostępność.
-   **MAUI:**
    -   Przeglądanie listy produktów w widoku mobilnym.
    -   Wyświetlanie szczegółów produktu na ekranie mobilnym.

### 3. **Zarządzanie koszykiem**

-   **MVC:**
    -   Możliwość dodawania produktów do koszyka.
    -   Wyświetlanie zawartości koszyka (z ilością i ceną).
    -   Możliwość usuwania produktów z koszyka.
-   **MAUI:**
    -   Koszyk dostępny również na urządzeniach mobilnych z możliwością edytowania pozycji.

### 4. **Proces płatności**

-   **MVC:**
    -   Przejście do płatności, wybór metody płatności.
    -   Wprowadzenie danych adresowych.
    -   Potwierdzenie oraz zakończenie transakcji.
-   **MAUI:**
    -   Proces płatności z możliwością wyboru metod płatności i formularzem do wprowadzenia danych.

## **Wymagania Niefunkcjonalne**

### 1. **Wydajność**

-   **Web MVC & MAUI:**  
    Aplikacje muszą ładować dane i strony w ciągu kilku sekund. Koszyk i proces płatności nie mogą powodować opóźnień przy dużej liczbie produktów.

### 2. **Bezpieczeństwo**

-   **API:**  
    Szyfrowanie danych użytkowników oraz płatności, np. za pomocą **HTTPS**. Przechowywanie haseł w sposób bezpieczny, poprzez hashowanie i solenie.

### 3. **Skalowalność**

-   **API (.NET Core):**  
    API musi być zaprojektowane w sposób umożliwiający łatwe skalowanie, np. przez rozdzielenie logiki na mikroserwisy w przyszłości.

### 4. **Responsywność**

-   **MVC:**  
    Aplikacja webowa musi dostosowywać się do różnych rozdzielczości ekranów, zwłaszcza w przypadku urządzeń mobilnych (rozdzielczości ekranów telefonów, tabletów).
    
-   **MAUI:**  
    Aplikacja mobilna musi działać na różnych urządzeniach Android i iOS, dostosowując się do różnych wersji systemów operacyjnych.
    

### 6. **Kompatybilność**

-   **Web MVC:**  
    Aplikacja musi działać na głównych przeglądarkach internetowych (Chrome, Firefox, Safari, Edge).
    
-   **MAUI:**  
    Kompatybilność z systemami operacyjnymi Android i iOS, obsługa różnych rozdzielczości ekranów urządzeń mobilnych.

## **Modele Projektów w Visual Studio**

1.  **Web MVC (ASP.NET Core MVC):**
    -   W projekcie będzie wykorzystywany **szablon ASP.NET Core Web Application** z opcją MVC.
    -   Dla wzorca MVC, aplikacja jest podzielona na trzy główne komponenty:
        -   **Model:** Reprezentacja danych (np. produkt, użytkownik).
        -   **View:** Strony HTML (widoki produktów, koszyka, formularze).
        -   **Controller:** Logika sterująca aplikacją (np. dodawanie produktu do koszyka).
2.  **Aplikacja Mobilna (MAUI MVVM):**
    -   Projekt będzie tworzony z użyciem **.NET MAUI** i szablonu **Mobile App (MAUI)**. Będzie stosowany wzorzec **MVVM**, który oddziela logikę aplikacji (Model) od interfejsu użytkownika (View), a pośrednikiem między nimi jest **ViewModel**.
3.  **API (.NET Core Web API):**
    -   Aplikacja API będzie stworzona z użyciem szablonu **ASP.NET Core Web API**, co pozwala na łatwą integrację z front-endem (MVC i MAUI).


