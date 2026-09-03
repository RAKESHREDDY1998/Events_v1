# Events_v1

Events_v1 is a simple ASP.NET Core 10 MVC application that demonstrates
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

* .NET 10 SDK (or later)
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

* upgraded from .NET 6 to .NET 10 (EF Core / Identity 10.0)
* money columns (`TicketPrice`, `SubTotal`, `Discount`, `DeliveryCharge`,
  `AmountDue`) are stored as `decimal(18,2)` instead of `float`, and
  `SaleDate` is a real `datetime2` instead of a formatted string
* deleting an event no longer cascade‑deletes its sales; the admin
  Delete page refuses to delete an event that has recorded sales
* the customer and the sale are saved in a single `SaveChanges` call,
  so a failed checkout cannot leave an orphaned customer row
* `/Cart/Confirmation` only accepts POST with an anti‑forgery token; a
  crafted GET URL can no longer create a sale
* an `Error` action and view exist for `UseExceptionHandler("/Home/Error")`
* the admin “Edit” page redisplays correctly when there are validation
  errors (categories are reloaded and the heading is set)
* validation: ticket count must be 1–100, ticket price must be positive,
  customer email must be a valid address, and the password confirmation
  error now appears under the Confirm Password field
* the navigation bar only renders the admin link for users who have
  the `IsAdmin` claim
* category drop‑downs retain their value when validation fails
* all pages include the validation scripts automatically
* various markup corrections (`form-control` classes, currency
  formatting on the sales and confirmation pages, dynamic footer year)

If you have an existing database, run `dotnet ef database update` to
apply the `MoneyAsDecimalAndRestrictSaleDelete` migration.  It converts
the existing `SaleDate` strings to `datetime2` using SQL Server's
implicit conversion, so any rows that were written under a non‑US date
format may need to be fixed by hand first.

### Extending

* add new delivery options or pricing rules in
  `CartViewModel`/`Cart.ProcessSale`
* change password requirements in `Program.cs`
* customise the Identity user by extending `Models.DomainModels.User`

## License

This project is provided for educational purposes; bundled client‑side
libraries are licensed under their own terms (see the
`wwwroot/lib/*/LICENSE*` files).
