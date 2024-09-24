# Bank System

[![CI](https://github.com/larsjuvik/BankSystem/actions/workflows/CI.yml/badge.svg?branch=main)](https://github.com/larsjuvik/BankSystem/actions/workflows/CI.yml)

**This project is a demo, and should not be considered production-ready.**
**Still under development.**

This is a demo of a (fictional) bank system called BlueFlare, written in Blazor Web App, with .NET 9.

![A screenshot of the application](./docs/Screenshot_Home.png)
All data in the screenshot are fictional.

## Building the application

### Building release version

```sh
dotnet publish -c Release
```

### Running the application

```sh
dotnet run --project WebApp
```

### Building and running with Docker

```sh
docker build -t bank-system .
docker run --name bank-system -d -p 8080:8080 bank-system
```

## Goals (as of yet)

- [ ] A data table for Admins to manage users
- [ ] A home page for users, where they can add bank accounts
- [ ] A transactions page, where users can see transactions from a specific account

## Security

This application uses cookie-based authentication and authorization, and has areas for improvement regarding security.
Due to the persistent nature of SignalR connection when using Blazor, the application does not detect cookie expiration immediately.
Therefore, if a user is still on the same SignalR connection after logging in, they may be logged in and authenticated
even after the cookie has expired. On page refresh, the user will be redirected to the login page.

## Attributions

Thank you to the following libraries and frameworks :heart:

- [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [MudBlazor](https://mudblazor.com)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
