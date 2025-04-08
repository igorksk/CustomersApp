# Use the ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 7081

# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CustomersAPI ./
RUN dotnet restore "CustomersAPI/CustomersAPI.csproj"
COPY . .
WORKDIR "/src/CustomersAPI"
RUN dotnet build "CustomersAPI/CustomersAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomersAPI/CustomersAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomersAPI.dll"]

