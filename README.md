# Bank System

This is a demo of a bank system, written in Blazor Web App, with .NET 8.

![A screenshot of the application](./docs/Screenshot.png)

## Building without Docker

```sh
dotnet run --project WebApp
```

## Building and running with Docker

```sh
docker build -t bank-system .
docker run --name bank-system -d -p 8080:8080 bank-system
```
