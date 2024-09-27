# Bank System

[![CI](https://github.com/larsjuvik/BankSystem/actions/workflows/CI.yml/badge.svg?branch=main)](https://github.com/larsjuvik/BankSystem/actions/workflows/CI.yml)

**This is a demo still under development and should not be considered production-ready.**

This is a demo of a fictional bank system called BlueFlare, written in Blazor Web App, using .NET 8.

![A screenshot of the application](./docs/Screenshot_Home.png)
All data in the screenshot are fictional.

## Building & Running :hammer:

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

## Security notes

This application is a demo at heart, and has settings promoting easy development on the cost of less security.
For an overview the security, see the [Security](./docs/SECURITY.md) document.

## Goals and nice-to-have's

- [ ] A data table for Admins to manage users
- [ ] A home page for users, where they can add bank accounts
- [ ] A transactions page, where users can see transactions from a specific account
- [ ] Sliding cookie expiration within SignalR-connection

## Attributions

Thank you to the following libraries and frameworks :heart:

- [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [MudBlazor](https://mudblazor.com)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
