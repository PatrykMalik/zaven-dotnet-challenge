# Zadanie rekrutacyjne .NET

Zadaniem kandydata jest poprawienie aplikacji ponieważ nie działa w sposób poprawny oraz jakość kodu pozostawia wiele do życzenia. W kodzie aplikacji jest pozostawionych specialnie kilka błędów i niekonsekwencji, które należy naprawić. Kod powinien być poprawiony z zachowaniem ogólnie przyjętych zasad __Clean Code__ oraz __SOLID__.

__UWAGA__: Rozwiązanie zadań będzie bazą do dyskusji na rozmowie rekrutacyjnej. Nie wszystko z treści zadania musi być zaimplementowane lub naprawione w 100% aby do rozmowy doszło. 

##### Przesyłanie rozwiązań
Repozytorium należy ściągnąć i umieścić na swoim indywidualnym repozytorium. Po zakończeniu implementacji link do repozytorium z rozwiązaniem należy przesłać osobie odpowiedzialną za rekrutację kandydata.

# Opis zadania

### Opis Aplikacji
Aplikacja jest napisana w ASP.NET MVC 5.
Jej zadaniem jest zlecanie zadań(Jobów) oraz ich równoległa obsługa(procesowanie).

Aplikacja powinna mieć 3 widoki:
- __Widok listy Job'ów__ - powinien mieć dwa przyciski na górze, pierwszy do dodawania nowych Job'ów, a drugi do włączenia ich procesowania, ponadto powinien zawierać listę Job'ów z ich statusami posortowaną po dacie dodania.
- __Widok dodawania Job'a__ - powinien mieć formularz z dwoma polami, Nazwą Job'a oraz Datą(opcjonalną) po której Job powinien móc być procesowany(Jeśli dany Job ma ustawioną datę 'Do After' na 02.02.2020 to  dopiero w ten dzień będzie móc być procesowany, nie wcześniej). Nazwa Job'a nie może się powtórzyć. Formularz powinien być walidowany po stronie aplikacji jak i serwera.
- __Szczegóły Job'a__ - powinien zawierać dane Joba takie jak: Nazwa, Status, data 'Do After' oraz posortowaną po dacie listę Log'ów danego Job'a

Zasady processowania Job'ów:
- Przycisk __Process__ powinien rozpocząć równoległe procesowanie tych Job'ów które są nowe(status __New__) lub nieudane(status __Failed__). 
- Nieudane Job'y po 5 próbach powinny zmienić status na zamknięte(status __Closed__) i nie być dalej processowane. 
- Jeśli Job się powiedzie powinien przejść w status zakończony(status __Done__).

### Rzeczy do zaimplementowania
0. Naprawienie równoległego procesowania Job'ów
   - Obecnie status procesowanych Jobów zmienia się dopiero przy drugim naciśnięciu przycisku 'Process'
1. Walidacja po stronie klienta oraz serwera
   - Data 'Do after' jest opcjonalna ale powinna być datą
   - Name jest wymagane i nie powinno być puste
   - Nie może być dwóch Job'ów o takiej samej nazwie
2. Dodawanie Log'ów Job'a
   - Dodanie Job'a powinno być logowane(Nowy wpis w tabeli Logs z deskryptywnym opisem)
   - Kazda zmiana Statusu Job'a powinna być logowana(Nowy wpis w tabeli Logs z deskryptywnym opisem)
3. Dodanie pola 'LastUpdatedAt' do Job'a
   - Pole powinno być nullowalne typu DateTime
   - Powinna być tam data ostatniej zmiany Statusu Job'a 
4. Widok szczegółów Job'a
   - Uzytkownik powinien być tam przeniesiony po kliknięciu przycisku 'Details' obok Job'a
   - Widok powinien zawierać wszystkie informacje na temat Job'a razem z jego Logami
   - Logi powinny być posortowane po dacie utworzenia Log'a 	
5. Posortowanie Job'ów
   - Job'y powinny być posortowane po dacie utworzenia
6. Dodanie licznika niepowodzeń Job'a, oraz dodanie statusu 'Closed'
   - Jeśli Job się nie powiedzie, licznik nipowodzeń powinien się zwiększyć
   - Gdy licznik niepowodzeń osiągnie wartość 5, status Job'a powinien być zmieniony na 'Closed'
7. Obsługa 'Do after'
   - Job powinien być procesowany po dacie 'Do After' jeśli ją posiada
8. Ostylowanie aplikacji wedle uznania(Bootstrap lub inny dowolny CSS framework) 

### Rzeczy potecjalnie do poprawienia
1. Dostęp do bazy danych
2. Inicjowanie srwisów i repozytoriów
3. Konfiguracja cyklu życia w kontenerze DI
4. Warstwowość solucji
5. Struktóra katalogów, nazwy plików, nazwy klas, nazwy metod i zmiennych
6. Uzycie Async/Await
7. Uzycie dedykowanych ViewModeli