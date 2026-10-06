# Card API

Prosta aplikacja REST API w C# / ASP.NET Core, która na podstawie użytkownika i numeru karty zwraca listę dozwolonych akcji dla danej karty.

Projekt wykorzystuje **Strategy Pattern** do rozdzielenia reguł zależnych od statusu karty.

## Technologie

* C#
* .NET 8
* ASP.NET Core Web API
* xUnit

---

## Uruchomienie

Po uruchomieniu aplikacji API jest dostępne pod adresem:

```text
http://localhost:5241
```

W pliku `.http` można wykorzystać następujące przykłady.

### Pobranie dozwolonych akcji dla karty

```http
@CardApi_HostAddress = http://localhost:5241
GET {{CardApi_HostAddress}}/api/cards/User1/Card11/actions
Accept: application/json
```

Endpoint:

```text
GET /api/cards/{userId}/{cardNumber}/actions
```

Przykład:

```text
GET /api/cards/User1/Card11/actions
```

API:

1. wyszukuje użytkownika,
2. wyszukuje kartę,
3. sprawdza jej status,
4. wybiera odpowiednią strategię,
5. zwraca dozwolone akcje.

Przykładowa odpowiedź:

```json
[
  "ACTION3",
  "ACTION4",
  "ACTION6",
  "ACTION8",
  "ACTION9",
  "ACTION10",
  "ACTION12",
  "ACTION13"
]
```

Dokładna lista akcji zależy od:

* typu karty,
* statusu karty,
* informacji, czy PIN jest ustawiony.

---

## Pobranie wszystkich przykładowych kart

```http
GET {{CardApi_HostAddress}}/api/cards
Accept: application/json
```

Endpoint:

```text
GET /api/cards
```

Zwraca wszystkie przykładowe karty wraz z informacjami o:

* numerze karty,
* typie karty,
* statusie,
* ustawieniu PIN-u.

Przykładowy fragment odpowiedzi:

```json
{
  "User1": [
    {
      "cardNumber": "Card11",
      "cardType": "Prepaid",
      "cardStatus": "Ordered",
      "isPinSet": false
    }
  ]
}
```

Statusy i typy kart są zwracane jako tekst, a nie jako wartości liczbowe enum.

---

# Reguły biznesowe

Aplikacja posiada następujące typy kart:

```text
Prepaid
Debit
Credit
```

oraz statusy:

```text
Ordered
Inactive
Active
Restricted
Blocked
Expired
Closed
```

Reguły dotyczące dozwolonych akcji są zaimplementowane w osobnych strategiach.

Przykładowo:

```text
Active
    ↓
ActiveCardActionStrategy
```

Strategia sprawdza:

```csharp
public bool CanHandle(CardDetails card)
{
    return card.CardStatus == CardStatus.Active;
}
```

Następnie:

```csharp
GetActions(card)
```

zwraca akcje dostępne dla danej karty.

---

# Strategy Pattern

Każda strategia implementuje:

```csharp
public interface ICardActionStrategy
{
    bool CanHandle(CardDetails card);

    IReadOnlyCollection<string> GetActions(CardDetails card);
}
```

Przykładowe strategie:

```text
OrderedCardActionStrategy
InactiveCardActionStrategy
ActiveCardActionStrategy
RestrictedCardActionStrategy
BlockedCardActionStrategy
ExpiredCardActionStrategy
ClosedCardActionStrategy
```

Za wybór strategii odpowiada:

```text
CardActionRuleEngine
```

Engine wyszukuje strategię, która potrafi obsłużyć dany status:

```csharp
var strategy = _strategies
    .FirstOrDefault(x => x.CanHandle(card));
```

Dzięki temu kontroler nie zawiera bezpośrednio reguł biznesowych.

---

# Automatyczna rejestracja strategii

Strategie są automatycznie wyszukiwane za pomocą Reflection.

Dzięki temu po dodaniu nowej strategii nie trzeba dodawać kolejnej ręcznej rejestracji w `Program.cs`.

Przykładowo:

```csharp
public class ActiveCardActionStrategy : ICardActionStrategy
{
    ...
}
```

zostanie automatycznie wykryta jako implementacja:

```text
ICardActionStrategy
```

i zarejestrowana w Dependency Injection.

---

# Obsługa błędów

API zwraca:

### Brak `userId`

```text
400 Bad Request
```

### Brak `cardNumber`

```text
400 Bad Request
```

### Nieistniejący użytkownik lub karta

```text
404 Not Found
```

Przykład:

```text
GET /api/cards/UnknownUser/Card11/actions
```

zwróci:

```text
404 Not Found
```

---

# Testy jednostkowe

Projekt posiada osobny projekt testowy wykorzystujący:

* xUnit

Testy sprawdzają przede wszystkim **reguły biznesowe**.


## Test pokrywający wszystkie statusy

Jeden z testów sprawdza, czy każdy element `CardStatus` posiada odpowiadającą mu strategię.

Przykładowo, jeżeli do enum zostanie dodany:

```csharp
public enum CardStatus
{
    Ordered,
    Inactive,
    Active,
    Restricted,
    Blocked,
    Expired,
    Closed,
    Inna
}
```

a nie zostanie utworzona strategia obsługująca:

```text
Inna
```

test zakończy się niepowodzeniem.

Dzięki temu test pełni rolę zabezpieczenia przed sytuacją:

```text
nowy CardStatus
       ↓
brak strategii
       ↓
test FAIL
```

Jest to szczególnie przydatne, ponieważ przy dodawaniu nowego statusu programista od razu otrzyma informację, że należy dodać odpowiednią strategię.

---

# Uruchomienie testów

W Visual Studio:

```text
Test
 └── Test Explorer
      └── Run All Tests
```

lub z terminala:

```bash
dotnet test
```



---

# Struktura projektu

```text
CardActions.Api
│
├── Controllers
│   └── CardsController.cs
│
├── Models
│   ├── CardDetails.cs
│   ├── CardStatus.cs
│   └── CardType.cs
│
└── Services
    ├── CardService.cs
    ├── CardActionService.cs
    │
    └── Rules
        ├── CardActionRuleEngine.cs
        │
        └── Strategies
            ├── ICardActionStrategy.cs
            ├── OrderedCardActionStrategy.cs
            ├── InactiveCardActionStrategy.cs
            ├── ActiveCardActionStrategy.cs
            ├── RestrictedCardActionStrategy.cs
            ├── BlockedCardActionStrategy.cs
            ├── ExpiredCardActionStrategy.cs
            └── ClosedCardActionStrategy.cs
```


