# Events_v1

Events_v1 is a simple ASP.NET Core 6 MVC application that demonstrates
ticket sales for a small theatre.  It uses Entity Framework Core with
SQL Server LocalDB for persistence and ASP.NET Core Identity for
authentication and an admin‑claim based authorization policy.

## Features

* public listing of events
* authenticated users can purchase tickets
* admin users (flagged at registration) can add/edit/delete events and
  view sales per event
* sales records include customer information, delivery option,
  discounts, totals
* client‑side and server‑side validation on all forms

## Getting started

### Prerequisites

* .NET 6 SDK (or later)
* SQL Server LocalDB (installed with Visual Studio or available via
  [sqlcmd tools](https://aka.ms/sql-localdb))
* Visual Studio 2022 / VS Code (optional)

### Build / run

1. Clone the repository:

   ```sh
   git clone <repo-url>
   cd Events_v1/Events_v1
   ```

2. Update the connection string in `appsettings.json` if you wish to
   use a different database.  By default it uses LocalDB and attaches an
   `Events.mdf` file under `App_Data`.

3. Apply EF Core migrations:

   ```sh
   dotnet ef database update
   ```

   (Tools are referenced in the project; you can also run the
   migrations from Visual Studio's Package Manager Console.)

4. Run the application:

   ```sh
   dotnet run
   ```

   or press **F5** from Visual Studio.  The app launches at
   `https://localhost:7167` (see
   `Properties/launchSettings.json`).

### Using the app

* Browse to **/Event/List** to see available events.
* Register a new user at **/Account/Register**.  Check the “Check if
  Admin” box to create an administrator.
* Administrators see an **Admin** link in the navigation; they can add,
  edit or delete events and view all sales.
* Logged‑in users can buy tickets; client‑side validation prevents
  incorrect input.

### Notes / fixes

The following improvements have been applied since the original
template:

* category drop‑downs retain their value when validation fails
* admin “Edit” page redisplays correctly when there are validation
  errors
* the navigation bar only renders the admin link for users who have
  the `IsAdmin` claim
* all pages include the validation scripts automatically
* various markup corrections (`form-control` classes, quoted `value`
  attributes)

### Extending

* add new delivery options or pricing rules in
  `CartViewModel`/`Cart.ProcessSale`
* change password requirements in `Program.cs`
* customise the Identity user by extending `Models.DomainModels.User`

## License

This project is provided for educational purposes; bundled client‑side
libraries are licensed under their own terms (see the
`wwwroot/lib/*/LICENSE*` files).
