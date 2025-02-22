# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7081

# Copy everything and build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY CustomersAPI ./
RUN dotnet restore "CustomersAPI/CustomersAPI.csproj"
RUN dotnet publish "CustomersAPI/CustomersAPI.csproj" -c Release -o /app/publish

# Use runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CustomersAPI.dll"]
