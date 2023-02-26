# CoffeeAndWifi - _Web.API ASP.NET Core_
[Zobacz demo aplikacji](https://coffeeandwifi.webapi.slajan.com.pl/swagger/index.html)


## Opis
- Aplikacja oparta na bazie danych MS SQL
- W bazie zapisywane są dane kawiarni umożliwiających pracę zdalną
- Zaimplementowana autentykacja i autoryzacja z uwzględnieniem ról
- Obsługa zapytań GET, POST, PUT, PATCH, DELETE

## Tech
- W bazie przechowywane Hash'e do haseł użytkowników - zastosowana biblioteka [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next)
- Autentykacja z uwzględnieniem ról z zastosowaniem JSON Web Token 
