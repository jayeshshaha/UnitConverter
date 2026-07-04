# Multi-stage Dockerfile for UnitConverter solution
# Uses .NET 10 SDK for build and ASP.NET runtime for runtime

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files and restore before copying everything for better caching
COPY ["UnitConverter.API/UnitConverter.API.csproj", "UnitConverter.API/"]
COPY ["UnitConverter.Services/UnitConverter.Services.csproj", "UnitConverter.Services/"]
COPY ["UnitConverter.Domain/UnitConverter.Domain.csproj", "UnitConverter.Domain/"]

RUN dotnet restore "UnitConverter.API/UnitConverter.API.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR /src/UnitConverter.API

# Publish the API project
RUN dotnet publish "UnitConverter.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Allow Render (or other platforms) to set the port via PORT env var
ENV ASPNETCORE_URLS="http://+:${PORT:-8080}"
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UnitConverter.API.dll"]
