FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Running dotnet restore first to cache dependencies
COPY WebApp/WebApp.csproj WebApp/
RUN dotnet restore WebApp/WebApp.csproj

# Copy the rest of the source code
COPY . .

# Set working dir
WORKDIR /src/WebApp

# Build the application
RUN dotnet build WebApp.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish WebApp.csproj -c $BUILD_CONFIGURATION -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exposing http- and https-ports
EXPOSE 80
EXPOSE 443

# Run the application
ENTRYPOINT [ "dotnet", "WebApp.dll" ]