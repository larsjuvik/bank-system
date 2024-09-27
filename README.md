# Bank System

[![CI](https://github.com/larsjuvik/BankSystem/actions/workflows/CI.yml/badge.svg?branch=main)](https://github.com/larsjuvik/BankSystem/actions/workflows/CI.yml)

**This is a demo still under development and should not be considered production-ready.**

This is a fictional bank system called BlueFlare, written in Blazor Web App using .NET 8.

![A screenshot of the application](./docs/Screenshot_Home.png)
*All data in the screenshot are fictional.*

## Building & Running :rocket:

### Building and running with Docker

```sh
docker build -t bank-system .
docker run --name bank-system -d -p 8080:8080 bank-system
```

### Running locally

```sh
dotnet run --project WebApp
```

### Publishing release version

```sh
dotnet publish -c Release
```

## Application Structure :books:

The application is in global `InteractiveServer` mode in order to use MudBlazor.
Uses cookie-based Authentication and authorization without `ASP.NET Core Identity`.
On startup, the database is seeded with randomized data.
The database is in-memory, and resets when the application resets.

## Security notes :closed_lock_with_key:

This application is a demo at heart, and has settings promoting easy development on the cost of less security.
For an overview the security, see the [Security](./docs/SECURITY.md) document.

## Goals :white_check_mark:

- [ ] A home page for users
  - [x] Overview over accounts and cards
  - [ ] Functionality for adding accounts
  - [ ] Functionality for adding cards
- [ ] A transactions page for users
  - Ability to see transactions on different accounts
- [ ] A page for users to edit their own information
- [ ] A payment menu-section for users
  - [ ] A page for users to pay other users within the bank system
    - [ ] Search with autocomplete for names / accounts
  - [ ] A page for users to prompt other users for payment
  - [ ] A page for users to see outstanding payments
- [x] An admin page for observing user logins
- [ ] An admin page for editing user profiles
- [ ] Decide on a common currency / date format

### Nice-to-have's

- [ ] Delete account functionality
- [ ] Unique generated profile picture based on username
- [ ] Change password functionality
- [ ] Use SQL Server / PostgreSQL instead of in-memory database
- [ ] Sliding cookie expiration within SignalR-connection
  - Per now users are logged out when cookie expires, even if they are still using app
- [ ] Use `ASP.NET Core Identity`

## Attributions :star:

BlueFlare was built on the following libraries and frameworks:

- [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [MudBlazor](https://mudblazor.com)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
